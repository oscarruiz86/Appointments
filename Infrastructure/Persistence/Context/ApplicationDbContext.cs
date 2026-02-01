using Application.Interfaces.Infrastructure.Services;
using Application.Interfaces.Persistence.Filters;
using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Identity;
using Infrastructure.Persistence.Filters;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Context;

/// <summary>
/// DbContext principal de la aplicación Appointments.
/// 
/// Responsabilidades:
/// - Exponer DbSets de entidades de negocio e Identity.
/// - Aplicar filtros globales de multi-tenancy y soft delete.
/// - Gestionar auditoría automática (creación, modificación, eliminación).
/// 
/// Este contexto está diseñado para:
/// - Soportar multi-tenancy mediante <see cref="ITenantProvider"/>.
/// - Permitir desactivar filtros de forma granular usando <see cref="IFilterContext"/>.
/// - Mantener Identity desacoplado del dominio.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Proveedor del tenant actual (resuelto por request).
    /// </summary>
    private readonly ITenantProvider _tenantProvider;

    /// <summary>
    /// Contexto de filtros que permite habilitar o deshabilitar
    /// filtros globales de forma controlada (ej: Login, Admin).
    /// </summary>
    private readonly IFilterContext _filterContext;

    /// <summary>
    /// Servicio que provee información del usuario autenticado
    /// (usado para auditoría).
    /// </summary>
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Constructor del DbContext.
    /// </summary>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantProvider tenantProvider,
        IFilterContext filterContext,
        ICurrentUserService currentUserService)
        : base(options)
    {
        _tenantProvider = tenantProvider;
        _filterContext = filterContext;
        _currentUserService = currentUserService;
    }

    // ==============================
    // DbSets de negocio
    // ==============================

    /// <summary>Tenants del sistema.</summary>
    public DbSet<Tenant> Tenants => Set<Tenant>();

    /// <summary>Empleados asociados a un tenant.</summary>
    public DbSet<Employee> Employees => Set<Employee>();

    /// <summary>Clientes del tenant.</summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>Servicios ofrecidos por el tenant.</summary>
    public DbSet<Service> Services => Set<Service>();

    /// <summary>Citas agendadas.</summary>
    public DbSet<Appointment> Appointments => Set<Appointment>();

    /// <summary>Estados posibles de una cita.</summary>
    public DbSet<AppointmentStatus> AppointmentStatuses => Set<AppointmentStatus>();

    /// <summary>Horarios de trabajo configurados.</summary>
    public DbSet<WorkingHour> WorkingHours => Set<WorkingHour>();

    /// <summary>Bloqueos de agenda.</summary>
    public DbSet<Block> Blocks => Set<Block>();

    // ==============================
    // Identity (ligero)
    // ==============================

    /// <summary>Usuarios de la aplicación.</summary>
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    /// <summary>Roles de la aplicación.</summary>
    public DbSet<ApplicationRole> Roles => Set<ApplicationRole>();

    /// <summary>Relación usuario-rol.</summary>
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    // ==============================
    // ModelCreating
    // ==============================

    /// <summary>
    /// Configura el modelo de EF Core.
    /// Aplica configuraciones por assembly y filtros globales.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todas las configuraciones IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);

        ApplyGlobalFilters(modelBuilder);
    }

    // ==============================
    // Global Filters
    // ==============================

    private void ApplyGlobalFilters(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            var isAuditable = typeof(IAuditableEntity).IsAssignableFrom(clrType);
            var isTenantScoped = typeof(ITenantScoped).IsAssignableFrom(clrType);

            if (isAuditable && isTenantScoped)
            {
                ApplyTenantAndSoftDeleteFilter(builder, clrType);
                continue;
            }

            if (isAuditable)
            {
                ApplySoftDeleteFilter(builder, clrType);
                continue;
            }

            if (isTenantScoped)
            {
                ApplyTenantFilter(builder, clrType);
            }
        }
    }

    private void ApplyTenantAndSoftDeleteFilter(ModelBuilder builder, Type entityType)
    {
        var method = typeof(ApplicationDbContext)
            .GetMethod(nameof(SetTenantAndSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Instance)!
            .MakeGenericMethod(entityType);

        method.Invoke(this, new object[] { builder });
    }

    private void SetTenantAndSoftDeleteFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IAuditableEntity, ITenantScoped
    {
        builder.Entity<TEntity>().HasQueryFilter(e =>
            (_filterContext.DisableSoftDeleteFilter || !e.IsDeleted) &&
            (_filterContext.DisableTenantFilter ||
             _tenantProvider.TenantId == Guid.Empty ||
             e.TenantId == _tenantProvider.TenantId)
        );
    }

    private void ApplySoftDeleteFilter(ModelBuilder builder, Type entityType)
    {
        var method = typeof(ApplicationDbContext)
            .GetMethod(nameof(SetSoftDeleteFilter),
                BindingFlags.NonPublic | BindingFlags.Instance)!
            .MakeGenericMethod(entityType);

        method.Invoke(this, new object[] { builder });
    }

    private void SetSoftDeleteFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, IAuditableEntity
    {
        builder.Entity<TEntity>().HasQueryFilter(e =>
            _filterContext.DisableSoftDeleteFilter || !e.IsDeleted
        );
    }

    private void ApplyTenantFilter(ModelBuilder builder, Type entityType)
    {
        var method = typeof(ApplicationDbContext)
            .GetMethod(nameof(SetTenantFilter),
                BindingFlags.NonPublic | BindingFlags.Instance)!
            .MakeGenericMethod(entityType);

        method.Invoke(this, new object[] { builder });
    }

    private void SetTenantFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, ITenantScoped
    {
        builder.Entity<TEntity>().HasQueryFilter(e =>
            _filterContext.DisableTenantFilter ||
            _tenantProvider.TenantId == Guid.Empty ||
            e.TenantId == _tenantProvider.TenantId
        );
    }

    // ==============================
    // SaveChanges (Auditoría + SoftDelete)
    // ==============================

    /// <summary>
    /// Intercepta SaveChanges para:
    /// - Asignar auditoría en creación y actualización.
    /// - Convertir eliminaciones físicas en Soft Delete.
    /// </summary>
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var userId = _currentUserService.UserId;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not IAuditableEntity auditable)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    auditable.CreatedAt = now;
                    auditable.CreatedBy = userId;
                    auditable.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    auditable.UpdatedAt = now;
                    auditable.UpdatedBy = userId;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    auditable.IsDeleted = true;
                    auditable.DeletedAt = now;
                    auditable.DeletedBy = userId;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}

using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{

    private readonly ITenantProvider _tenantProvider;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    // ==============================
    // DbSets negocio
    // ==============================

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<AppointmentStatus> AppointmentStatuses => Set<AppointmentStatus>();
    public DbSet<WorkingHour> WorkingHours => Set<WorkingHour>();
    public DbSet<Block> Blocks => Set<Block>();

    // ==============================
    // Identity ligero (solo tablas necesarias)
    // ==============================

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<ApplicationRole> Roles => Set<ApplicationRole>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();


    // ==============================
    // ModelCreating
    // ==============================

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        ApplyTenantFilter(modelBuilder);
    }


    // ==============================
    // Multi-tenant global filter
    // ==============================

    private void ApplyTenantFilter(ModelBuilder builder)
    {
        var entityTypes = builder.Model.GetEntityTypes()
            .Where(t => typeof(Domain.Entities.Common.BaseEntity)
            .IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            var method = typeof(ApplicationDbContext)
                .GetMethod(nameof(SetTenantFilter),
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance)!
                .MakeGenericMethod(entityType.ClrType);

            method.Invoke(this, new object[] { builder });
        }

        // aplicar también a Users/Roles
        builder.Entity<ApplicationUser>().HasQueryFilter(e =>
            _tenantProvider.TenantId == Guid.Empty ||
            e.TenantId == _tenantProvider.TenantId);
    }

    private void SetTenantFilter<TEntity>(ModelBuilder builder)
        where TEntity : Domain.Entities.Common.BaseEntity
    {
        builder.Entity<TEntity>()
        .HasQueryFilter(e =>
            _tenantProvider.TenantId == Guid.Empty ||
            e.TenantId == _tenantProvider.TenantId);
    }
}

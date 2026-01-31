
using Application.Common.Rules.Auth;
using Application.Common.Rules.Tenants;
using Application.Common.Rules.Users;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.Models;

namespace Application.Common.Rules
{
    public class RuleFactory
    {

        private readonly IServiceProvider _provider;
        

        public RuleFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        // ===============================
        // Reglas dinámicas (con parámetros)
        // ===============================

        public IRule TenantNameUnique(Tenant tenant)
                => ActivatorUtilities.CreateInstance<TenantNameUniqueRule>(_provider, tenant);

        public IRule TenantPhoneUnique(Tenant tenant)
                => ActivatorUtilities.CreateInstance<TenantPhoneUniqueRule>(_provider, tenant);

        public IRule TenantMustExist(Guid tenantId)
                => ActivatorUtilities.CreateInstance<TenantMustExistRule>(_provider, tenantId);

        public IRule TenantIsActive(Tenant tenant)
                => ActivatorUtilities.CreateInstance<TenantActiveRule>(_provider, tenant);

        // =========================================================
        // USERS
        // =========================================================

       

        public IRule ChangePasswordPermission(ApplicationUser user)
            => ActivatorUtilities.CreateInstance<ChangePasswordPermissionRule>(_provider, user);

        public IRule PasswordNotSame(ApplicationUser user, string newPassword)
            => ActivatorUtilities.CreateInstance<PasswordNotSameRule>(_provider, user, newPassword);

        public UserIsActiveRule UserIsActive(ApplicationUser user)
            => ActivatorUtilities.CreateInstance<UserIsActiveRule>(_provider, user);

        public CanManageUserRule CanManageUser(Guid tenantId)
            => ActivatorUtilities.CreateInstance<CanManageUserRule>(_provider, tenantId);

        public EmailUniqueRule EmailUnique(ApplicationUser user)
            => ActivatorUtilities.CreateInstance<EmailUniqueRule>(_provider, user);

        public RolesMustExistRule RolesMustExist(IReadOnlyCollection<Guid> roles)
            => ActivatorUtilities.CreateInstance<RolesMustExistRule>(_provider, roles);

        public UserMustExistRule UserMustExist(ApplicationUser user)
            => ActivatorUtilities.CreateInstance<UserMustExistRule>(_provider, user);

        public CannotDisableSelfRule CannotDisableSelf(Guid id)
            => ActivatorUtilities.CreateInstance<CannotDisableSelfRule>(_provider, id);

        public CanViewUserRule CanViewUser(ApplicationUser user)
            => ActivatorUtilities.CreateInstance<CanViewUserRule>(_provider, user);


        public UserMustHaveTenantRule UserMustHaveTenant(ApplicationUser user)
            => ActivatorUtilities.CreateInstance<UserMustHaveTenantRule>(_provider, user);

        public UserMustHaveRolesRule UserMustHaveRoles(ApplicationUser user)
            => ActivatorUtilities.CreateInstance<UserMustHaveRolesRule>(_provider, user);

        // =========================================================
        // AUTH
        // =========================================================

        public PasswordMustBeValidRule PasswordMustBeValid(ApplicationUser user, string password)
            => ActivatorUtilities.CreateInstance<PasswordMustBeValidRule>(_provider, user, password);

        //public IRule EmployeeBelongsToTenant(Guid employeeId)
        //    => ActivatorUtilities.CreateInstance<EmployeeBelongsToTenantRule>(
        //        _provider, employeeId);

        //public IRule AppointmentTimeAvailable(DateTime start, DateTime end, Guid employeeId)
        //    => ActivatorUtilities.CreateInstance<AppointmentTimeAvailableRule>(
        //        _provider, start, end, employeeId);
    }
}

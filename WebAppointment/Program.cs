
using Infrastructure.Tenancy;
using WebApi.Modules.Api;
using WebApi.Modules.Persistence;
using WebApi.Modules.Tenancy;
using WebApi.Modules.Auth;
using WebApi.Modules.Core;
using WebApi.Modules.Rules;
using WebApi.Modules.Users;
using WebApi.Modules.Tenants;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiModule()
    .AddPersistenceModule(builder.Configuration)
    .AddTenancyModule()
    .AddAuthModule(builder.Configuration)
    .AddApplicationCoreModule()
    .AddRuleModule()
    .AddTenantModule()
    .AddUserModule();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseTenancy();
app.UseAuthorization();

app.UseApiPipeline();

app.Run();


using Infrastructure.Tenancy;
using WebApi.Modules.Api;
using WebApi.Modules.Auth;
using WebApi.Modules.Core;
using WebApi.Modules.Persistence;
using WebApi.Modules.Rules;
using WebApi.Modules.Tenancy;
using WebApi.Modules.Tenants;
using WebApi.Modules.Users;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios por módulos
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

// Migraciones y seed usando módulo
await app.ApplyMigrationsAndSeedAsync();

// Middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseTenancy();
app.UseAuthorization();

app.UseApiPipeline();

app.Run();


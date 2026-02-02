using Infrastructure.Middleware;
using Infrastructure.Tenancy;
using Serilog;
using WebApi.Modules.Api;
using WebApi.Modules.Auth;
using WebApi.Modules.Core;
using WebApi.Modules.Persistence;
using WebApi.Modules.Rules;
using WebApi.Modules.Tenancy;
using WebApi.Modules.Tenants;
using WebApi.Modules.Users;

var builder = WebApplication.CreateBuilder(args);

// Necesario para HttpContext en middlewares
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// -----------------------------
// Serilog
// -----------------------------

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// -----------------------------
// Módulos
// -----------------------------
builder.Services
    .AddApiModule()
    .AddPersistenceModule(builder.Configuration)
    .AddTenancyModule()
    .AddAuthModule(builder.Configuration)
    .AddApplicationCoreModule()
    .AddRuleModule()
    .AddTenantModule()
    .AddUserModule();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // Requerido por el ExceptionHandler

var app = builder.Build();

// 1. Generamos el ID primero
app.UseMiddleware<TraceIdMiddleware>();

// 2. El manejador de excepciones ahora sí tiene acceso al UserTraceId de arriba
app.UseExceptionHandler();

// 3. El wrapper de respuesta
app.UseMiddleware<ResponseWrapperMiddleware>();

// HTTPS 
app.UseHttpsRedirection();

// Seguridad
app.UseAuthentication();
app.UseAuthorization();

// Tenancy
app.UseTenancy();

// API
app.UseApiPipeline();

// -----------------------------
// Migraciones / Seed
// -----------------------------
await app.ApplyMigrationsAndSeedAsync();

app.Run();

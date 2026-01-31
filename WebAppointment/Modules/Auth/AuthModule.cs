using Application.Interfaces.Infrastructure.Services;
using Domain.Entities.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Modules.Auth
{
    public static class AuthModule
    {
        public static IServiceCollection AddAuthModule(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            var jwtKey = config["Jwt:Key"]!;
            var jwtIssuer = config["Jwt:Issuer"]!;

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            return services;
        }
    }
}

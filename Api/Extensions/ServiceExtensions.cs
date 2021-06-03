using UnaPinta.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnaPinta.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            }); 
        }

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionType = configuration.GetSection("ConnectionType");
            bool.TryParse(connectionType.GetSection("IsAzureConnection").Value, out bool isAzure);
            bool.TryParse(connectionType.GetSection("IsLocalConnection").Value, out bool isLocal);

            if (isAzure)
            {
                services.AddDbContext<UnaPintaDBContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("AzureConnection"))
                );

                return;
            }

            if (isLocal)
            {
                services.AddDbContext<UnaPintaDBContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("LocalConnection"))
                );

                return;
            }

            services.AddDbContext<UnaPintaDBContext>(

                options => options.UseSqlite(configuration.GetConnectionString("SQLiteConnection"))
            );
        }
    }
}

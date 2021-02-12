using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Api.Entities;
using Api.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Sqlite;
using Api.Contracts;
using Api.Extensions;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling 
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddDbContext<UnaPintaDBContext>(
                //Para cambiar a SQL Server reemplazar metodo "UseSqlite" por "UseSqlServer" y cambiar el connection string.
                options => options.UseSqlite(Configuration.GetConnectionString("SQLiteConnection"))
            );
            services.AddAuthentication();
            services.AddIdentity<User, Role>( options => {
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<UnaPintaDBContext>().AddDefaultTokenProviders();
            services.ConfigureJWT(Configuration);
            services.AddScoped<IUnaPintaRepository, SqlUnaPintaRepo>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(
                opt => opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Una Pinta Platform API", Version = "v1"})
            );


            services.AddScoped<IUsersServices, UsersServices>();
            services.AddScoped<IRequestsService, RequestsService>();
            services.AddScoped<IWaitListServices, WaitListServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(
                opt => 
                {
                    opt.RoutePrefix = "";
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Una Pinta Platform API");
                }
            );

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context => {
                    context.Response.Redirect("/swagger/");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }
    }
}

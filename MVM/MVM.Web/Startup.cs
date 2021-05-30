using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MVM.Common.Config;
using MVM.Domain.Services;
using MVM.Infrastructure.Contracts;
using MVM.Infrastructure.DB;
using MVM.Infrastructure.Entities;
using MVM.Infrastructure.Repositories;
using System.Text;

namespace MVM.Web
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
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection("Jwt")["BaseUrl"],
                        ValidAudience = Configuration.GetSection("Jwt")["BaseUrl"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt")["Key"]))
                    };
                }
            );

            services.AddMvc();
            services.AddControllers().AddNewtonsoftJson();

            // Sql Connection
            services.AddDbContext<MVMContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
            });

            services.AddScoped<IGenericRepository<CorrespondencesEntity, CorrespondencesContract, MVMContext>, GenericRepository<CorrespondencesEntity, CorrespondencesContract, MVMContext>>();
            services.AddScoped<IGenericRepository<CorrespondenceTypesEntity, CorrespondenceTypesContract, MVMContext>, GenericRepository<CorrespondenceTypesEntity, CorrespondenceTypesContract, MVMContext>>();
            services.AddScoped<IGenericRepository<LogEntity, LogContract, MVMContext>, GenericRepository<LogEntity, LogContract, MVMContext>>();
            services.AddScoped<IGenericRepository<RolesEntity, RolesContract, MVMContext>, GenericRepository<RolesEntity, RolesContract, MVMContext>>();
            services.AddScoped<IGenericRepository<UsersEntity, UsersContract, MVMContext>, GenericRepository<UsersEntity, UsersContract, MVMContext>>();

            // Adiciona AutoMapper
            services.AddAutoMapper();
            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperConfig>();
            });
            services.AddSingleton(s => mapperConfiguration.CreateMapper());

            // Api Versioning
            services.AddApiVersioning();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/MVM.Web";
            });

            // Services
            services.AddScoped<IDBMmvRepository, DBMmvRepository>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICorrespondenceService, CorrespondenceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

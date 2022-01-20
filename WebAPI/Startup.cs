using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Seguridad;
using WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc.Authorization;
using RiskFirst.Hateoas;
using Aplicacion.Modelos;
using WebAPI.Controllers;
using HateoasNet.DependencyInjection.Core;
using Aplicacion.Seguridad;
using AutoMapper;
using Persistencia.DapperConexion;
using Persistencia.DapperConexion.Instructor;
using Microsoft.OpenApi.Models;
using Persistencia.DapperConexion.Paginacion;

namespace WebAPI
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
            services.AddDbContext<CursoOnlineContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.Configure<ConexionConfiguracion>(Configuration.GetSection("ConnectionStrings"));
            services.AddOptions();

            services.AddMediatR(typeof(Consulta.Manejador).Assembly);
            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));//Pide autorizacion antes del request del cliente
            })
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<CursoOnlineContext>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.ConfigureHateoas(context =>
            {
                return context.Configure<List<UsuarioData>>(members =>
                {
                    members.AddLink("get-members");
                    members.AddLink("DevovlerUsuario");

                });
            });

            services.AddLinks(config =>
            {
                config.AddPolicy<UsuarioData>("FullInfoPolicy", policy =>
                {
                    policy.RequireSelfLink()
                    .RequireRoutedLink("seddlf", "DevovlerUsuario", _ => new { id = _.Direccion.Barrio })
                    .RequireRoutedLink("next", "get-members", _ => new { id = _.Direccion.DireccionCasa })
                    .RequireRoutedLink("previous", "get-members", _ => new { id = _.Direccion });

                });
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false, //Quien puede crear la clave
                        ValidateIssuer = false
                    };
                });

            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.AddScoped<IJwtGenerador, JwtGenerator>();
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            services.AddScoped<IInstructor, InstructorRepositorio>();
            services.AddScoped<IPaginacion, PaginacionRepositorio>();

            services.AddAutoMapper(typeof(Consulta.Manejador));
            services.AddTransient<IFactoryConnection, FactoryConnection>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Servicios para mantenimiento de cursos",
                    Version = "v1"
                });
                c.CustomSchemaIds(c => c.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ManejadorErrorMiddlewar>();
            if (env.IsDevelopment())
            {

                //Middleware por defecto de .netCore
                //app.UseDeveloperExceptionPage();
            }

            //Es para habientes de produccion, ya que se debe usar un certificado ssl
            // app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cursos Online v1");
            });
        }
    }
}

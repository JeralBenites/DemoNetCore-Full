using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sol.TallerNetCore.ApiVentas.Contexto;
using Sol.TallerNetCore.ApiVentas.Dominio.PerfilService;
using Sol.TallerNetCore.ApiVentas.Dominio.UsuarioService;

namespace Sol.TallerNetCore.ApiVentas
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
            services.AddDbContext<BDPedidosContext>(
                (
                    options => {
                        options.UseSqlServer(Configuration.GetConnectionString("CnnPedidos"));
                    }
                ));
            services.AddScoped<IPerfilService, PerfilServiceBD>();
            services.AddScoped<IUsuarioService, UsuarioServiceBD>();

            services.AddCors(
                opcion => {
                    opcion.AddPolicy("PasenTodos", 
                        builder => {
                            builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });

                }
                );

            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    opciones => {
                        opciones.TokenValidationParameters =
                        new TokenValidationParameters()
                        {
                            ValidIssuer = "midominio.com",
                            ValidAudience = "midominio.com",
                            ValidateIssuer = true,
                            ValidateActor = true,
                            ValidateLifetime = true,
                            ValidateAudience = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    Configuration["SemillaJWT"]
                                    )
                                )

                        };

                    }
                )
                ;

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("PasenTodos");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebProov_API.Data;
using WebProov_API.Data.Interfaces;
using System;
using System.Text;

namespace WebProov_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaConfig = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaConfig { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

            //        .AddJwtBearer(x =>
            //        {
            //            x.SaveToken = true;
            //            x.RequireHttpsMetadata = false;
            //            x.TokenValidationParameters = new TokenValidationParameters
            //            {
            //                ValidateIssuer = false,
            //                ValidateAudience = false,
            //                ValidateIssuerSigningKey = true,
            //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //            };
            //        });
#endif
            services.AddControllers();

            services.AddScoped<IBusinessPartnerRepository, BusinessPartnerDIAPI>();
            services.AddScoped<IPedidoRepository, PedidoDIAPI>();
            services.AddScoped<IFacturaRepository, FacturaDIAPI>();
            services.AddScoped<ILoginRepository, LoginDIAPI>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CINEPLANET_API", Version = "v1" });
#if DEBUG
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Description = "JWT Token \r\n\r\n Usar 'Bearer' {token}",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                }
                //            },
                //            new string[] {}
                //    }
                //});
#endif
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CINEPLANET-API WebProveedores v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();
#if DEBUG
            //app.UseAuthentication();
#endif
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
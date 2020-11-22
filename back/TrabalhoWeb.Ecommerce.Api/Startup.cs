using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace TrabalhoWeb.Ecommerce.Api
{
    public class Startup
    {
        readonly string TrabalhoWebEcommerceApiOrigins = "_trabalhoWebEcommerceApiOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: TrabalhoWebEcommerceApiOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:8080",
                                                          "http://localhost:8081",
                                                          "http://localhost:8082",
                                                          "https://hortifruti.giovannacutieri.repl.co")
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod();
                                  });
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "SCC0219 - INTRODUÇÃO AO DESENVOLVIMENTO WEB",
                        Version = "v1",
                        Description = "Grupo 3 - Tema: Hortifruti",
                        Contact = new OpenApiContact
                        {
                            Name = "Grupo 3 - Tema: Hortifruti",
                            Url = new Uri("https://github.com/gicutieri/trab_web/")
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseCors(TrabalhoWebEcommerceApiOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "release/1.0.0");
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using Teste.Application.Service;
using Teste.Infrastructure.Data.Repositories;
using Teste.Infrastructure.IoC;

namespace Teste.Api
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
            var serviceName = "TraceServiceName";
            var serviceVersion = "1.0.0";

            services.AddOpenTelemetryTracing(b =>
            {
                b
                .AddJaegerExporter()
                .AddSource(serviceName)
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation(options => options.SetDbStatementForText = true)
                .AddEntityFrameworkCoreInstrumentation()
                .AddAspNetCoreInstrumentation();
            });
        

            services.AddControllers();
            services.AddMvc();

            services.AddDbContext(Configuration);
            services.AddRepositories();
            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ISaboresRepository, SaboresRepository>();

            services.AddScoped<IPedidoService, PedidoService>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1", Title = "Teste",
                    Description = "Descrição de teste.",
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

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "swagger";
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Example");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Teste.Infrastructure.Data.Contexts;
using Teste.Infrastructure.Data.Repositories;

namespace Teste.Infrastructure.IoC
{
    public static class IoCInfrastructure
    {
        private const string ConnectionStringKey = "CONNECTION_STRING";
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TesteContext>(options =>
                        options.UseLazyLoadingProxies().UseSqlServer(configuration.GetValue<string>(ConnectionStringKey)));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services.AddScoped<IPedidoRepository, PedidoRepository>();    
    }
}

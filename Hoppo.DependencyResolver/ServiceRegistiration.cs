using Hoppo.Business.DatabaseServices;
using Hoppo.Business.Services;
using Hoppo.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;
using TicimaxProductService;

namespace Hoppo.DependencyResolver
{
    public static class ServiceRegistiration
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, DapperDbContext>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUrunServis, UrunServisClient>();
        }

    }
}

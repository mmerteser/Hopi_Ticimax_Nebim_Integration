using Hoppo.Common.Common;
using Hoppo.Common.Contracts;
using Hoppo.Models.DTOs.Product;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hoppo.BackgroundServices
{
    public class SendXmlToHopiService : BackgroundService
    {
        private readonly ILogger<SendXmlToHopiService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public SendXmlToHopiService(IServiceProvider serviceProvider, ILogger<SendXmlToHopiService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        Timer timer;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning("Worker running at: {time}", DateTimeOffset.Now);

                timer = new Timer(GetAsync, state: null, TimeSpan.Zero, TimeSpan.FromMinutes(Configuration.TimerTickFromMinute));

                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        private void GetAsync(object state)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            IProductService _productService = scope.ServiceProvider.GetRequiredService<IProductService>();

            _logger.LogWarning("GetAsync running at: {time}", DateTimeOffset.Now);

            var result = _productService.GetProductsAsync().GetAwaiter().GetResult();

            _logger.LogWarning($"{result.Data.Count()} products were received");

            if (result.Data is not null && result.Data.Any())
                SendAsync(result.Data).GetAwaiter().GetResult();
        }

        private async Task SendAsync(IEnumerable<Item> products)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            IProductService _productService = scope.ServiceProvider.GetRequiredService<IProductService>();

            _logger.LogWarning("CreateXml running at: {time}", DateTimeOffset.Now);

            var result = _productService.CreateItemXml(products).GetAwaiter().GetResult();

            _logger.LogWarning($"Xml oluşturuldu");
        }
    }
}
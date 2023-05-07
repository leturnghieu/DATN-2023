using DATN.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN.Services.Service
{
    public class ProductStatusService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<ProductStatusService> _logger;
        private Timer _timer;

        public ProductStatusService(IServiceProvider services, ILogger<ProductStatusService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product status worker is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            await Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Product status worker is working.");

            using var scope = _services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MasterDbContext>();

            var products = dbContext.NhatKyMuaSams.ToList();

            foreach (var product in products)
            {
                if(product.HanSuDung < DateTime.Now)
                {
                    product.TrangThai = "Hết hạn";
                }
                else
                {
                    product.TrangThai = "Còn hạn";
                }
                
            }
            dbContext.SaveChanges();
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Product status worker is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            await base.StopAsync(stoppingToken);
        }
    }
}

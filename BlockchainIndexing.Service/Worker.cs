using BlockchainIndexing.Application.Services.Interfaces;

namespace BlockchainIndexing.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    int fromBlock = _configuration.GetValue<int>("FromBlock");
                    int toBlock = _configuration.GetValue<int>("ToBlock");

                    using var scope = _scopeFactory.CreateScope();
                    var indexingService = scope.ServiceProvider.GetService<IIndexingService>();
                    await indexingService.Process(fromBlock, toBlock);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing service: ");
                }
                finally
                {
                    _logger.LogInformation("Stopping service...");
                    await StopAsync(stoppingToken);
                }
            }
        }
    }
}
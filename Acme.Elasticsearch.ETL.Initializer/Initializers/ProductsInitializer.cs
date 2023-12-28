namespace Acme.Elasticsearch.ETL.Initializer.Initializers;

public class ProductsInitializer : BackgroundService
{
    private readonly ILogger<ProductsInitializer> _logger;

    public ProductsInitializer(ILogger<ProductsInitializer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
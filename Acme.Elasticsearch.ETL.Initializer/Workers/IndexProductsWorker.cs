namespace Acme.Elasticsearch.ETL.Initializer.Workers;

public class IndexProductsWorker : BackgroundService
{
    private readonly ILogger<IndexProductsWorker> _logger;

    public IndexProductsWorker(ILogger<IndexProductsWorker> logger)
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
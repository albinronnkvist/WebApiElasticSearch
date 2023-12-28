using Acme.Elasticsearch.ETL.Initializer;
using Acme.Elasticsearch.ETL.Initializer.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<IndexProductsWorker>();
        services.ConfigureElasticsearch(hostContext.Configuration);
    })
    .Build();

host.Run();
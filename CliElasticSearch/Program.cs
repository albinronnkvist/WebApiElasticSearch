using Acme.Elasticsearch.Cli;
using Acme.Elasticsearch.Cli.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<IndexProductsWorker>();
        services.ConfigureElasticsearch(hostContext.Configuration);
    })
    .Build();

host.Run();
using Acme.Elasticsearch.ETL.Initializer;
using Acme.Elasticsearch.ETL.Initializer.Initializers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<ProductsInitializer>();
        services.ConfigureElasticsearch(hostContext.Configuration);
    })
    .Build();

host.Run();
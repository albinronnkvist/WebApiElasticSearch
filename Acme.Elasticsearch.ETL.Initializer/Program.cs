using Acme.Elasticsearch.ETL.Initializer;
using Acme.Elasticsearch.ETL.Initializer.Initializers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<ProductsInitializer>();
        services.ConfigureOptionsPattern(hostContext.Configuration);
        services.ConfigureDatabase();
        services.ConfigureElasticsearch(hostContext.Configuration);
        services.ConfigureElasticsearchServices();
    })
    .Build();

host.Run();
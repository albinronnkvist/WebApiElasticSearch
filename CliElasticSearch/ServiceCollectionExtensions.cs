using Acme.Elasticsearch.Cli.Options;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace Acme.Elasticsearch.Cli;

public static class ServiceCollectionExtensions
{
    public static void ConfigureElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ElasticsearchOptions>(
            configuration.GetSection(nameof(ElasticsearchOptions)));

        var elasticsearchOptions = configuration.GetSection(nameof(ElasticsearchOptions))
            .Get<ElasticsearchOptions>();
        ArgumentNullException.ThrowIfNull(elasticsearchOptions);

        var elasticSearchSettings = new ElasticsearchClientSettings(new Uri(elasticsearchOptions.Url))
            .CertificateFingerprint(elasticsearchOptions.FingerPrint)
            .Authentication(new BasicAuthentication(elasticsearchOptions.Username, elasticsearchOptions.Password));

        var elasticSearchClient = new ElasticsearchClient(elasticSearchSettings);

        services.AddSingleton(elasticSearchClient);
    }
}

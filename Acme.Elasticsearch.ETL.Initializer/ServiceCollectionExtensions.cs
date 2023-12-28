using Acme.Elasticsearch.Core.Options;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace Acme.Elasticsearch.ETL.Initializer;

public static class ServiceCollectionExtensions
{
    public static void ConfigureElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var elasticsearchOptions = configuration.GetSection(nameof(ElasticsearchOptions))
            .Get<ElasticsearchOptions>() ?? throw new ArgumentNullException(nameof(ElasticsearchOptions), "Elasticsearch options are not configured properly");

        var elasticSearchSettings = new ElasticsearchClientSettings(new Uri(elasticsearchOptions.Url))
            .CertificateFingerprint(elasticsearchOptions.FingerPrint)
            .Authentication(new BasicAuthentication(elasticsearchOptions.Username, elasticsearchOptions.Password));

        services.AddSingleton(new ElasticsearchClient(elasticSearchSettings));
    }
}

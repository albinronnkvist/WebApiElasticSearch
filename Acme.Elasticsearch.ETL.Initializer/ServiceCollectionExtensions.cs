using Acme.Elasticsearch.Core.Options;
using Acme.Elasticsearch.ETL.Initializer.IndexTemplates;
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

    public static void ConfigureElasticsearchServices(this IServiceCollection services)
    {
        services.AddTransient<IIndexTemplateService, IndexTemplateService>();
    }
}

using CSharpFunctionalExtensions;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace Acme.Elasticsearch.ETL.Initializer.IndexTemplates;

public class IndexTemplateService : IIndexTemplateService
{
    private readonly ElasticsearchClient _elasticsearchClient;

    public IndexTemplateService(ElasticsearchClient elasticsearchClient)
    {
        _elasticsearchClient = elasticsearchClient;
    }
    
    public async Task<UnitResult<string>> UpsertIndexTemplate(PutIndexTemplateRequest request)
    {
        var response = await _elasticsearchClient.Indices.PutIndexTemplateAsync(request);
        if (!response.IsValidResponse)
        {
            return UnitResult.Failure(response.DebugInformation);
        }

        return UnitResult.Success<string>();
    }
}
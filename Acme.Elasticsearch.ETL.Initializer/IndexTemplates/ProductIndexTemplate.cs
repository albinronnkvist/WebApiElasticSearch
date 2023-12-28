using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace Acme.Elasticsearch.ETL.Initializer.IndexTemplates;

public class ProductIndexTemplate
{
    private readonly ElasticsearchClient _elasticsearchClient;

    public ProductIndexTemplate(ElasticsearchClient elasticsearchClient)
    {
        _elasticsearchClient = elasticsearchClient;
    }
    
    public async Task<PutIndexTemplateResponse> Update()
    {
        return await _elasticsearchClient.Indices.PutIndexTemplateAsync("products", it => it
            .IndexPatterns("products-*")
            .Template(x => x
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                )
            ));
    }
}
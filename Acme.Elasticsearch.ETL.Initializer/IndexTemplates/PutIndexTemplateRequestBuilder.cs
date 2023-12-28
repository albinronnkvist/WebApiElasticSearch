using Acme.Elasticsearch.Core.Constants.Indices;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;

namespace Acme.Elasticsearch.ETL.Initializer.IndexTemplates;

public class PutIndexTemplateRequestBuilder
{
    private readonly PutIndexTemplateRequest _templateRequest;
    private readonly IndexConstantsBase _indexDetails;
    
    public PutIndexTemplateRequestBuilder(IndexConstantsBase indexDetails, int version)
    {
        _indexDetails = indexDetails;
        _templateRequest = new PutIndexTemplateRequest(_indexDetails.GetTemplateName())
        {
            IndexPatterns = new[] { _indexDetails.GetTemplatePattern() },
            Template = new IndexTemplateMapping
            {
                Aliases = new Dictionary<IndexName, Alias>
                {
                    { _indexDetails.GetVersionedAlias(version), new Alias() }
                },
                Mappings = new TypeMapping
                {
                    Dynamic = DynamicMapping.True
                },
                Settings = new IndexSettings
                {
                    AutoExpandReplicas = "1-5",
                    NumberOfShards = 3,
                    NumberOfReplicas = 1
                }
            },
            Version = version
        };
    }

    public PutIndexTemplateRequestBuilder WithSearchAlias()
    {
        _templateRequest.Template?.Aliases?.Add(_indexDetails.GetSearchAlias(), new Alias() );
        return this;
    }
    
    public PutIndexTemplateRequest Build()
    {
        return _templateRequest;
    }
}
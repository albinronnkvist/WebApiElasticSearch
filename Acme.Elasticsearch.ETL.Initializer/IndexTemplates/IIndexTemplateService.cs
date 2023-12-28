using CSharpFunctionalExtensions;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace Acme.Elasticsearch.ETL.Initializer.IndexTemplates;

public interface IIndexTemplateService
{
    Task<UnitResult<string>> UpsertIndexTemplate(PutIndexTemplateRequest request);
}
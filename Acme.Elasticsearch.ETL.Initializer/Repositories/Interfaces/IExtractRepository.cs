using Acme.Elasticsearch.ETL.Initializer.Records;

namespace Acme.Elasticsearch.ETL.Initializer.Repositories.Interfaces;

public interface IExtractRepository<T, TCursor>
{
    Task<IEnumerable<T>> Get(KeysetPagination<TCursor> pagination);
}
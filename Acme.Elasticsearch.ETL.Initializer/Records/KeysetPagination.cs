namespace Acme.Elasticsearch.ETL.Initializer.Records;

public record KeysetPagination<TCursor>
{
    public required TCursor Cursor { get; init; }
    public required int BatchSize { get; init; }
}
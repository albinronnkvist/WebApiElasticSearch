namespace Acme.Elasticsearch.Core.Options;

public record ElasticsearchOptions
{
    public required string Url { get; init; }
    public required string FingerPrint { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}
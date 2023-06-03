namespace Acme.Elasticsearch.Web.Dtos;

public record UpdateProduct
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
}

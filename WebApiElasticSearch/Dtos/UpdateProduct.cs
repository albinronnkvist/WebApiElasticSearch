namespace Acme.Elasticsearch.Web.Dtos;

public record CreateProduct
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required decimal Price { get; init; }
}

using Acme.Elasticsearch.Api.Dtos;
using Acme.Elasticsearch.Api.Entities;

namespace Acme.Elasticsearch.Api.Mappers;

public static class ProductMapper
{
    public static ReadProduct MapToReadProduct(Product product)
        => new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };

    public static IReadOnlyCollection<ReadProduct> MapToReadProducts(IReadOnlyCollection<Product> products)
        => products.Select(x => new ReadProduct
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price
            }).ToList().AsReadOnly();

    public static Product MapCreateToProduct(CreateProduct createProduct)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = createProduct.Name,
            Description = createProduct.Description,
            Price = createProduct.Price
        };

    public static Product MapUpdateToProduct(Guid id, UpdateProduct updateProduct)
        => new()
        {
            Id = id,
            Name = updateProduct.Name,
            Description = updateProduct.Description,
            Price = updateProduct.Price
        };
}

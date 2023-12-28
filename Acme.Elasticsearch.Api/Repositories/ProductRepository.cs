using Acme.Elasticsearch.Api.Entities;
using Elastic.Clients.Elasticsearch;
using OneOf;
using OneOf.Types;
using Sieve.Models;

namespace Acme.Elasticsearch.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ElasticsearchClient _elasticsearchClient;
    private const string ProductIndex = "product";

    public ProductRepository(ElasticsearchClient elasticsearchClient)
    {
        _elasticsearchClient = elasticsearchClient;
    }



    public async Task<OneOf<Product, NotFound, Error>> GetAsync(Guid id)
    {
        var response = await _elasticsearchClient.GetAsync<Product>(id, idx => idx.Index(ProductIndex));
        if (!response.IsValidResponse)
        {
            if (!response.Found)
            {
                return new NotFound();
            }

            return new Error();
        }

        return response.Source is null 
            ? new NotFound()
            : response.Source;
    }

    public async Task<OneOf<IReadOnlyCollection<Product>, Error>> SearchAsync(SieveModel sieveModel)
    {
        // TODO: add more filtering logic. Shouldn't pass SieveModel either for less dependance

        var searchDescriptor = new SearchRequestDescriptor<Product>()
            .Index(ProductIndex)
            .From(sieveModel.Page ?? 0)
            .Size(sieveModel.PageSize ?? 10);

        var response = await _elasticsearchClient.SearchAsync(searchDescriptor);
        if (!response.IsValidResponse)
        {
            return new Error();
        }

        return OneOf<IReadOnlyCollection<Product>, Error>.FromT0(response.Documents);
    }

    public async Task<OneOf<Success, Error>> CreateAsync(Product product)
    {
        var response = await _elasticsearchClient.IndexAsync(product, ProductIndex);
        if (!response.IsValidResponse)
        {
            return new Error();
        }

        return new Success();
    }

    public async Task<OneOf<Success, NotFound, Error>> UpdateAsync(Product product)
    {
        var response = await _elasticsearchClient.UpdateAsync<Product, Product>(ProductIndex, 
            product.Id,
            u => u.Doc(product));

        if (!response.IsValidResponse)
        {
            if (response.Result == Result.NotFound)
            {
                return new NotFound();
            }

            return new Error();
        }

        return new Success();
    }

    public async Task<OneOf<Success, NotFound, Error>> DeleteAsync(Guid id)
    {
        var response = await _elasticsearchClient.DeleteAsync(ProductIndex, id);
        if (!response.IsValidResponse)
        {
            if (response.Result == Result.NotFound)
            {
                return new NotFound();
            }

            return new Error();
        }

        return new Success();
    }
}

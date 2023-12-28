using Acme.Elasticsearch.Api.Dtos;
using Acme.Elasticsearch.Api.Mappers;
using Acme.Elasticsearch.Api.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Acme.Elasticsearch.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IValidator<CreateProduct> _createProductValidator;
    private readonly IValidator<UpdateProduct> _updateProductValidator;
    private readonly IProductRepository _productRepository;

    public ProductsController(IValidator<CreateProduct> createProductValidator,
        IValidator<UpdateProduct> updateProductValidator,
        IProductRepository productRepository)
    {
        _createProductValidator = createProductValidator;
        _updateProductValidator = updateProductValidator;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SieveModel sieveModel)
    {
        var searchProductsResult = await _productRepository.SearchAsync(sieveModel);

        return searchProductsResult.Match<IActionResult>(
            products => Ok(ProductMapper.MapToReadProducts(products)),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );
    }

    [HttpGet("{id}", Name = nameof(GetById))]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var getProductResult = await _productRepository.GetAsync(id);

        return getProductResult.Match<IActionResult>(
            product => Ok(ProductMapper.MapToReadProduct(product)),
            notFound => NotFound(),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProduct createProductDto)
    {
        var validationResult = await _createProductValidator.ValidateAsync(createProductDto);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        var newProduct = ProductMapper.MapCreateToProduct(createProductDto);

        var createProductResult = await _productRepository.CreateAsync(newProduct);
        return createProductResult.Match<IActionResult>(
            success => CreatedAtRoute(nameof(GetById), new { newProduct.Id }, newProduct),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProduct updateProductDto)
    {
        var validationResult = await _updateProductValidator.ValidateAsync(updateProductDto);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return BadRequest(ModelState);
        }

        var updatedProduct = ProductMapper.MapUpdateToProduct(id, updateProductDto);

        var updateProductResult = await _productRepository.UpdateAsync(updatedProduct);
        return updateProductResult.Match<IActionResult>(
            success => NoContent(),
            notFound => NotFound(),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleteProductResult = await _productRepository.DeleteAsync(id);
        return deleteProductResult.Match<IActionResult>(
            success => NoContent(),
            notFound => NotFound(),
            error => StatusCode(StatusCodes.Status500InternalServerError)
        );
    }
}

using Acme.Elasticsearch.Api;
using Acme.Elasticsearch.Api.Dtos;
using Acme.Elasticsearch.Api.Repositories;
using Acme.Elasticsearch.Api.Validators;
using FluentValidation;
using Sieve.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureElasticsearch(builder.Configuration);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<SieveProcessor>();
builder.Services.AddScoped<IValidator<CreateProduct>, CreateProductValidator>();
builder.Services.AddScoped<IValidator<UpdateProduct>, UpdateProductValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Acme.Elasticsearch.Web;
using Acme.Elasticsearch.Web.Dtos;
using Acme.Elasticsearch.Web.Repositories;
using Acme.Elasticsearch.Web.Validators;
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

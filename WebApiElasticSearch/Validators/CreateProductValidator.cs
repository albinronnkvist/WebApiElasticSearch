﻿using Acme.Elasticsearch.Web.Dtos;
using FluentValidation;

namespace Acme.Elasticsearch.Web.Validators;

public class CreateProductValidator : AbstractValidator<CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 50);

        RuleFor(x => x.Description)
            .Length(0, 400);

        RuleFor(x => x.Price)
            .NotNull()
            .InclusiveBetween(0, decimal.MaxValue);
    }
}

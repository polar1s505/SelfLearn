using FluentValidation;

namespace backend.Application.DTOs.Stock.Validators
{
    public class CreateStockDTOValidator : AbstractValidator<CreateStockDTO>
    {
        public CreateStockDTOValidator()
        {
            RuleFor(s => s.Symbol)
                .NotEmpty().WithMessage("Symbol cannot be empty")
                .MaximumLength(10).WithMessage("Symbol cannot be over 10 over characters");

            RuleFor(s => s.CompanyName)
                .NotEmpty().WithMessage("Company name cannot be empty")
                .MaximumLength(10).WithMessage("Company name cannot be over 10 over characters");

            RuleFor(s => s.Purchase)
                .NotEmpty().WithMessage("Purchase cannot be empty")
                .InclusiveBetween(1, 1000000000);

            RuleFor(s => s.LastDiv)
                .NotEmpty().WithMessage("Last divident cannot be empty")
                .InclusiveBetween(0.001m, 100);

            RuleFor(s => s.Industry)
                .NotEmpty().WithMessage("Industry cannot be empty")
                .MaximumLength(10).WithMessage("Industry cannot be over 10 over characters");

            RuleFor(s => s.MarketCap)
                .NotEmpty().WithMessage("Market Cap cannot be empty")
                .InclusiveBetween(1, 5000000000);
        }
    }
}

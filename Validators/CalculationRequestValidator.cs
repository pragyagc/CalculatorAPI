using FluentValidation;
using CalculatorAPI.Models;

namespace CalculatorAPI.Validators
{
    public class CalculationRequestValidator : AbstractValidator<CalculationRequest>
    {
        public CalculationRequestValidator()
        {
                RuleFor(x => x.Operation)
                .NotEmpty().WithMessage("Operation is required")
                .Must(op => new[] { "add", "subtract", "multiply", "divide" }
                .Contains(op.ToLower()))
                .WithMessage("Operation must be add, subtract, multiply, or divide");

            RuleFor(x => x.Operation)
                .NotEqual("divide").When(x => x.B == 0)
                .WithMessage("Division by zero is not allowed");

        }
    }
}

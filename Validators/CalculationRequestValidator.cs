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

            RuleFor(x => x.B)
                .NotEqual(0).When(x => x.Operation != null && x.Operation.ToLower() == "divide")
                .WithMessage("Cannot divide by zero");
        }
    }
}

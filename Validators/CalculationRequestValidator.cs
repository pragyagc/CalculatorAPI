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
                .Must(op => new[] { "+", "-", "*", "/" }.Contains(op))
                .WithMessage("Operation must be +, -, * or /");


            RuleFor(x => x.Operation)
                .NotEqual("divide").When(x => x.B == 0)
                .WithMessage("Division by zero is not allowed");

            RuleFor(x => x.A)
                .NotEmpty().WithMessage("provide me a value for operand A");


            RuleFor(x => x.B)
                .NotEmpty().WithMessage("provide me a value for operand B");
        }
    }
}

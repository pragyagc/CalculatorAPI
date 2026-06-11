using CalculatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using CalculatorAPI.Models;

using FluentValidation;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IValidator<CalculationRequest> _validator;
        private readonly Addition _addition;
        private readonly Subtract _subtract;
        private readonly Multiply _multiply;
        private readonly Divide _divide;

        public CalculatorController(
            IValidator<CalculationRequest> validator,
            Addition addition,
            Subtract subtract,
            Multiply multiply,
            Divide divide)
        {
            _validator = validator;
            _addition = addition;
            _subtract = subtract;
            _multiply = multiply;
            _divide = divide;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(CalculationRequest request)
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            double output = request.Operation.ToLower() switch
            {
                "add" => _addition.Calculate(request.A, request.B),
                "subtract" => _subtract.Calculate(request.A, request.B),
                "multiply" => _multiply.Calculate(request.A, request.B),
                "divide" => _divide.Calculate(request.A, request.B),
                _ => throw new InvalidOperationException("Invalid operation")
            };

            return Ok(output);
        }
    }
}

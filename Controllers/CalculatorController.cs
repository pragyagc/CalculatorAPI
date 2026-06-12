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
        private readonly OperationFactory _factory;

        public CalculatorController(IValidator<CalculationRequest> validator, OperationFactory factory)
        {
            _validator = validator;
            _factory = factory;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalculationRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var operation = _factory.GetOperation(request.Operation);
                var result = operation.Calculate(request.A, request.B); 

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

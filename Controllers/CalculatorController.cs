using CalculatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using CalculatorAPI.Models;
using FluentValidation;
using CalculatorAPI.Data;
using Microsoft.EntityFrameworkCore;


namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CalculatorController : ControllerBase
    {
        private readonly IValidator<CalculationRequest> _validator;
        private readonly OperationFactory _factory;
        private readonly IRepository<Calculation> _calculationRepo;
        private readonly CalculatorDbContext _context;

        public CalculatorController(IValidator<CalculationRequest> validator,OperationFactory factory,IRepository<Calculation> calculationRepo, CalculatorDbContext context) // constructor injection
        {
            _validator = validator;
            _factory = factory;
            _calculationRepo = calculationRepo;
            _context = context;
        }

        [HttpGet("History")]
        public async Task<ActionResult<IEnumerable<Calculation>>> GetAll()
        {
            var calculations = await _context.Calculations.ToListAsync();
            return Ok(calculations);
        }

        


        [HttpPost("calculate")]
        // we have async task as we are using await in the code down
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
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

                var operationEntity = await _context.Operations
             .FirstOrDefaultAsync(o => o.Symbol == request.Operation);

                if (operationEntity == null)
                {
                    return BadRequest($"Operation '{request.Operation}' not found in database.");
                }

                // Save to database using repository
                var calculation = new Calculation
                {
                    OperandA = request.A,
                    Operator = request.Operation,
                    OperandB = request.B,
                    Result = result,
                    OperationId = operationEntity.Id,
                    Operation=operationEntity// or map to actual Operation entity
                };

                await _calculationRepo.AddAsync(calculation);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}

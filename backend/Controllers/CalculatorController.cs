using CalculatorAPI.Models;
using CalculatorAPI.Services;
using CalculatorAPI.Data;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IValidator<CalculationRequest> _validator;
        private readonly OperationFactory _factory;
        private readonly CalculatorDbContext _context;
        private readonly IMemoryCache _cache;

        public CalculatorController(
            IValidator<CalculationRequest> validator,
            OperationFactory factory,
            CalculatorDbContext context,
            IMemoryCache cache)
        {
            _validator = validator;
            _factory = factory;
            _context = context;
            _cache = cache;
        }

        // =========================
        // SESSION HELPER
        // =========================
        private Guid? GetSessionId()
        {
            var sessionStr = Request.Cookies["calc_session"];

            if (Guid.TryParse(sessionStr, out var guid))
                return guid;

            return null;
        }


        private string GetSessionKey(Guid sessionId)
        {
            return $"calc_session_{sessionId}";
        }

        // =========================
        // NEW SESSION
        // =========================
        [HttpPost("new-session")]
        public IActionResult NewSession()
        {
            var sessionId = Guid.NewGuid();

            Response.Cookies.Append("calc_session", sessionId.ToString(), new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new
            {
                message = "Session created",
                sessionId
            });
        }

        // =========================
        // CALCULATE
        // =========================
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var sessionId = GetSessionId();
            if (sessionId == null)
                return BadRequest("Session not found. Call /new-session first.");

            var operationEntity = await _context.Operations
                .FirstOrDefaultAsync(o => o.Symbol == request.Operation);

            if (operationEntity == null)
                return BadRequest("Invalid operation");

            var operation = _factory.GetOperation(request.Operation);
            var result = operation.Calculate(request.A, request.B);

            var calculation = new Calculation
            {
                OperandA = request.A,
                OperandB = request.B,
                Operator = request.Operation,
                Result = result,
                OperationId = operationEntity.Id,
                SessionId = sessionId.Value
            };

            await _context.Calculations.AddAsync(calculation);
            await _context.SaveChangesAsync();

            //save to cache
            var key = GetSessionKey(sessionId.Value);

            var history = _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2);
                return new List<Calculation>();
            });

            history.Add(calculation);

            return Ok(new { result });
        }

        // =========================
        // HISTORY (SESSION BASED)
        // =========================
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(int page = 1, int pageSize = 2)
        {
            var sessionId = GetSessionId();
            if (sessionId == null)
                return BadRequest("Session not found");

            var key = GetSessionKey(sessionId.Value);

            if (!_cache.TryGetValue(key, out List<Calculation>? history))
                return Ok(new { items = new List<object>(), totalCount = 0 });


            var items = history.Select(h => new
            {
                h.OperandA,
                h.Operator,
                h.OperandB,
                h.Result
            });

            return Ok(new { items, totalCount = history.Count });
        }
    }
}
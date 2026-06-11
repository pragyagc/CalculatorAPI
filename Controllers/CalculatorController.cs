using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly Addition _addition;
    private readonly Subtract _subtract;
    private readonly Multiply _multiply;
    private readonly Divide _divide;

    // ASP.NET Core will inject these automatically because you registered them in Program.cs
    public CalculatorController(Addition addition, Subtract subtract, Multiply multiply, Divide divide)
    {
        _addition = addition;
        _subtract = subtract;
        _multiply = multiply;
        _divide = divide;
    }

    [HttpGet("add")]
    public IActionResult Add(double a, double b)
    {
        return Ok(_addition.Calculate(a, b));
    }

    [HttpGet("subtract")]
    public IActionResult Subtract(double a, double b)
    {
        return Ok(_subtract.Calculate(a, b));
    }

    [HttpGet("multiply")]
    public IActionResult Multiply(double a, double b)
    {
        return Ok(_multiply.Calculate(a, b));
    }

    [HttpGet("divide")]
    public IActionResult Divide(double a, double b)
    {
        try
        {
            return Ok(_divide.Calculate(a, b));
        }
        catch (DivideByZeroException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

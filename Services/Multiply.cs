namespace CalculatorAPI.Services
{
    public class Multiply : IOperation
    {
        public double Calculate(double a, double b) => a * b;
    }
}

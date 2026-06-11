namespace CalculatorAPI.Services
{
    public class Addition : IOperation
    {
        public double Calculate(double a, double b) => a + b;
    }
}

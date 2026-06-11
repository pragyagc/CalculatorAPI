namespace CalculatorAPI.Services
{
    public class Subtract : IOperation
    {
        public double Calculate(double a, double b) => a - b;
    }
}

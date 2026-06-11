namespace CalculatorAPI.Services
{
    public class Divide : IOperation
    {
        public double Calculate(double a, double b) => a / b; // validator handles divide-by-zero
    }
}

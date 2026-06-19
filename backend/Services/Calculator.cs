namespace CalculatorAPI.Services
{
    public class Calculator
    {
        private readonly IOperation _operation;

        public Calculator(IOperation operation)
        {
            _operation = operation;
        }

        public double Calculate(double a, double b)
        {
            return _operation.Calculate(a, b);
        }
    }
}

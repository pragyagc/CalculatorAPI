namespace CalculatorAPI.Services
{
    public class Divide : IOperation
    {
        private double result_div;
       
        public double Calculate(double a, double b)
        {
            result_div = a / b;
            return result_div;
        }
    }
}

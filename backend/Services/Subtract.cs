namespace CalculatorAPI.Services
{
    public class Subtract : IOperation
    {
        private double result_sub;
        
        public double Calculate(double a, double b)
        {

            result_sub = a - b;
            return result_sub;
        }
    }
}
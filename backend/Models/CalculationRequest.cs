namespace CalculatorAPI.Models
{
    public class CalculationRequest
    {
        public double A { get; set; }
        public double B { get; set; }
        public string Operation { get; set; } = string.Empty; 
    }
}

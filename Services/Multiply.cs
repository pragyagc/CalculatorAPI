 public class Multiply : IOperation
{
    private double result_mul;
    public  double Calculate(double a ,double b)
    {
        result_mul= a * b;
        return result_mul;
    }
}
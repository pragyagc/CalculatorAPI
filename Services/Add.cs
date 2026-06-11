public class Addition : IOperation
{ private double result_add;
    public double Calculate(double a,double b)
    {
        result_add= a + b;
        return result_add;
    }
}
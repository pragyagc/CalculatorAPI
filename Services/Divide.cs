public class Divide: IOperation
{
    private double result_div;
    public double Calculate(double a,double b )
    {
        if( b==0)
        {
            throw new DivideByZeroException(" cannot divide by zero.");
        }
        result_div= a / b;
        return result_div;
    }
}
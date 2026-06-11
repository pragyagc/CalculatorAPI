public class  Calculator
{
    private readonly IOperation operation;
    public Calculator(IOperation operation) //DI injected via constructor
    {
        this.operation = operation;
    }

    public  double Calculate (double a ,double b)
     {
        return  operation.Calculate(a,b);
     }
}
using System;

namespace CalculatorAPI.Services
{
    public class OperationFactory
    {
        public IOperation GetOperation(string operationName)
        {
            return operationName.ToLower() switch
            {
                "+" => new Addition(),
                "-" => new Subtract(),
                "*" => new Multiply(),
                "/" => new Divide(),
                _ => throw new InvalidOperationException($"Unknown operation: {operationName}")
            };
        }
    }
}

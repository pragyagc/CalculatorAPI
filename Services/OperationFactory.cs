using System;

namespace CalculatorAPI.Services
{
    public class OperationFactory
    {
        public IOperation GetOperation(string operationName)
        {
            return operationName.ToLower() switch
            {
                "add" => new Addition(),
                "subtract" => new Subtract(),
                "multiply" => new Multiply(),
                "divide" => new Divide(),
                _ => throw new InvalidOperationException($"Unknown operation: {operationName}")
            };
        }
    }
}

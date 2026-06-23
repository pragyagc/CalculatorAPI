using System.Text.Json.Serialization;
namespace CalculatorAPI.Models
{
    public class Calculation
    {
        public int Id { get; set; }                    
        public double OperandA { get; set; }            
        public string Operator { get; set; } = string.Empty; 
        public double OperandB { get; set; }            
        public double Result { get; set; }              
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid SessionId { get; set; }
        public int OperationId { get; set; }

        [JsonIgnore]
        public Operation Operation { get; set; } = default!;// navigation to operation entity

        

    }
}

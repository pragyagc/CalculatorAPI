using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorAPI.Models
{
    //[Table("operations")]
    public class Operation
    {
       
        public int Id { get; set; }  

       
        public string Symbol { get; set; } = "";
       
        public string Name { get; set; } = "";   

        public ICollection<Calculation>? Calculations { get; set; }
    }
}

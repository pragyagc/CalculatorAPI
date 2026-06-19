using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorAPI.Models
{
    //[Table("operations")]
    public class Operation
    {
        //[Key]
        //[Column("id")]
        public int Id { get; set; }  // Primary key

        //[Column("symbol")]
        public string Symbol { get; set; } = "";
        //[Column("name")]
        public string Name { get; set; } = "";   

        public ICollection<Calculation>? Calculations { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class Request : Entity 
    {
        public User? User { get; set; }
        public Media? Media { get; set; }

        [Column("answer", TypeName = "varchar(10)")]
        public string? Answer { get; set; }

        [Column("date", TypeName = "date")]
        public DateOnly Date { get; set; }
    }
}
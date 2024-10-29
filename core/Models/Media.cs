using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class Media : Entity 
    {
        public User? User { get; set; }

        [Column("content", TypeName = "decimal(5, 4)")]
        public byte[]? Content { get; set; }
    }
}
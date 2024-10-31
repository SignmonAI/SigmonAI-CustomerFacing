using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class Media : Entity 
    {

        [Column("content", TypeName = "varbinary(MAX)")]
        public byte[]? Content { get; set; }
    }
}
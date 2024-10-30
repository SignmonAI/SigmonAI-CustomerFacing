using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class Language : Entity
    {
        [Column("name", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        public Country? Country { get; set; }
    }
}
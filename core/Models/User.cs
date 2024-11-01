using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class User : Entity
    {
        [Column("name", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [Column("email", TypeName = "varchar(255)")]
        public string? Email { get; set; }

        [Column("phone", TypeName = "varchar(17)")]
        public string? Phone { get; set; }

        [Column("password", TypeName = "varchar(255)")]
        public string? Password { get; set; }

        public Country? Country { get; set; }
        public Subscription? Subscription { get; set; }
    }
}
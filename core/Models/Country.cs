using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;
using Microsoft.EntityFrameworkCore;

namespace core.Models
{
    public class Country : Entity
    {
        [Column("name", TypeName = "varchar(255)")]
        public string? Name { get; set; }

        [Column("phone_code", TypeName = "varchar(3)")]
        public string? PhoneCode { get; set; }
    }
}
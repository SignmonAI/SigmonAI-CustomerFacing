using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace core.Models
{
    public class Tier : Entity
    {
        [Column("model_desctiption", TypeName = "varchar(255)")]
        public string ModelDescription { get; set; }

        [Column("model_number", TypeName = "tinyint")]
        Int16 ModelNumber { get; set; }

        [Column("base_pricing", TypeName = "decimal(5, 4)")]
        float BasePricing { get; set; }
    }
}
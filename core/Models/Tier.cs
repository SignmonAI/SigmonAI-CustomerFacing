using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace core.Models
{
    public enum ClassificationModel
    {
        FREE = 1,
        INTERMEDIATE,
        ADVANCED,
    }

    public class Tier : Entity
    {
        [Column("model_desctiption", TypeName = "varchar(255)")]
        public string ModelDescription { get; set; } = string.Empty;

        [Column("model_number", TypeName = "tinyint")]
        public ClassificationModel ModelNumber { get; set; }
    }
}
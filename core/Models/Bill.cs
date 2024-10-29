using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class Bill : Entity 
    {
        public Subscription? Subscription { get; set; }

        [Column("expiral_date", TypeName = "date")]
        public DateOnly? ExpiralDate { get; set; }

        [Column("payment_date", TypeName = "date")]
        public DateOnly? PaymentDate { get; set; }

        [Column("payment_due", TypeName = "decimal(5, 4)")]
        public double PaymentDue { get; set; }
    }
}
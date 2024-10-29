using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class Subscription : Entity 
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Column("payment_due", TypeName = "decimal(5, 4)")]
        public double? PaymentDue { get; set; }
        public ICollection<Bill> Bills { get; set; } = [];
    }
}
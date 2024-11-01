using System.ComponentModel.DataAnnotations.Schema;
using core.Models.Seed;

namespace core.Models
{
    public class Subscription : Entity 
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Tier? Tier { get; set; }

        public ICollection<Bill> Bills { get; set; } = [];

        public static implicit operator short(Subscription v)
        {
            throw new NotImplementedException();
        }
    }
}
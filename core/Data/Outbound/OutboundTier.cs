using core.Models;

namespace core.Data.Outbound
{
    public class OutboundTier
    {
        public string? ModelDescription { get; set; }
        public ClassificationModel ModelNumber { get; set; }
        public float BasePricing { get; set; }
    }
}
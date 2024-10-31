using core.Models;

namespace core.Data.Outbound
{
    public readonly record struct OutboundMedia(
        Guid Id,
        byte[] Content
    ){
        public static OutboundMedia BuildFromEntity(Media media)
        {
            return new OutboundMedia (
                media.Id,
                media.Content!
            );
        }
    }
}
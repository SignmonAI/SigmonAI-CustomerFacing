using core.Models;

namespace core.Data.Outbound
{
    public readonly record struct OutboundRequest(
        Guid Id,
        Guid UserId,
        string Answer,
        DateOnly Date
    ){
        public static OutboundRequest BuildFromEntity(Request request)
        {
            return new OutboundRequest (
                request.Id,
                request.User!.Id,
                request.Answer!,
                request.Date
            );
        }
    }
}
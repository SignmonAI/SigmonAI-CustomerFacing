namespace core.Data.Payloads
{
    public record MediaCreatePayload(
        Guid UserId,
        IFormFile Content
    );
}
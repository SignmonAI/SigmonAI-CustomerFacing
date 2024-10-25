namespace core.Data.Payloads
{
    public record UserCreatePayload(
        string Name,
        string Email,
        string Phone,
        string Password
    );
}
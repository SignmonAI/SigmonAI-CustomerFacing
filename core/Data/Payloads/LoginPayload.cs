namespace core.Data.Payloads
{
    public readonly record struct LoginPayload(
        string Email,
        string Password
    );
}
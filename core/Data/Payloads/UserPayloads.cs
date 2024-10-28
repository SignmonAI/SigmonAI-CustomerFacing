namespace core.Data.Payloads
{
    public readonly record struct UserCreatePayload(
        string Name,
        string Email,
        string Phone,
        string Password
    );

    public readonly record struct UserUpdatePayload(
        string? Name,
        string? Email,
        string? Phone,
        string? Password
    );
}
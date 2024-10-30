namespace core.Data.Payloads
{
    public readonly record struct CountryCreatePayload(
        string Name,
        string PhoneCode
    );

    public readonly record struct CountryUpdatePayload(
        string? Name,
        string? PhoneCode
    );
}
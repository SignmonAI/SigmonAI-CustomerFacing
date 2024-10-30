namespace core.Data.Payloads
{
    public readonly record struct LanguageCreatePayload(
        Guid CountryId,
        string Name
    );

    public readonly record struct LanguageUpdatePayload(
        Guid? CountryId,
        string? Name
    );
}
namespace core.Data.Payloads
{
    public readonly record struct CreateTierPayload(
        string ModelDescription,
        float BasePricing
    );
}
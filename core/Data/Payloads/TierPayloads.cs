namespace core.Data.Payloads
{
    public readonly record struct TierCreatePayload(
        string ModelDescription,
        float BasePricing
    );
}
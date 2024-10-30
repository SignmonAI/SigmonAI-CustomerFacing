namespace core.Data.Payloads
{
    public readonly record struct TierCreatePayload(
        string ModelDescription,
        float BasePricing
    );

    public readonly record struct TierUpdatePayload(
        string ModelDescription,
        float BasePricing
    );
}
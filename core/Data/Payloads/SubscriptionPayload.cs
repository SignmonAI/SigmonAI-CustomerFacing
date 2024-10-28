namespace core.Data.Payloads
{
    public readonly record struct SubscriptionCreatePayload(
        Guid UserId,
        double PaymentDue
    );

}
namespace core.Data.Payloads
{
    public readonly record struct BillCreatePayload(
        Guid SubscriptionId,
        DateOnly ExpiralDate,
        DateOnly PaymentDate,
        double PaymentDue
    );

    public readonly record struct BillUpdatePayload(
            Guid? SubscriptionId,
            DateOnly? ExpiralDate,
            DateOnly? PaymentDate,
            double? PaymentDue
        );
}
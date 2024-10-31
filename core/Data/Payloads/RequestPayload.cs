namespace core.Data.Payloads
{
    public readonly record struct RequestCreatePayload(
        Guid UserId,
        Guid MediaId,
        string Answer
    );

}
using core.Models;

namespace core.Data.Payloads
{
    public readonly record struct TierCreatePayload(
        string ModelDescription,
        ClassificationModel ModelNumber
    );
}
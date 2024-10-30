namespace core.Data
{
    public record JwtSettings
    {
        public required string SecretKey { get; init; }
    }
}
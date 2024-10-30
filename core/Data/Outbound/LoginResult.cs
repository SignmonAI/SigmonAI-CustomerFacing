namespace core.Data.Outbound
{
    public abstract record LoginResult()
    {
        public record Failed() : LoginResult;
        public record Succeeded(Guid UserId) : LoginResult;
    }
}
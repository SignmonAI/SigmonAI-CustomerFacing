namespace core.Data.Outbound
{
    public abstract record LoginResult()
    {
        public record Failed(bool UsernameMatches, bool PasswordMatches) : LoginResult;
        public record Succeeded(Guid UserId) : LoginResult;
    }
}
using core.Data.Outbound;

namespace core.Errors
{
    public class AuthenticationException : Exception
    {
        public LoginResult.Failed Failure { get; private set; }

        public AuthenticationException(string message, LoginResult.Failed failure) : base(message)
        {
            Failure = failure;
        }
    }
}
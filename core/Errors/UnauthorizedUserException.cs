namespace core.Errors
{
    public class UnauthorizedUserException : Exception
    {
        public UnauthorizedUserException(string message) : base(message) {}

        public UnauthorizedUserException(string message, Exception inner): base(message, inner) {}
    }
}
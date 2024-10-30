namespace core.Errors
{
    public class UnknownServerError : Exception
    {
        public UnknownServerError(): base("Unknown server error.") { }

        public UnknownServerError(Exception inner): base("Unknown server error.", inner) { }
    }
}
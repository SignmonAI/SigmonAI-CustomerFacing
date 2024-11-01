namespace core.Errors
{
    public class ExternalResponseException : Exception
    {
        public ExternalResponseException(string message) : base(message) {}

        public ExternalResponseException(string message, Exception inner): base(message, inner) {}
    } 
}
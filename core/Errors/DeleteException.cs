namespace core.Errors
{
    public class DeleteException : Exception
    {
        public DeleteException(string message) : base(message) {}

        public DeleteException(string message, Exception inner): base(message, inner) {}
    }
}
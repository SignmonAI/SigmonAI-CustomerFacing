namespace core.Errors
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message) : base(message) { }
        
        public InvalidTokenException(string message, Exception inner) : base(message, inner) { }
    }   
}
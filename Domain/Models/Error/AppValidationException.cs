namespace Domain.Models.Error
{
    public class AppValidationException : Exception
    {
        public AppValidationException()
        {
        }

        public AppValidationException(string message)
            : base(message)
        {
        }

        public AppValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

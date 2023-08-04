namespace CleanArchitecture.API.Errors
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {

        }
     
    }
}

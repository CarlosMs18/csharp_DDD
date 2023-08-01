using FluentValidation.Results;


namespace CleanArchitecture.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base("Sé presentaron uno o mas errores de validacion")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this() //para este constructor no quiero pasar mensaje al padre
            //this -> que sea referenciado a esta misma clase 
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()); //error qyue representara el conjunto de errores
                                                         //validaciones se inicialize desde failures
        }

        public IDictionary<string, string[]> Errors { get;}
    }
}

namespace SchoolMedicalSystem.Application.ExceptionHandler
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message)
         : base(message, "VALIDATION_ERROR") { }


    }
}

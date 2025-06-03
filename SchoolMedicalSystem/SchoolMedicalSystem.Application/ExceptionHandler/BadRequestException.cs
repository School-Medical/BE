namespace SchoolMedicalSystem.Application.ExceptionHandler
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message)
        : base(message, "BAD_REQUEST") { }
    }
}

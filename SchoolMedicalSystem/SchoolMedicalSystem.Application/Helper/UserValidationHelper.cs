using System.Text.RegularExpressions;
using SchoolMedicalSystem.Application.DTO.Request;

public static class UserValidationHelper
{
    public static (bool IsValid, string ErrorMessage) ValidateLogin(UserLoginDTORequest request)
    {
        if (request == null)
            return (false, "Request is null.");
        if (string.IsNullOrWhiteSpace(request.Account))
            return (false, "Account is required.");
        if (string.IsNullOrWhiteSpace(request.Password))
            return (false, "Password is required.");
        if (request.Password.Length < 6)
            return (false, "Password must be at least 6 characters.");
        return (true, string.Empty);
    }

    public static (bool IsValid, string ErrorMessage) ValidateRegister(UserRegisterDTORequest request)
    {
        if (request == null)
            return (false, "Request is null.");
        if (string.IsNullOrWhiteSpace(request.Account))
            return (false, "Account is required.");
        if (string.IsNullOrWhiteSpace(request.HashPassword))
            return (false, "Password is required.");
        if (request.HashPassword.Length < 6)
            return (false, "Password must be at least 6 characters.");
        if (string.IsNullOrWhiteSpace(request.FirstName))
            return (false, "First name is required.");
        if (string.IsNullOrWhiteSpace(request.LastName))
            return (false, "Last name is required.");
        if (!string.IsNullOrWhiteSpace(request.PhoneNumber) &&
            !Regex.IsMatch(request.PhoneNumber, @"^0\d{9}$"))
            return (false, "Phone number is invalid.");
        // Add more rules as needed
        return (true, string.Empty);
    }
}

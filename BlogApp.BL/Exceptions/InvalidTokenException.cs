using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions;

public class InvalidTokenException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public InvalidTokenException()
    {
        ErrorMessage = "Invalid token!";
    }

    public InvalidTokenException(string message)
    {
        ErrorMessage = message;
    }
}

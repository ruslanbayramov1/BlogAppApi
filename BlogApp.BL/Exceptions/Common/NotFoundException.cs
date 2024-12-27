using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions.Common;

public class NotFoundException<T> : Exception, IBaseException where T : class, new()
{
    public int StatusCode => StatusCodes.Status404NotFound;

    public string ErrorMessage { get; }

    public NotFoundException()
    {
        ErrorMessage = $"No {typeof(T).Name.ToLower()} with given id";
    }

    public NotFoundException(string message)
    {
        ErrorMessage = message;
    }
}

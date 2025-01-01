using BlogApp.Core.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions.Common;

public class ExistsException<T> : Exception, IBaseException where T : BaseEntity, new()
{
    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public ExistsException()
    {
        ErrorMessage = $"{typeof(T).Name} is already exists";
    }

    public ExistsException(string message)
    {
        ErrorMessage = message;
    }
}

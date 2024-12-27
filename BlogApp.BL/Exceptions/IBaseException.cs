namespace BlogApp.BL.Exceptions;

public interface IBaseException
{
    int StatusCode { get; }
    string ErrorMessage { get; }
}

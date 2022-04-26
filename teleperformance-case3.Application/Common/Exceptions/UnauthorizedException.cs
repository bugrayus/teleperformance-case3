namespace teleperformance_case3.Application.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(Error error)
    {
        Error = error;
    }

    public Error Error { get; set; }
}
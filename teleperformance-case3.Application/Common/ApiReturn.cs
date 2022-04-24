namespace teleperformance_case3.Application.Common;

public class ApiReturn
{
    public string ApiVersion { get; set; } = "1.0";
    public int StatusCode { get; set; }
    public DateTime UtcTimestamp { get; set; } = DateTime.UtcNow;
    public bool IsErrorOccured { get; set; }
    public Error Error { get; set; }

    public bool ShouldSerializeError()
    {
        return IsErrorOccured;
    }

    public bool ShouldSerializeData()
    {
        return !IsErrorOccured;
    }

    public static ApiReturn ErrorResponse(Error error, int statusCode)
    {
        return new ApiReturn
        {
            Error = error,
            IsErrorOccured = true,
            StatusCode = statusCode
        };
    }
}
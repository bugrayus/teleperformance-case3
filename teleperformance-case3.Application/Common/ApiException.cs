﻿namespace teleperformance_case3.Application.Common;

public class ApiException : Exception
{
    public ApiException(Error error)
    {
        Error = error;
    }

    public Error Error { get; set; }
}

public class Error
{
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public List<string> Errors { get; set; }

    public bool ShouldSerializeStackTrace()
    {
        return false;
    }
}
﻿namespace teleperformance_case3.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(Error error)
    {
        Error = error;
    }

    public Error Error { get; set; }
}
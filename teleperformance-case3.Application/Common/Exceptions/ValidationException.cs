namespace teleperformance_case3.Application.Common.Exceptions;

//public class ValidationException : Exception
//{
//    public ValidationException()
//        : base("One or more validation failures have occurred.")
//    {
//        Errors = new Dictionary<string, string[]>();
//    }

//    public ValidationException(IEnumerable<ValidationFailure> failures)
//        : this()
//    {
//        Errors = failures
//            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
//            .ToList(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
//    }

//    public IDictionary<string, string[]> Errors { get; }
//}

public class ValidationException : Exception
{
    public ValidationException(Error error)
    {
        Error = error;
    }

    public Error Error { get; set; }
}
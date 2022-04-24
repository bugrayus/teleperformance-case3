namespace teleperformance_case3.Application.Common;

public class ApiResponse<T>
{
    public string Message { get; set; }
    public T Data { get; set; }
}
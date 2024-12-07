using System;

namespace WebAPI.Models.Result;

public class ApiResponse<T> // Use Of Generics
{
    public int StatusCode { get; set; }
    public String ?HttpMethod { get; set; }
    public T ?Data { get; set; }
}

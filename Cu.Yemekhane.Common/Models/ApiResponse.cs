
namespace Cu.Yemekhane.Common.Models;
public struct ApiResponse<T>
{
    public bool Success => string.IsNullOrEmpty(ErrorMessage) ? true : false;
    public string ErrorMessage { get; set; }
    public T? Data { get; set; }
}
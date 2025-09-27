using System.Net;

namespace LIST.TASK.DEMO.Servicios.Common
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success => (int)StatusCode >= 200 && (int)StatusCode < 300;

        public static ApiResponse<T> SuccessResponse(T data, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "")
        {
            return new ApiResponse<T>
            {
                Data = data,
                StatusCode = statusCode,
                Message = message
            };
        }

        public static ApiResponse<T> ErrorResponse(HttpStatusCode statusCode, string message)
        {
            return new ApiResponse<T>
            {
                Data = default,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}
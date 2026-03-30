namespace MediCue.Exceptions
{
    public class HttpRequestExceptionEx : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpRequestExceptionEx(HttpStatusCode statusCode)
            : base($"Request Failed with Status Code {statusCode}")
        {
            StatusCode = statusCode;
        }

        public HttpRequestExceptionEx(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpRequestExceptionEx(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}

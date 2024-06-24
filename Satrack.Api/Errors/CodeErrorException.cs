namespace Satrack.Api.Errors
{
    public class CodeErrorException<T> : CodeErrorResponse
    {
        public T? Details { get; set; }


        public CodeErrorException(int statusCode, string? message = null, T? details = default) : base(statusCode, message)
        {
            Details = details;
        }
    }
}


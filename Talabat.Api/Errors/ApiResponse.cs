namespace Talabat.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode , string? message = null) 
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);       
        }

        private string?  GetDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "A Bad Request, You have made",
                401 => "Authorized you arn't",
                404 => "Not Found Resources",
                500 => "Errors are the path to success",
                _ => null

            };
        }
    }
}

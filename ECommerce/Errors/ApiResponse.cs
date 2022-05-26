namespace ECommerce.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GenerateDefaultMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GenerateDefaultMessage(int statusCode)
        {
            var response = statusCode switch
            {
                400 => "Bad request. The parameter is incorrect",
                401=> "You are not authorized",
                404 => "Not found Item",
                500 => "Internal server error. Something happended",
                _ => null
            };

            return response;
        }
    }
}

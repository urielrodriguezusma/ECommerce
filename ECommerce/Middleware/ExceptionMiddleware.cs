using ECommerce.Errors;
//using Newtonsoft.Json;
using System.Text.Json;

namespace ECommerce.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //if (env.IsDevelopment())
                //{
                //    var registro = new ApiException(500, ex.Message, ex.StackTrace.ToString());
                //    var prueba = JsonSerializer.Serialize(registro);
                //    await context.Response.WriteAsync(prueba);
                //}
                //else
                //{
                //    var registro = new ApiResponse(500, ex.Message);
                //    var prueba = JsonSerializer.Serialize(registro);
                //    await context.Response.WriteAsync(prueba);
                //}
                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var regExcep = env.IsDevelopment() ? new ApiException(500, ex.Message, ex.StackTrace.ToString())
                                                   : new ApiResponse(500, ex.Message);

                var json = JsonSerializer.Serialize(regExcep,option);
                //var json = JsonConvert.SerializeObject(regExcep);
                await context.Response.WriteAsync(json);
            }
        }
    }
}

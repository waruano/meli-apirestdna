using Meli.ApiRestDNA.Shared.Emuns;
using Meli.ApiRestDNA.Shared.Exceptions;
using Meli.ApiRestDNA.Shared.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Shared.Extensions
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger = Log.ForContext<ExceptionMiddleware>();
        public ExceptionMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var message = CreateMessage(context, ex);
                _logger.Error(message);
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var (statusCode, result) = ErrorDetailsCreator.GetStatusCodeAndErrorDetailFromException(e);
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var response = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(response);
        }

        private static string CreateMessage(HttpContext context, Exception e)
        {
            var message = $"Exception caught in global error handler, exception message: {e.Message}, exception stack: {e.StackTrace}";

            if (e.InnerException != null)
            {
                message = $"{message}, inner exception message {e.InnerException.Message}, inner exception stack {e.InnerException.StackTrace}";
            }

            return $"{message} RequestId: {context.TraceIdentifier}";
        }
    }

    public static class ErrorDetailsCreator
    {
        public static Tuple<int, ErrorDetails> GetStatusCodeAndErrorDetailFromException(Exception exception)
        {
            return exception switch
            {
                BusinessException businessException => new Tuple<int, ErrorDetails>(StatusCodes.Status403Forbidden, new ErrorDetails()
                { Message = businessException.Message, ErrorCode = businessException.ErrorCode, Details = "Business Exception" }),
                _ => new Tuple<int, ErrorDetails>(StatusCodes.Status500InternalServerError, new ErrorDetails()
                { Message = "Unknown error, please contact the system admin" /*exception.StackTrace*/, ErrorCode = ExceptionEnum.UnknownError.GetHashCode() }),
            };
        }
    }
}

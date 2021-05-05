using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Meli.ApiRestDNA.Shared.Extensions
{
    public static class ExceptionHandlerMiddlewareExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

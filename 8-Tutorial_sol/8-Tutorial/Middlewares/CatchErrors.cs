using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _8_Tutorial.Middlewares
{
    public class CatchErrors
    {
        private readonly RequestDelegate _next;
        public CatchErrors(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exc) {
                httpContext.Response.StatusCode = 404;
                File.WriteAllText("Log.txt",exc.ToString());
            }
        }
    }
}

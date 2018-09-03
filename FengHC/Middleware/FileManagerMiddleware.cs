using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FengHC.Middleware
{
    public class FileManagerMiddleware:IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFileProvider _fileProvider;

        public FileManagerMiddleware(RequestDelegate next, IFileProvider fileProvider)
        {
            _next = next;
            _fileProvider = fileProvider;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}

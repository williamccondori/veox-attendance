using System;
using Microsoft.AspNetCore.Builder;
using Veox.Attendance.Identity.Api.Middlewares;

namespace Veox.Attendance.Identity.Api.Extensions
{
    /// <summary>
    /// Error handler middleware extension.
    /// </summary>
    public static class ErrorHandlerExtension
    {
        /// <summary>
        /// Adds the <see cref="ErrorHandlerMiddleware"/> to the specified <see cref="IApplicationBuilder"/>,
        /// which enables error handling.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        // ReSharper disable once UnusedMethodReturnValue.Global
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            return app;
        }
    }
}
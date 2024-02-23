using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Diagnostics;

namespace SignageLivePlayer.Api.Extensions;

public static class ErrorHandlingExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;

            Log.Information("{@status} Unable to process request. Trace Id: {@trace}", StatusCodes.Status500InternalServerError, Activity.Current?.Id);

            await Results.Problem(
                title: "Unable to process request",
                statusCode: StatusCodes.Status500InternalServerError,
                extensions: new Dictionary<string, object?>
                {
                    { "traceId", Activity.Current?.Id }
                }

                )
                .ExecuteAsync(context);

        });
    }
}

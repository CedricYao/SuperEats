using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SuperEats.Bootstrap;
using Steeltoe.Discovery.Client;
using Steeltoe.Management.CloudFoundry;

namespace SuperEats.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

                    options.RoutePrefix = string.Empty;
                });

            return app;
        }

        public static IApplicationBuilder ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ValidationExceptionMiddleware>();

            return app;
        }
    }
}

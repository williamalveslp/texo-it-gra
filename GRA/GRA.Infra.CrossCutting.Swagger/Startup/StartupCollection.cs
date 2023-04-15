using GRA.Infra.CrossCutting.Swagger.ConfigurationsDto;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GRA.Infra.CrossCutting.Swagger.Startup
{
    public static class StartupCollection
    {
        public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerData = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            var serviceCollectionData = swaggerData?.ServiceCollection;

            _ = services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc(swaggerData?.ServiceCollection.Version,
                    new OpenApiInfo
                    {
                        Title = serviceCollectionData?.Title,
                        Version = serviceCollectionData?.Version,
                        Description = serviceCollectionData?.Description,
                        Contact = new OpenApiContact
                        {
                            Name = serviceCollectionData?.Contact?.Name,
                            Email = serviceCollectionData?.Contact?.Email,
                            Url = new Uri(serviceCollectionData?.Contact?.Url)
                        }
                    });

                var rootFullDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                var rootDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(rootFullDirectory).FullName).FullName).FullName;

                // API project.
                var projectStartupName = $"{rootDirectory.Split(Path.DirectorySeparatorChar).Last()}.xml";

                var xmlPath = Path.Combine(rootFullDirectory, projectStartupName);

                if (!File.Exists(xmlPath))
                    return;

                // Display documentations in endpoints.
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerData = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            var serviceCollectionData = swaggerData?.ServiceCollection;
            var appBuilderData = swaggerData?.AppBuilder;

            string? title = serviceCollectionData?.Title;
            string? version = serviceCollectionData?.Version;

            string? urlRouteSwagger = appBuilderData?.UrlSwagger;
            urlRouteSwagger = string.Format(urlRouteSwagger, version);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // Example: "/swagger/{0}/swagger.json".
                c.SwaggerEndpoint(urlRouteSwagger, title);
            });
        }
    }
}

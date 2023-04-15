using System.ComponentModel;

namespace GRA.Infra.CrossCutting.Swagger.ConfigurationsDto
{
    public class SwaggerSettings
    {
        public AppBuilderDto AppBuilder { get; set; } = null!;

        public ServiceCollectionDto ServiceCollection { get; set; } = null!;

        public class ServiceCollectionDto
        {
            public string Title { get; set; } = null!;

            public string Version { get; set; } = null!;

            public string? Description { get; set; }

            public ContactDto? Contact { get; set; }

            public class ContactDto
            {
                public string? Name { get; set; }

                public string? Email { get; set; }

                public string? Url { get; set; }
            }
        }

        public class AppBuilderDto
        {
            public string UrlSwagger { get; set; } = "/swagger";
        }
    }
}

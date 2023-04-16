using Microsoft.Extensions.Configuration;

namespace GRA.Tests.AppService.Fixture
{
    public class MovieFixture
    {
        public IConfiguration Configuration { get; }

        public MovieFixture()
        {
            this.Configuration = new ConfigurationBuilder()
              .AddJsonFile("AppService/Fixture/gra.appsettings.json", optional: false)
              .Build();
        }
    }
}

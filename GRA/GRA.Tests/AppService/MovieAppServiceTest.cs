using GRA.Tests.AppService.Fixture;
using Microsoft.Extensions.Configuration;

namespace GRA.Tests.AppService
{
    public class MovieAppServiceTest : IClassFixture<MovieFixture>
    {
        private IConfiguration Configuration { get; }


        public MovieAppServiceTest(MovieFixture fixture)
        {
            this.Configuration = fixture.Configuration;
        }
    }
}

using GRA.Application.AppInterfaces.MoviesFeature;
using GRA.Application.AppServices.MoviesFeature;

namespace GRA.API.IoC
{
    public static partial class Register
    {
        /// <summary>
        /// Add the register to AppServices.
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppServices(this IServiceCollection services)
        {
            // Movies.
            services.AddScoped<IMovieAppService, MovieAppService>();
        }
    }
}

using GRA.Domain.Interfaces.Repositories.ReadOnly;
using GRA.Domain.Interfaces.Repositories.Write;
using GRA.Infra.DataStore.EntityFrameworkCore.Repositories.ReadOnly.MoviesFeature;
using GRA.Infra.DataStore.EntityFrameworkCore.Repositories.Write.MoviesFeature;

namespace GRA.API.IoC
{
    public static partial class Register
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            // Movies.
            services.AddScoped<IMovieRepositoryReadOnly, MovieRepositoryReadOnly>();
            services.AddScoped<IMovieRepositoryWrite, MovieRepositoryWrite>();
        }
    }
}

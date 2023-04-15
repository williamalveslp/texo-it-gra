using GRA.Domain.Entities.MovieFeature;
using GRA.Domain.Interfaces.Repositories.ReadOnly;
using GRA.Infra.DataStore.EntityFrameworkCore.Context;
using GRA.Infra.DataStore.EntityFrameworkCore.Repositories.Base;

namespace GRA.Infra.DataStore.EntityFrameworkCore.Repositories.ReadOnly.MoviesFeature
{
    public class MovieRepositoryReadOnly : RepositoryBaseReadOnly<Movie>, IMovieRepositoryReadOnly
    {
        public MovieRepositoryReadOnly(DatabaseContext context) : base(context)
        {
        }
    }
}

using GRA.Domain.Entities.MovieFeature;
using GRA.Domain.Interfaces.Repositories.Write;
using GRA.Infra.DataStore.EntityFrameworkCore.Context;
using GRA.Infra.DataStore.EntityFrameworkCore.Repositories.Base;

namespace GRA.Infra.DataStore.EntityFrameworkCore.Repositories.Write.MoviesFeature
{
    public class MovieRepositoryWrite : RepositoryBaseWrite<Movie>, IMovieRepositoryWrite
    {
        public MovieRepositoryWrite(DatabaseContext context) : base(context)
        {
        }
    }
}

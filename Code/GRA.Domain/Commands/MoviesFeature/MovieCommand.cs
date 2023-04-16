using GRA.Domain.Interfaces.Repositories.ReadOnly;

namespace GRA.Domain.Commands.MoviesFeature
{
    public abstract  class MovieCommand : CommandBase<IMovieRepositoryReadOnly>
    {
        public string? Title { get; set; }

        public string? Studio { get; set; }

        public string? Producer { get; set; }

        public int Year { get; set; }

        public bool? IsWinner { get; set; }
    }
}

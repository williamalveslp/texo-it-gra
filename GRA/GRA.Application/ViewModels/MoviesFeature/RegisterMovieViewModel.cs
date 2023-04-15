namespace GRA.Application.ViewModels.MoviesFeature
{
    public record RegisterMovieViewModel
    {
        public string? Title { get; set; }

        public string? Studio { get; set; }

        public string? Producer { get; set; }

        public int Year { get; set; }
    }
}

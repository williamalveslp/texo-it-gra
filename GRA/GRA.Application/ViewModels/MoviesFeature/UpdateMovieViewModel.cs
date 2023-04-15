namespace GRA.Application.ViewModels.MoviesFeature
{
    public class UpdateMovieViewModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Studio { get; set; }

        public string? Producer { get; set; }

        public int Year { get; set; }

        public bool? IsWinner { get; set; }
    }
}

namespace GRA.Domain.Entities.MovieFeature
{
    public class Movie : EntityBase
    {
        public virtual string Title { get; private set; } = null!;

        public virtual string Studio { get; private set; } = null!;

        public virtual string Producer { get; private set; } = null!;

        public virtual int Year { get; private set; }

        public virtual bool? IsWinner { get; private set; }

        public Movie(string? title, string? studio, string? producer, int year, bool? isWinner)
                    : base()
        {
            this.Title = title?.Trim() ?? throw new ArgumentNullException(title);
            this.Studio = studio?.Trim() ?? throw new ArgumentNullException(studio);
            this.Producer = producer?.Trim() ?? throw new ArgumentNullException(producer);
            this.Year = year;
            this.IsWinner = isWinner;
        }

        public void Update(string? title, string? studio, string? producer, int year, bool? isWinner)
        {
            this.Title = title?.Trim() ?? throw new ArgumentNullException(title);
            this.Studio = studio?.Trim() ?? throw new ArgumentNullException(studio);
            this.Producer = producer?.Trim() ?? throw new ArgumentNullException(producer);
            this.Year = year;
            this.IsWinner = isWinner;
            RefreshUpdatedDate();
        }
    }
}

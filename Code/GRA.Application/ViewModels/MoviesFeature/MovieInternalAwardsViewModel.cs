using System.Text.Json.Serialization;

namespace GRA.Application.ViewModels.MoviesFeature
{
    public class MovieInternalAwardsViewModel
    {
        public IEnumerable<ProducerAwardsVM>? Min { get; set; }

        public IEnumerable<ProducerAwardsVM>? Max { get; set; }

        public class ProducerAwardsVM
        {
            public string? Producer { get; set; }

            public int Interval { get; set; }

            public int PreviousWin { get; set; }

            public int FollowingWin { get; set; }

            [JsonIgnore]
            public bool IsConsective { get; set; }
        }
    }
}

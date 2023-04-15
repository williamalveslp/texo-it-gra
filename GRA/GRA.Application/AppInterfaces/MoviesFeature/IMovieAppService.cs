using GRA.Application.ViewModels.MoviesFeature;

namespace GRA.Application.AppInterfaces.MoviesFeature
{
    public interface IMovieAppService
    {
        Task<int?> InsertAsync(RegisterMovieViewModel viewModel);

        Task<IList<MovieViewModel>> GetAllAsync();

        bool DeleteById(int id);
    }
}

﻿using GRA.Application.ViewModels.MoviesFeature;
using static GRA.Infra.CrossCutting.Helpers.CsvHelper;

namespace GRA.Application.AppInterfaces.MoviesFeature
{
    public interface IMovieAppService
    {
        Task<int?> InsertAsync(RegisterMovieViewModel viewModel);

        Task<int?> UpdateAsync(UpdateMovieViewModel viewModel);

        Task<IList<MovieViewModel>> GetAllAsync();

        bool DeleteById(int id);

        Task<CsvHelperResponse?> ImportFromCsv(string csvFilePath);
    }
}

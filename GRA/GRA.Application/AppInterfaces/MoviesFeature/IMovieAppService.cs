﻿using GRA.Application.ViewModels.MoviesFeature;

namespace GRA.Application.AppInterfaces.MoviesFeature
{
    public interface IMovieAppService
    {
        Task<int?> InsertAsync(RegisterMovieViewModel viewModel);

        Task GetAllAsync();
    }
}
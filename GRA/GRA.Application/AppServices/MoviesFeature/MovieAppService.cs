using AutoMapper;
using GRA.Application.AppInterfaces.MoviesFeature;
using GRA.Application.ViewModels.MoviesFeature;
using GRA.Domain.CommandHandlers;
using GRA.Domain.Commands.MoviesFeature;
using GRA.Domain.Entities.MovieFeature;
using GRA.Domain.Interfaces.Repositories.ReadOnly;
using GRA.Domain.Interfaces.Repositories.Write;
using MediatR;
using System.Net;

namespace GRA.Application.AppServices.MoviesFeature
{
    ///<inheritdoc cref="IMovieAppService"/>
    public class MovieAppService: CommandHandler, IMovieAppService
    {
        private readonly IMovieRepositoryReadOnly _movieRepositoryReadOnly;
        private readonly IMovieRepositoryWrite _movieRepositoryWrite;
        private readonly IMapper _mapper;

        public MovieAppService(IMovieRepositoryReadOnly movieRepositoryReadOnly,
                               IMovieRepositoryWrite movieRepositoryWrite,
                               IMapper mapper,
                               IMediator mediator) : base(mediator)
        {
            this._movieRepositoryReadOnly = movieRepositoryReadOnly;
            this._movieRepositoryWrite = movieRepositoryWrite;
            this._mapper = mapper;
        }

        public async Task<int?> InsertAsync(RegisterMovieViewModel viewModel)
        {
            var command = _mapper.Map<RegisterMovieViewModel, RegisterMovieCommand>(viewModel);

            if (!command.Validator(_movieRepositoryReadOnly).IsValid)
            {
                NotifyValidationErrors(command.ValidatorFailures, HttpStatusCode.BadRequest);
                return await Task.FromResult<int?>(null);
            }

            Movie movie = new Movie(command?.Title, command?.Studio, command?.Producer, command?.Year ?? 0);
            movie = await _movieRepositoryWrite.InsertSaveAsync(movie);

            return movie.Id;
        }

        public async Task GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

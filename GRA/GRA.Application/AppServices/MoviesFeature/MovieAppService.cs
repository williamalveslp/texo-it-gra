using AutoMapper;
using GRA.Application.AppInterfaces.MoviesFeature;
using GRA.Application.ViewModels.MoviesFeature;
using GRA.Domain.CommandHandlers;
using GRA.Domain.Commands.MoviesFeature;
using GRA.Domain.Entities.MovieFeature;
using GRA.Domain.Interfaces.Repositories.ReadOnly;
using GRA.Domain.Interfaces.Repositories.Write;
using GRA.Infra.CrossCutting.Helpers;
using MediatR;
using System.Data;
using System.Net;
using static GRA.Infra.CrossCutting.Helpers.CsvHelper;

namespace GRA.Application.AppServices.MoviesFeature
{
    ///<inheritdoc cref="IMovieAppService"/>
    public class MovieAppService : CommandHandler, IMovieAppService
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

            Movie movie = new Movie(command?.Title, command?.Studio, command?.Producer, command?.Year ?? 0, command?.IsWinner);
            movie = await _movieRepositoryWrite.InsertSaveAsync(movie);

            return movie.Id;
        }

        public async Task<int?> UpdateAsync(UpdateMovieViewModel viewModel)
        {
            var command = _mapper.Map<UpdateMovieViewModel, UpdateMovieCommand>(viewModel);

            if (!command.Validator(_movieRepositoryReadOnly).IsValid)
            {
                NotifyValidationErrors(command.ValidatorFailures, HttpStatusCode.BadRequest);
                return await Task.FromResult<int?>(null);
            }

            var movie = _movieRepositoryReadOnly.GetById(command.Id);

            if (movie == null)
            {
                NotifyValidationErrors("Filme não foi encontrado para ser atualizado.", HttpStatusCode.BadRequest);
                return await Task.FromResult<int?>(null);
            }

            movie.Update(command.Title, command.Studio, command.Producer, command.Year, command.IsWinner);
            movie = await _movieRepositoryWrite.UpdateSaveAsync(movie);

            return movie.Id;
        }

        public async Task<IList<MovieViewModel>> GetAllAsync()
        {
            var movies = await _movieRepositoryReadOnly.GetAllAsync();
            return _mapper.Map<IList<Movie>, IList<MovieViewModel>>(movies);
        }

        public async Task<CsvHelperResponse?> ImportFromCsv(string csvFilePath)
        {
            CsvHelperResponse? csvExtracted = CsvHelper.GetDataFromCsvFile(csvFilePath);

            if (csvExtracted == null || csvExtracted?.DataTable?.Rows == null)
                return csvExtracted;

            IList<Movie> movies = new List<Movie>(capacity: csvExtracted.DataTable.Rows.Count);
            IEnumerable<DataRow> enumerableDataRows = csvExtracted.DataTable.Rows.Cast<DataRow>();

            Parallel.ForEach(enumerableDataRows, row =>
            {
                int year = int.Parse($"{row.ItemArray[0]}");
                string title = $"{row.ItemArray[1]}";
                string studios = $"{row.ItemArray[2]}";
                string producers = $"{row.ItemArray[3]}";

                string? isWinnerValue = $"{row.ItemArray[3]}";
                bool? isWinner;

                if (string.IsNullOrEmpty(isWinnerValue))
                    isWinner = null;
                else if (isWinnerValue.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    isWinner = true;
                else
                    isWinner = false;

                Movie movie = new Movie(title, studios, producers, year, isWinner);
                movies.Add(movie);
            });

            await _movieRepositoryWrite.InsertSaveInLoteAsync(movies);

            return csvExtracted;
        }

        public bool DeleteById(int id)
        {
            var movie = _movieRepositoryReadOnly.GetById(id);

            if (movie == null)
            {
                NotifyValidationErrors("Filme não encontrado para ser excluído.", HttpStatusCode.BadRequest);
                return false;
            }

            _movieRepositoryWrite.Remove(id);
            return true;
        }
    }
}

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
using static GRA.Application.ViewModels.MoviesFeature.MovieInternalAwardsViewModel;
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

        public async Task<MovieViewModel> GetByIdAsync(int id)
        {
            var movie = await _movieRepositoryReadOnly.GetByIdAsync(id);
            return _mapper.Map<Movie, MovieViewModel>(movie);
        }

        public async Task<MovieInternalAwardsViewModel> GetIntervalBestAwards()
        {
            IList<MovieProducerViewModel> movieProducerViewModels = await GetProducerByMovies();
            IList<ProducerAwardsVM> producersAwardsVM = new List<ProducerAwardsVM>();

            foreach (var item in movieProducerViewModels)
            {
                ProducerAwardsVM producerAwardsVM = new ProducerAwardsVM();

                var findMoviesByProducer = producersAwardsVM.Where(f => !string.IsNullOrEmpty(f.Producer)
                                                                 && f.Producer.Equals(item.Producer, StringComparison.OrdinalIgnoreCase))
                                                            .ToList();
                producerAwardsVM.Producer = item.Producer;

                if (!findMoviesByProducer.Any())
                {
                    producerAwardsVM.PreviousWin = item.Year;
                }
                else
                {
                    foreach (var movieByProducer in findMoviesByProducer)
                    {
                        if (item.Year == movieByProducer.PreviousWin
                            && movieByProducer.FollowingWin == 0)
                        {
                            producerAwardsVM.PreviousWin = item.Year;
                            producerAwardsVM.FollowingWin = item.Year;
                        }
                        else
                        {
                            var itemToRemove = producersAwardsVM.FirstOrDefault(f => f == movieByProducer);
                            if (itemToRemove != null)
                                producersAwardsVM.Remove(itemToRemove);

                            producerAwardsVM.PreviousWin = item.Year > movieByProducer.PreviousWin ? movieByProducer.PreviousWin : item.Year;
                            producerAwardsVM.FollowingWin = item.Year > movieByProducer.PreviousWin ? item.Year : movieByProducer.PreviousWin;
                        }

                        producerAwardsVM.Interval = producerAwardsVM.FollowingWin - producerAwardsVM.PreviousWin;
                    }
                }

                producersAwardsVM.Add(producerAwardsVM);
            }

            MovieInternalAwardsViewModel result = new MovieInternalAwardsViewModel();
            result.Min = producersAwardsVM.Where(f => f.Interval > 0).OrderBy(f => f.Interval).Take(2);
            result.Max = producersAwardsVM.Where(f => f.Interval > 0).OrderByDescending(f => f.Interval).Take(2);

            return result;
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
                if (string.IsNullOrEmpty(row.ItemArray[0]?.ToString())
                    || string.IsNullOrEmpty(row.ItemArray[1]?.ToString())
                    || string.IsNullOrEmpty(row.ItemArray[2]?.ToString())
                    || string.IsNullOrEmpty(row.ItemArray[3]?.ToString()))
                {
                    return;
                }

                int year = int.Parse($"{row.ItemArray[0]}");
                string title = $"{row.ItemArray[1]}";
                string studios = $"{row.ItemArray[2]}";
                string producers = $"{row.ItemArray[3]}";

                string? isWinnerValue = $"{row.ItemArray[4]}";
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

            await _movieRepositoryWrite.InsertSaveInBatchAsync(movies);

            return csvExtracted;
        }

        public bool DeleteById(int id)
        {
            var movie = _movieRepositoryReadOnly.GetById(id);

            if (movie == null)
            {
                NotifyValidationErrors("Filme foi não encontrado para poder ser excluído.", HttpStatusCode.BadRequest);
                return false;
            }

            _movieRepositoryWrite.Remove(id);
            return true;
        }

        private async Task<IList<MovieProducerViewModel>> GetProducerByMovies()
        {
            IEnumerable<Movie> allMovies = await _movieRepositoryReadOnly.GetAllAsync();

            var moviesByYear = allMovies.Where(f => f.IsWinner == true)
                                        .GroupBy(f => f.Year);

            IList<MovieProducerViewModel> movieProducerViewModels = new List<MovieProducerViewModel>();

            foreach (IGrouping<int, Movie> movieItem in moviesByYear)
            {
                IEnumerable<string> producersLine = movieItem.Select(f => f.Producer);

                if (producersLine == null)
                    continue;

                foreach (var producersByLine in producersLine)
                {
                    List<string> producersList = new List<string>();

                    string wordAndToSplit = " and ";

                    if (producersByLine.Contains(wordAndToSplit, StringComparison.OrdinalIgnoreCase))
                    {
                        // Position that Starts the " and ".
                        int positionAnd = producersByLine.LastIndexOf(wordAndToSplit, StringComparison.OrdinalIgnoreCase);

                        // Split the names using the comma as separator.
                        List<string>? firstNames = null;

                        if (producersByLine.Contains(','))
                            firstNames = producersByLine.Substring(0, positionAnd).Split(',').ToList();
                        else
                            firstNames = producersByLine.Substring(0, positionAnd).Split(string.Empty).ToList();

                        // Add the Last name after the value "and".
                        string lastName = producersByLine.Substring(positionAnd + wordAndToSplit.Length);

                        producersList.AddRange(firstNames);
                        producersList.Add(lastName);
                    }
                    else
                    {
                        producersList.Add(producersByLine);
                    }

                    // Hygienization on list.
                    producersList = producersList.Select(f => f.Trim())
                                                 .Where(f => !string.IsNullOrEmpty(f))
                                                 .ToList();

                    // Iterate the list of producers.
                    foreach (var producerItem in producersList)
                    {
                        // Iterate the movies by year of the producers.
                        foreach (var item in movieItem)
                        {
                            MovieProducerViewModel movieProducerViewModel = new MovieProducerViewModel
                            {
                                Producer = producerItem,
                                Year = item.Year,
                                Title = item.Title
                            };

                            if (!movieProducerViewModels.Contains(movieProducerViewModel))
                            {
                                movieProducerViewModels.Add(movieProducerViewModel);
                            }
                        }
                    }
                }
            }

            return movieProducerViewModels;
        }
    }
}

using GRA.API.Controllers.Base;
using GRA.Application.AppInterfaces.MoviesFeature;
using GRA.Application.Responses;
using GRA.Application.ViewModels.MoviesFeature;
using GRA.Domain.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GRA.API.Controllers
{
    /// <summary>
    /// API de Filmes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ApiBaseController
    {
        private readonly IMovieAppService _movieAppService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="movieAppService"></param>
        /// <param name="notifications"></param>
        /// <param name="logger"></param>
        public MovieController(IMovieAppService movieAppService,
                               INotificationHandler<DomainNotificationRequest> notifications,
                               ILogger<MovieController> logger) : base(notifications)
        {
            this._movieAppService = movieAppService;
        }

        /// <summary>
        /// Cadastrar Filme.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response200ConfirmationViewModel<int?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response400ClientErrorViewModel<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response500ServerErrorViewModel<IEnumerable<string>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] RegisterMovieViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return ResponseModelStateInvalid();

            return Response(await _movieAppService.InsertAsync(viewModel));
        }

        /// <summary>
        /// Lista todos os Filmes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response200ConfirmationViewModel<IList<MovieViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response400ClientErrorViewModel<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response500ServerErrorViewModel<IEnumerable<string>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return ResponseModelStateInvalid();

            return Response(await _movieAppService.GetAllAsync());
        }
    }
}

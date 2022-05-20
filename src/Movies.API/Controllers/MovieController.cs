using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Common.Exceptions;
using Movies.Application.Movie.Queries;

namespace Movies.API.Controllers
{
    public class MovieController : BaseController
    {
        public MovieController(ILogger<MovieController> logger, IMediator mediator) : base(logger, mediator)
        {

        }

        /// <summary>
        /// Lists movies by filters
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseWrapper<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<MovieListItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MovieListItemDto>>> Get(CancellationToken cancellationToken, [FromQuery] ListMoviesQuery request)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.Any())
                return NotFound();

            return Ok(response);
        }


        /// <summary>
        /// Lists top n movies by total user rating
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("top-rated")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseWrapper<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<MovieListItemDto>), StatusCodes.Status200OK)]
        [ResponseCache(VaryByQueryKeys = new[] { "itemCount" }, Duration = 60)]
        public async Task<ActionResult<IEnumerable<MovieListItemDto>>> GetTopN(CancellationToken cancellationToken, [FromQuery] ListTopNMoviesQuery request)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.Any())
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Lists the top n movies based on a specific user's rating
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("top-rated-by-user")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseWrapper<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<MovieListItemDto>), StatusCodes.Status200OK)]
        [ResponseCache(VaryByQueryKeys = new[] { "itemCount", "userId" }, Duration = 60)]
        public async Task<ActionResult<IEnumerable<MovieListItemDto>>> GetTopN(CancellationToken cancellationToken, [FromQuery] ListTopNMoviesByUserQuery request)
        {
            var response = await _mediator.Send(request, cancellationToken);

            if (!response.Any())
                return NotFound();

            return Ok(response);
        }
    }
}
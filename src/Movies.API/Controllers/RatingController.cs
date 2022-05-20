using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Common.Exceptions;
using Movies.Application.Rate.Commands;

namespace Movies.API.Controllers
{
    public class RatingController : BaseController
    {
        public RatingController(ILogger<RatingController> logger, IMediator mediator) : base(logger, mediator)
        {

        }

        /// <summary>
        /// Creates or Updates a rating
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseWrapper<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(AddRatingCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }
    }
}
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    }
}
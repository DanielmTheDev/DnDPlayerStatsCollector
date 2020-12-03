﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Roll20Stats.ApplicationLayer.Commands.CreateGame;

namespace Roll20Stats.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> Create(string name)
        {
            var result = await _mediator.Send(new CreateGameCommand { Name = name });
            if (!result.HasError)
                return Ok(result.Response);
            HttpContext.Response.StatusCode = result.StatusCode;
            return new JsonResult(new { Error = result.ErrorMessage });
        }
    }
}
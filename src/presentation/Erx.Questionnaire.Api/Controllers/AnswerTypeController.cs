using Application.CQRS.Commands.AnswerTypeCommand;
using Application.CQRS.Queries.AnswerTypeQuery;
using Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erx.Questionnaire.Api.Controllers
{
    [Authorize(Constants.AuthorizePolicy.CLIENT_KEY)]
    [ApiController]
    public class AnswerTypeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AnswerTypeController> _logger;

        public AnswerTypeController(IMediator mediator, ILogger<AnswerTypeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllAnswerTypeQuery());

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetAnswerTypeByIdQuery { Id = id });

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateAnswerTypeCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(long id, UpdateAnswerTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new DeleteAnswerTypeByIdCommand { Id = id });

            if (response <= 0)
                return NotFound();

            return Ok(response);
        }
    }
}

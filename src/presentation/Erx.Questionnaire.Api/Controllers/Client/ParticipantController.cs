using Application.CQRS.Commands.ParticipantCommand;
using Application.CQRS.Queries.ParticipantQuery;
using Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Erx.Questionnaire.Api.Controllers.Client
{
    [Authorize(Constants.AuthorizePolicy.CLIENT_KEY)]
    [ApiController]
    public class ParticipantController : ClientBaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ParticipantController> _logger;

        public ParticipantController(IMediator mediator, ILogger<ParticipantController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllParticipantQuery());
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetParticipantByIdQuery { Id = id });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateParticipantCommand command)
        {
            var response = await _mediator.Send(command);
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(long id, UpdateParticipantCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            return Ok(response);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new DeleteParticipantByIdCommand { Id = id });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response <= 0)
                return NotFound();

            return Ok(response);
        }
    }
}

using Application.CQRS.Commands.ParticipantAnswerCommand;
using Application.CQRS.Queries.ParticipantAnswerQuery;
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
    public class ParticipantAnswerController : ClientBaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ParticipantAnswerController> _logger;

        public ParticipantAnswerController(IMediator mediator, ILogger<ParticipantAnswerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            if (page < 0 || pageSize <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetAllParticipantAnswerQuery { Page = page, PageSize = pageSize });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByParticipantId(int page, int pageSize, int participantId)
        {
            if (page < 0 || pageSize <= 0 || participantId <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetParticipantAnswerByParticipantIdQuery { Page = page, PageSize = pageSize, ParticipantId = participantId });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByQuestionId(int page, int pageSize, int questionId)
        {
            if (page < 0 || pageSize <= 0 || questionId <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetParticipantAnswerByQuestionIdQuery { Page = page, PageSize = pageSize, QuestionId = questionId });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateParticipantAnswerCommand command)
        {
            var response = await _mediator.Send(command);
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            return Ok(response);
        }
    }
}

using Application.CQRS.Queries.QuestionQuery;
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
    public class ParticipantQuestionController : ClientBaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ParticipantController> _logger;

        public ParticipantQuestionController(IMediator mediator, ILogger<ParticipantController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetQuestion(int participantId)
        {
            if (participantId <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetParticipantQuestionQuery { ParticipantId = participantId });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}

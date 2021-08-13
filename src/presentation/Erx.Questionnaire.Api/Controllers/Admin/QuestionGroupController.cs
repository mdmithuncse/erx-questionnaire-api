using Application.CQRS.Commands.QuestionGroupCommand;
using Application.CQRS.Queries.QuestionGroupQuery;
using Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Erx.Questionnaire.Api.Controllers.Admin
{
    [Authorize(Constants.AuthorizePolicy.ADMIN_KEY)]
    [ApiController]
    public class QuestionGroupController : AdminBaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionGroupController> _logger;

        public QuestionGroupController(IMediator mediator, ILogger<QuestionGroupController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllQuestionGroupQuery());
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

            var response = await _mediator.Send(new GetQuestionGroupByIdQuery { Id = id });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateQuestionGroupCommand command)
        {
            var response = await _mediator.Send(command);
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(long id, UpdateQuestionGroupCommand command)
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

            var response = await _mediator.Send(new DeleteQuestionGroupByIdCommand { Id = id });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response <= 0)
                return NotFound();

            return Ok(response);
        }
    }
}

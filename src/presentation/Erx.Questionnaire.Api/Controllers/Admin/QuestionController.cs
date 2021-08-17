using Application.CQRS.Commands.QuestionCommand;
using Application.CQRS.Queries.QuestionQuery;
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
    public class QuestionController : AdminBaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionGroupController> _logger;

        public QuestionController(IMediator mediator, ILogger<QuestionGroupController> logger)
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

            var response = await _mediator.Send(new GetAllQuestionQuery { Page = page, PageSize = pageSize });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetQuestionByIdQuery { Id = id });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateQuestionCommand command)
        {
            var response = await _mediator.Send(command);
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(long id, UpdateQuestionCommand command)
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

            var response = await _mediator.Send(new DeleteQuestionByIdCommand { Id = id });
            _logger.LogInformation($"End Point: { Request.Path.Value } executed successfully at { DateTime.UtcNow }");

            if (response <= 0)
                return NotFound();

            return Ok(response);
        }
    }
}

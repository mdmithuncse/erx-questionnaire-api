using Application.CQRS.Commands.QuestionGroupCommand;
using Application.CQRS.Queries.QuestionGroupQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erx.Questionnaire.Api.Controllers
{
    [ApiController]
    public class QuestionGroupController : BaseController
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

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateQuestionGroupCommand command)
        {
            var response = await _mediator.Send(command);

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

            if (response <= 0)
                return NotFound();

            return Ok(response);
        }
    }
}

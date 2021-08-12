using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erx.Questionnaire.Api.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(ClientErrors), 400)]
    [ProducesResponseType(typeof(ServerError), 500)]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {

    }
}

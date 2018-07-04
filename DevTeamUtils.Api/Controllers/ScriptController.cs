using DevTeamUtils.Api.Assertions;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class ScriptController : Controller
    {
        private readonly IScriptRepository _scriptRepository;

        public ScriptController(MongoDbContext context)
        {
            _scriptRepository = context.GetScriptRepository();
        }

        [HttpGet("api/[controller]/idPasso/{passoId}")]
        [EnableCors("AllowAll")]
        public IEnumerable<Script> GetAllByProject(Guid passoId)
        {
            return _scriptRepository.GetAllByProject(passoId);
        }

        [HttpGet("api/[controller]/{id}", Name = "GetScript")]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                var error = new
                {
                    value = "O parâmetro id deve possuir um valor",
                    status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                };
                Response.StatusCode = error.status;
                return new ObjectResult(error);
            }
            else
            {
                var item = _scriptRepository.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPost("api/[controller]")]
        [EnableCors("AllowAll")]
        public IActionResult Create([FromBody]JObject body)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            Script script = body.ToObject<Script>();

            //Verifica se há inconsistência nos dados
            ScriptAssertion scriptAssertion = new ScriptAssertion(script, true);
            if (scriptAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(scriptAssertion.Notifications.Notify());
            }

            _scriptRepository.Add(script);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(script);
        }

        [HttpPut("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Update(Guid id, [FromBody]dynamic body)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }

            //Verifica se o registro existe na base
            var scriptFounded = _scriptRepository.Find(id);
            if (scriptFounded == null)
            {
                return NotFound();
            }

            Script scriptNew = body.ToObject<Script>();
            scriptNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            ScriptAssertion scriptAssertion = new ScriptAssertion(scriptNew);
            if (scriptAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(scriptAssertion.Notifications.Notify());
            }
            _scriptRepository.Update(scriptNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(scriptNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var script = _scriptRepository.Find(id);
            if (script == null)
            {
                return NotFound();
            }

            _scriptRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(script);
        }
    }
}

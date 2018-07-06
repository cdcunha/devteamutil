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
    public class ScriptGeradoController : Controller
    {
        private readonly IScriptGeradoRepository _scriptGeradoRepository;

        public ScriptGeradoController(MongoDbContext context)
        {
            _scriptGeradoRepository = context.GetScriptGeradoRepository();
        }

        [HttpGet("api/[controller]/idScript/{scriptId}")]
        [EnableCors("AllowAll")]
        public IEnumerable<ScriptGerado> GetAllByScript(Guid scriptId)
        {
            return _scriptGeradoRepository.GetAllByScript(scriptId);
        }

        [HttpGet("api/[controller]/{id}", Name = "GetScriptGerado")]
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
                var item = _scriptGeradoRepository.Find(id);
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

            //ScriptGerado scriptGerado = new ScriptGerado();
            //scriptGerado.DeserializeJson(body); //Converte Json para o objeto 
            ScriptGerado scriptGerado = body.ToObject<ScriptGerado>();            

            //Verifica se há inconsistência nos dados
            ScriptGeradoAssertion scriptGeradoAssertion = new ScriptGeradoAssertion(scriptGerado, true);
            if (scriptGeradoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(scriptGeradoAssertion.Notifications.Notify());
            }

            _scriptGeradoRepository.Add(scriptGerado);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(scriptGerado);
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
            var scriptGeradoFounded = _scriptGeradoRepository.Find(id);
            if (scriptGeradoFounded == null)
            {
                return NotFound();
            }

            ScriptGerado scriptGeradoNew = body.ToObject<ScriptGerado>();
            scriptGeradoNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            ScriptGeradoAssertion scriptGeradoAssertion = new ScriptGeradoAssertion(scriptGeradoNew);
            if (scriptGeradoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(scriptGeradoAssertion.Notifications.Notify());
            }
            _scriptGeradoRepository.Update(scriptGeradoNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(scriptGeradoNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var scriptGerado = _scriptGeradoRepository.Find(id);
            if (scriptGerado == null)
            {
                return NotFound();
            }

            _scriptGeradoRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(scriptGerado);
        }
    }
}

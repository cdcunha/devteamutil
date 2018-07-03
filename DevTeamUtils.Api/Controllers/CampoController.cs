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
    public class CampoController : Controller
    {
        private readonly ICampoRepository _campoRepository;

        public CampoController(MongoDbContext context)
        {
            _campoRepository = context.GetCampoRepository();
        }

        [HttpGet("api/[controller]/byTable/{tabelaId}")]
        [EnableCors("AllowAll")]
        public IEnumerable<Campo> GetAllByTable(Guid tabelaId)
        {
            return _campoRepository.GetAllByTable(tabelaId);
        }

        [HttpGet("api/[controller]/{id}", Name = "GetCampo")]
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
                var item = _campoRepository.Find(id);
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
            Campo campo = body.ToObject<Campo>();            

            //Verifica se há inconsistência nos dados
            CampoAssertion campoAssertion = new CampoAssertion(campo, true);
            if (campoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(campoAssertion.Notifications.Notify());
            }

            _campoRepository.Add(campo);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(campo);
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
            var campoFounded = _campoRepository.Find(id);
            if (campoFounded == null)
            {
                return NotFound();
            }

            Campo campoNew = body.ToObject<Campo>();
            campoNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            CampoAssertion campoAssertion = new CampoAssertion(campoNew);
            if (campoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(campoAssertion.Notifications.Notify());
            }
            _campoRepository.Update(campoNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(campoNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var campo = _campoRepository.Find(id);
            if (campo == null)
            {
                return NotFound();
            }

            _campoRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(campo);
        }
    }
}

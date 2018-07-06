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
    public class PassoGeradoController : Controller
    {
        private readonly IPassoGeradoRepository _passoGeradoRepository;

        public PassoGeradoController(MongoDbContext context)
        {
            _passoGeradoRepository = context.GetPassoGeradoRepository();
        }

        [HttpGet("api/[controller]/idScript/{scriptId}")]
        [EnableCors("AllowAll")]
        public IEnumerable<PassoGerado> GetAllByPasso(Guid passoId)
        {
            return _passoGeradoRepository.GetAllByPasso(passoId);
        }

        [HttpGet("api/[controller]/{id}", Name = "GetPassoGerado")]
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
                var item = _passoGeradoRepository.Find(id);
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

            //PassoGerado passoGerado = new PassoGerado();
            //passoGerado.DeserializeJson(body); //Converte Json para o objeto 
            PassoGerado passoGerado = body.ToObject<PassoGerado>();            

            //Verifica se há inconsistência nos dados
            PassoGeradoAssertion passoGeradoAssertion = new PassoGeradoAssertion(passoGerado, true);
            if (passoGeradoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(passoGeradoAssertion.Notifications.Notify());
            }

            _passoGeradoRepository.Add(passoGerado);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(passoGerado);
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
            var passoGeradoFounded = _passoGeradoRepository.Find(id);
            if (passoGeradoFounded == null)
            {
                return NotFound();
            }

            PassoGerado passoGeradoNew = body.ToObject<PassoGerado>();
            passoGeradoNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            PassoGeradoAssertion passoGeradoAssertion = new PassoGeradoAssertion(passoGeradoNew);
            if (passoGeradoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(passoGeradoAssertion.Notifications.Notify());
            }
            _passoGeradoRepository.Update(passoGeradoNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(passoGeradoNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var passoGerado = _passoGeradoRepository.Find(id);
            if (passoGerado == null)
            {
                return NotFound();
            }

            _passoGeradoRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(passoGerado);
        }
    }
}

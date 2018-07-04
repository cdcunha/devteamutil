using DevTeamUtils.Api.Assertions;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class PassoController : Controller
    {
        private readonly IPassoRepository _passoRepository;

        public PassoController(MongoDbContext context)
        {
            _passoRepository = context.GetPassoRepository();
        }

        [HttpGet("api/[controller]")]
        [EnableCors("AllowAll")]
        public IEnumerable<Passo> GetAll()
        {
            return _passoRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetPasso")]
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
                var item = _passoRepository.Find(id);
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
            //Passo passo = new Passo();
            //passo.DeserializeJson(body); //Converte Json para o objeto Apoiado
            Passo passo = body.ToObject<Passo>();

            //Verifica se há inconsistência nos dados
            PassoAssertion passoAssertion = new PassoAssertion(passo, true);
            if (passoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(passoAssertion.Notifications.Notify());
            }

            _passoRepository.Add(passo);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(passo);
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
            var passoFounded = _passoRepository.Find(id);
            if (passoFounded == null)
            {
                return NotFound();
            }

            Passo passoNew = body.ToObject<Passo>();
            passoNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            PassoAssertion passoAssertion = new PassoAssertion(passoNew);
            if (passoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(passoAssertion.Notifications.Notify());
            }
            _passoRepository.Update(passoNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(passoNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var passo = _passoRepository.Find(id);
            if (passo == null)
            {
                return NotFound();
            }

            _passoRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(passo);
        }
        
        [HttpGet("api/[controller]/download/{id}", Name = "DownloadPasso")]
        public IActionResult Download(Guid id)
        {
            var passo = _passoRepository.Find(id);
            if (passo == null)
            {
                return NotFound();
            }
            else
            {
                //Verifica se há inconsistência nos dados
                ArquivoPassoAssertion arquivoPassoAssertion = new ArquivoPassoAssertion(passo.TxtPasso, passo.Validado);
                if (arquivoPassoAssertion.Notifications.HasNotifications())
                {   
                    Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                    return new ObjectResult(arquivoPassoAssertion.Notifications.Notify());
                }
            }

            var stream = _passoRepository.DownloadArquivoPasso(passo.TxtPasso);
            var response = File(stream, "text/plain"); // FileStreamResult
            return response;
        }
    }
}

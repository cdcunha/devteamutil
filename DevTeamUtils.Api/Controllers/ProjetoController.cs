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
    public class ProjetoController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoController(MongoDbContext context)
        {
            _projetoRepository = context.GetProjetoRepository();
        }

        [HttpGet("api/[controller]")]
        [EnableCors("AllowAll")]
        public IEnumerable<Projeto> GetAll()
        {
            return _projetoRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetProjeto")]
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
                var item = _projetoRepository.Find(id);
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
            //Projeto projeto = new Projeto();
            //projeto.DeserializeJson(body); //Converte Json para o objeto Apoiado
            Projeto projeto = body.ToObject<Projeto>();

            //Verifica se há inconsistência nos dados
            ProjetoAssertion projetoAssertion = new ProjetoAssertion(projeto, true);
            if (projetoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(projetoAssertion.Notifications.Notify());
            }

            _projetoRepository.Add(projeto);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(projeto);
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
            var projetoFounded = _projetoRepository.Find(id);
            if (projetoFounded == null)
            {
                return NotFound();
            }

            Projeto projetoNew = body.ToObject<Projeto>();
            projetoNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            ProjetoAssertion projetoAssertion = new ProjetoAssertion(projetoNew);
            if (projetoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(projetoAssertion.Notifications.Notify());
            }
            _projetoRepository.Update(projetoNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(projetoNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var projeto = _projetoRepository.Find(id);
            if (projeto == null)
            {
                return NotFound();
            }

            _projetoRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(projeto);
        }
        
        [HttpGet("api/[controller]/download/{id}", Name = "DownloadPasso")]
        public IActionResult Download(Guid id)
        {
            var projeto = _projetoRepository.Find(id);
            if (projeto == null)
            {
                return NotFound();
            }
            else
            {
                //Verifica se há inconsistência nos dados
                ArquivoPassoAssertion arquivoPassoAssertion = new ArquivoPassoAssertion(projeto.Passo, projeto.Validado);
                if (arquivoPassoAssertion.Notifications.HasNotifications())
                {   
                    Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                    return new ObjectResult(arquivoPassoAssertion.Notifications.Notify());
                }
            }

            var stream = _projetoRepository.DownloadArquivoPasso(projeto.Passo);
            var response = File(stream, "text/plain"); // FileStreamResult
            return response;
        }
    }
}

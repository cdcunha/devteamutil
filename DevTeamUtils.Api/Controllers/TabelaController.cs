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
    public class TabelaController : Controller
    {
        private readonly ITabelaRepository _tabelaRepository;

        public TabelaController(MongoDbContext context)
        {
            _tabelaRepository = context.GetTabelaRepository();
        }

        [HttpGet("api/[controller]")]
        [EnableCors("AllowAll")]
        public IEnumerable<Tabela> GetAll()
        {
            return _tabelaRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetTabela")]
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
                var item = _tabelaRepository.Find(id);
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
            Tabela tabela = new Tabela();
            tabela.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            TabelaAssertion tabelaAssertion = new TabelaAssertion(tabela, true);
            if (tabelaAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(tabelaAssertion.Notifications.Notify());
            }

            _tabelaRepository.Add(tabela);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(tabela);
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
            var tabelaFounded = _tabelaRepository.Find(id);
            if (tabelaFounded == null)
            {
                return NotFound();
            }

            Tabela tabelaNew = new Tabela();
            tabelaNew = tabelaFounded;
            tabelaNew.DeserializeJson(body); //Converte Json para o objeto Apoiado
            tabelaNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            TabelaAssertion tabelaAssertion = new TabelaAssertion(tabelaNew);
            if (tabelaAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(tabelaAssertion.Notifications.Notify());
            }
            _tabelaRepository.Update(tabelaNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(tabelaNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var tabela = _tabelaRepository.Find(id);
            if (tabela == null)
            {
                return NotFound();
            }

            _tabelaRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(tabela);
        }
    }
}

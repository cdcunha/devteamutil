using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using DevTeamUtils.Api.Assertions;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class ConexaoController : Controller
    {
        private readonly IConexaoRepository _conexaoRepository;

        public ConexaoController(MongoDbContext context)
        {
            _conexaoRepository = context.GetConexaoRepository();
        }

        [HttpGet("api/[controller]")]
        [EnableCors("AllowAll")]
        public IEnumerable<Conexao> GetAll()
        {
            return _conexaoRepository.GetAll();
        }

        [HttpGet("api/[controller]/import")]
        public IActionResult Import()
        {
            try
            {

                return Ok();
            }
            catch
            {
                return Forbid();
            }
        }

        [HttpGet("api/[controller]/{id}", Name = "GetConexao")]
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
                var item = _conexaoRepository.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPost("api/[controller]")]
        [EnableCors("AllowAll")]
        public IActionResult Create([FromBody]JObject body)//[FromBody] Apoiado conexao)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            Conexao conexao = new Conexao();//(((JValue)body.SelectToken("nome")).Value.ToString());
            conexao.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            ConexaoAssertion conexaoAssertion = new ConexaoAssertion(conexao, true);
            if (conexaoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(conexaoAssertion.Notifications.Notify());
            }

            _conexaoRepository.Add(conexao);
            //return CreatedAtRoute("GetApoio", new { id = conexao.Id }, conexao);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(conexao);
        }

        [HttpPut("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Update(Guid id, [FromBody]dynamic body)//[FromBody]Apoiado conexaoNew)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            
            //Verifica se o registro existe na base
            var conexaoFounded = _conexaoRepository.Find(id);
            if (conexaoFounded == null)
            {
                return NotFound();
            }

            Conexao conexaoNew = new Conexao();
            conexaoNew = conexaoFounded;
            conexaoNew.DeserializeJson(body); //Converte Json para o objeto Apoiado
            conexaoNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            ConexaoAssertion conexaoAssertion = new ConexaoAssertion(conexaoNew);
            if (conexaoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(conexaoAssertion.Notifications.Notify());
            }
            _conexaoRepository.Update(conexaoNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(conexaoNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            /*Apoiado conexao = GetDetail(id);
            DeleteResult result = conexoes.DeleteOne(Builders<Apoiado>.Filter.Eq(p => p.Id, ObjectId.Parse(id)));
            if (result.DeletedCount < 1)
            {
                if (conexao == null)
                    conexao = new Apoiado("Erro detelando");
                conexao.Notifications.Handle("500", "O Registro não foi encontrado");
            }
            return conexao;
            */
            var conexao = _conexaoRepository.Find(id);
            if (conexao == null)
            {
                return NotFound();
            }

            _conexaoRepository.Remove(id);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(conexao);
        }
    }
}

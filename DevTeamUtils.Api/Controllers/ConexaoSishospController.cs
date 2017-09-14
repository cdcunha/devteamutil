using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using DevTeamUtils.Api.Assertions;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class ConexaoSishospController : Controller
    {
        private readonly IConexaoSishospRepository _conexaoSishospRepository;

        public ConexaoSishospController(MongoDbContext context)
        {
            _conexaoSishospRepository = context.GetConexaoSishospRepository();
        }

        [HttpGet("api/[controller]")]
        //[Route("api/[controller]")]
        public IEnumerable<ConexaoSishosp> GetAll()
        {
            return _conexaoSishospRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetConexaoSishosp")]
        //[HttpGet]
        //[Route("api/[controller]/{id}")]
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
                var item = _conexaoSishospRepository.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        /*[HttpGet]
        [Route("api/[controller]/{nome}")]
        public List<Apoiado> Get(string nome)
        {
            var resultado = conexaoSishosps.Find(it => it.Nome.Contains(nome))
                .SortBy(it => it.Nome).Skip(0).Limit(50);
            if (!resultado.Any())
            {
                Apoiado n = new Apoiado("José Maria");
                conexaoSishosps.InsertOne(n);

                n = new Apoiado("José Pedro");
                conexaoSishosps.InsertOne(n);

                n = new Apoiado("Carlos José");
                n.Nome = "Monitor";
                conexaoSishosps.InsertOne(n);

                n = new Apoiado("Marilda Abravanel");
                conexaoSishosps.InsertOne(n);

                n = new Apoiado("Nivaldo Damasceno");
                conexaoSishosps.InsertOne(n);
            }

            
            //Notifications.Handle("TESTE", "Teste de erro");

            //return CreateResponse(HttpStatusCode.Created, resultado);
            return resultado.ToList();
        }*/

        [HttpPost("api/[controller]")]
        //[ValidateAntiForgeryToken]
        //[Route("api/[controller]")]
        public IActionResult Create([FromBody]JObject body)//[FromBody] Apoiado conexaoSishosp)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            ConexaoSishosp conexaoSishosp = new ConexaoSishosp();//(((JValue)body.SelectToken("nome")).Value.ToString());
            conexaoSishosp.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            ConexaoSishospAssertion conexaoSishospAssertion = new ConexaoSishospAssertion(conexaoSishosp, true);
            if (conexaoSishospAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(conexaoSishospAssertion.Notifications.Notify());
            }

            _conexaoSishospRepository.Add(conexaoSishosp);
            //return CreatedAtRoute("GetApoio", new { id = conexaoSishosp.Id }, conexaoSishosp);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(conexaoSishosp);
        }

        [HttpPut("api/[controller]/{id}")]
        //[HttpPut]
        //[Route("api/[controller]/{id}")]
        public IActionResult Update(Guid id, [FromBody]dynamic body)//[FromBody]Apoiado conexaoSishospNew)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            
            //Verifica se o registro existe na base
            var conexaoSishospFounded = _conexaoSishospRepository.Find(id);
            if (conexaoSishospFounded == null)
            {
                return NotFound();
            }

            ConexaoSishosp conexaoSishospNew = new ConexaoSishosp();
            conexaoSishospNew = conexaoSishospFounded;
            conexaoSishospNew.DeserializeJson(body); //Converte Json para o objeto Apoiado
            conexaoSishospNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            ConexaoSishospAssertion conexaoSishospAssertion = new ConexaoSishospAssertion(conexaoSishospNew);
            if (conexaoSishospAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(conexaoSishospAssertion.Notifications.Notify());
            }
            _conexaoSishospRepository.Update(conexaoSishospNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(conexaoSishospNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        //[HttpDelete]
        //[Route("api/[controller]/{id}")]
        public IActionResult Delete(Guid id)
        {
            /*Apoiado conexaoSishosp = GetDetail(id);
            DeleteResult result = conexaoSishosps.DeleteOne(Builders<Apoiado>.Filter.Eq(p => p.Id, ObjectId.Parse(id)));
            if (result.DeletedCount < 1)
            {
                if (conexaoSishosp == null)
                    conexaoSishosp = new Apoiado("Erro detelando");
                conexaoSishosp.Notifications.Handle("500", "O Registro não foi encontrado");
            }
            return conexaoSishosp;
            */
            var conexaoSishosp = _conexaoSishospRepository.Find(id);
            if (conexaoSishosp == null)
            {
                return NotFound();
            }

            _conexaoSishospRepository.Remove(id);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(conexaoSishosp);
        }
    }
}

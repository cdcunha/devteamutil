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
    public class ContatoController : Controller
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(MongoDbContext context)
        {
            _contatoRepository = context.GetVoluntarioRepository();
        }

        [HttpGet("api/[controller]")]
        //[Route("api/[controller]")]
        public IEnumerable<Contato> Get()
        {
            return _contatoRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetContato")]
        //[Route("api/[controller]/{id}")]
        public IActionResult GetById(System.Guid id)
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
                var item = _contatoRepository.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        /*[HttpGet]
        [Route("api/[controller]/{nome}")]
        public List<Voluntario> Get(string nome)
        {
            var resultado = contatos.Find(it => it.Nome.Contains(nome)).SortBy(it => it.Nome).Skip(0).Limit(50);
            //var resultado = contatos.Find(Builders<Voluntario>.Filter.Eq("Id", ObjectId.Parse(id)));
            #region
            if (!resultado.Any())
            {
                Voluntario n = new Voluntario("dr. José Maria");
                contatos.InsertOne(n);

                n = new Voluntario("dr. José Pedro");
                contatos.InsertOne(n);

                n = new Voluntario("dr. Carlos José");
                n.Nome = "Monitor";
                contatos.InsertOne(n);

                n = new Voluntario("dra. Marilda Abravanel");
                contatos.InsertOne(n);

                n = new Voluntario("dr. Nivaldo Damasceno");
                contatos.InsertOne(n);
            }
            #endregion
            return resultado.ToList();
        }
        */
        
        [HttpPost("api/[controller]")]
        //[ValidateAntiForgeryToken]
        //[Route("api/[controller]")]
        public IActionResult Create([FromBody]dynamic body)//[FromBody] Voluntario contato)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            Contato contato = new Contato();//(((JValue)body.SelectToken("nome")).Value.ToString());
            contato.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            ContatoAssertion contatoAssertion = new ContatoAssertion(contato, true);
            if (contatoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(contatoAssertion.Notifications.Notify());
            }

            _contatoRepository.Add(contato);
            //return CreatedAtRoute("GetApoio", new { id = apoiado.Id }, apoiado);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(contato);
        }

        [HttpPut("api/[controller]/{id}")]
        //[Route("api/[controller]/{id}")]
        public IActionResult Update(Guid id, [FromBody]dynamic body)//[FromBody]Voluntario item)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }

            //Verifica se o registro existe na base
            var contatoFounded = _contatoRepository.Find(id);
            if (contatoFounded == null)
            {
                return NotFound();
            }

            Contato contatoNew = new Contato();
            contatoNew = contatoFounded;
            contatoNew.DeserializeJson(body); //Converte Json para o objeto Apoiado
            contatoNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            ContatoAssertion contatoAssertion = new ContatoAssertion(contatoNew);
            if (contatoAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(contatoAssertion.Notifications.Notify());
            }
            _contatoRepository.Update(contatoNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(contatoNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        //[Route("api/[controller]/{id}")]
        public IActionResult Delete(System.Guid id)
        {
            var contato = _contatoRepository.Find(id);
            if (contato == null)
            {
                return NotFound();
            }

            _contatoRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(contato);
        }
    }
}

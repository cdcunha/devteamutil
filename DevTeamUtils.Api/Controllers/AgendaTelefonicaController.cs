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
    public class AgendaTelefonicaController : Controller
    {
        private readonly IAgendaTelefonicaRepository _agendaTelefonicaRepository;

        public AgendaTelefonicaController(MongoDbContext context)
        {
            _agendaTelefonicaRepository = context.GetVoluntarioRepository();
        }

        [HttpGet("api/[controller]")]
        //[Route("api/[controller]")]
        public IEnumerable<AgendaTelefonica> Get()
        {
            return _agendaTelefonicaRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetAgendaTelefonica")]
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
                var item = _agendaTelefonicaRepository.Find(id);
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
            var resultado = agendaTelefonicas.Find(it => it.Nome.Contains(nome)).SortBy(it => it.Nome).Skip(0).Limit(50);
            //var resultado = agendaTelefonicas.Find(Builders<Voluntario>.Filter.Eq("Id", ObjectId.Parse(id)));
            #region
            if (!resultado.Any())
            {
                Voluntario n = new Voluntario("dr. José Maria");
                agendaTelefonicas.InsertOne(n);

                n = new Voluntario("dr. José Pedro");
                agendaTelefonicas.InsertOne(n);

                n = new Voluntario("dr. Carlos José");
                n.Nome = "Monitor";
                agendaTelefonicas.InsertOne(n);

                n = new Voluntario("dra. Marilda Abravanel");
                agendaTelefonicas.InsertOne(n);

                n = new Voluntario("dr. Nivaldo Damasceno");
                agendaTelefonicas.InsertOne(n);
            }
            #endregion
            return resultado.ToList();
        }
        */
        
        [HttpPost("api/[controller]")]
        //[ValidateAntiForgeryToken]
        //[Route("api/[controller]")]
        public IActionResult Create([FromBody]dynamic body)//[FromBody] Voluntario agendaTelefonica)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            AgendaTelefonica agendaTelefonica = new AgendaTelefonica();//(((JValue)body.SelectToken("nome")).Value.ToString());
            agendaTelefonica.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            AgendaTelefonicaAssertion agendaTelefonicaAssertion = new AgendaTelefonicaAssertion(agendaTelefonica, true);
            if (agendaTelefonicaAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(agendaTelefonicaAssertion.Notifications.Notify());
            }

            _agendaTelefonicaRepository.Add(agendaTelefonica);
            //return CreatedAtRoute("GetApoio", new { id = apoiado.Id }, apoiado);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(agendaTelefonica);
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
            var agendaTelefonicaFounded = _agendaTelefonicaRepository.Find(id);
            if (agendaTelefonicaFounded == null)
            {
                return NotFound();
            }

            AgendaTelefonica agendaTelefonicaNew = new AgendaTelefonica();
            agendaTelefonicaNew = agendaTelefonicaFounded;
            agendaTelefonicaNew.DeserializeJson(body); //Converte Json para o objeto Apoiado
            agendaTelefonicaNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            AgendaTelefonicaAssertion agendaTelefonicaAssertion = new AgendaTelefonicaAssertion(agendaTelefonicaNew);
            if (agendaTelefonicaAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(agendaTelefonicaAssertion.Notifications.Notify());
            }
            _agendaTelefonicaRepository.Update(agendaTelefonicaNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(agendaTelefonicaNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        //[Route("api/[controller]/{id}")]
        public IActionResult Delete(System.Guid id)
        {
            var agendaTelefonica = _agendaTelefonicaRepository.Find(id);
            if (agendaTelefonica == null)
            {
                return NotFound();
            }

            _agendaTelefonicaRepository.Remove(id);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(agendaTelefonica);
        }
    }
}

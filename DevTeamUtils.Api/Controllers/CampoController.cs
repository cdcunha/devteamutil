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
        private readonly IProjetoRepository _projetoRepository;

        public CampoController(MongoDbContext context)
        {
            _projetoRepository = context.GetProjetoRepository();
        }

        [HttpGet("api/[controller]/{id}/{index}", Name = "GetCampo")]
        public IActionResult GetById(Guid id, int indTabela, int index)
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

                if ((item.Tabelas == null) || (item.Tabelas.Count <= 0))
                {
                    var error = new
                    {
                        value = "Não existem tabelas",
                        status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                    };
                    Response.StatusCode = error.status;
                    return new ObjectResult(error);
                }

                if (item.Tabelas.Count -1 < indTabela)
                {
                    var error = new
                    {
                        value = "Índice da tabela é inválido",
                        status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                    };
                    Response.StatusCode = error.status;
                    return new ObjectResult(error);
                }

                if ((item.Tabelas[indTabela].Campos == null) || (item.Tabelas[indTabela].Campos.Count <= 0))
                {
                    var error = new
                    {
                        value = "Não existem campos",
                        status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                    };
                    Response.StatusCode = error.status;
                    return new ObjectResult(error);
                }

                if (item.Tabelas[indTabela].Campos.Count - 1 < index)
                {
                    var error = new
                    {
                        value = "Índice da tabela é inválido",
                        status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                    };
                    Response.StatusCode = error.status;
                    return new ObjectResult(error);
                }

                return new ObjectResult(item.Tabelas[indTabela].Campos[index]);
            }
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
    }
}

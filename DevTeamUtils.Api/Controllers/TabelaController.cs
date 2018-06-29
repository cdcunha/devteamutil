using DevTeamUtils.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class TabelaController : Controller
    {
        private readonly IProjetoRepository _projetoRepository;

        public TabelaController(MongoDbContext context)
        {
            _projetoRepository = context.GetProjetoRepository();
        }

        [HttpGet("api/[controller]/{id}/{index}", Name = "GetTabela")]
        public IActionResult GetById(Guid id, int index)
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

                if (item.Tabelas.Count -1 < index)
                {
                    var error = new
                    {
                        value = "Índice da tabela é inválido",
                        status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError
                    };
                    Response.StatusCode = error.status;
                    return new ObjectResult(error);
                }

                return new ObjectResult(item.Tabelas[index]);
            }
        }
    }
}

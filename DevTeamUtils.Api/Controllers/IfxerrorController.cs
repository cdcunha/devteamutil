using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class IfxerrorController : Controller
    {
        private readonly IIfxerroRepository _ifxerroRepository;
        private readonly MongoDbContext _context;

        public IfxerrorController(MongoDbContext context)
        {
            _ifxerroRepository = context.GetIfxerroRepository();
            _context = context;
        }

        [HttpGet("api/[controller]")]
        [EnableCors("AllowAll")]
        public IEnumerable<IfxError> GetAll()
        {
            return _ifxerroRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetIfxErro")]
        public IActionResult GetById(System.Guid id)
        {
            if (id == System.Guid.Empty)
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
                var item = _ifxerroRepository.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpGet("api/[controller]/{code}", Name = "GetIfxErroByCode")]
        public IActionResult GetByCode(int code)
        {
            var item = _ifxerroRepository.Find(code);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet("api/[controller]/import", Name = "ImportIfxErro")]
        public IActionResult Import(string pathAndFile)
        {
            _ifxerroRepository.Import(pathAndFile);
            return Ok();
        }
    }
}

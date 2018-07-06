using DevTeamUtils.Api.Assertions;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Compression;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class PassoController : Controller
    {
        private readonly IPassoRepository _passoRepository;
        private readonly MongoDbContext _context;

        public PassoController(MongoDbContext context)
        {
            _passoRepository = context.GetPassoRepository();
            _context = context;
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
            Passo passo = new Passo();
            passo.DeserializeJson(body); //Converte Json para o objeto 
            //Passo passo = body.ToObject<Passo>();

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
            FileStreamResult response;
            List<System.IO.MemoryStream> streams = new List<System.IO.MemoryStream>();
            
            var passo = _passoRepository.Find(id);
            if (passo == null)
            {
                return NotFound();
            }
            else
            {
                ArquivoPassoAssertion arquivoPassoAssertion = new ArquivoPassoAssertion();
                arquivoPassoAssertion.CheckPassoValidadoAssertion(passo.Validado);

                var passosGerados = new PassoGeradoController(_context).GetAllByPasso(id);
                foreach (PassoGerado item in passosGerados)
                {
                    //Verifica se há inconsistência nos dados
                    arquivoPassoAssertion.CheckPassoAssertion(item.Passo);

                    streams.Add(_passoRepository.CreateFile(item.Passo));
                }

                #region Verifica inconsistências
                if (arquivoPassoAssertion.Notifications.HasNotifications())
                {
                    Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                    return new ObjectResult(arquivoPassoAssertion.Notifications.Notify());
                }
                #endregion

                #region Lê lista de streams e compacta
                using (System.IO.MemoryStream streamGZip = new System.IO.MemoryStream())
                {
                    foreach (System.IO.MemoryStream stream in streams)
                    {
                        using (GZipStream compressionStream = new GZipStream(stream, CompressionMode.Compress))
                        {
                            streamGZip.CopyTo(compressionStream);

                        }
                    }

                    response = File(streamGZip, "text/plain"); // FileStreamResult
                }
                #endregion
            }

            return response;
        }
    }
}

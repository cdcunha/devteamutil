using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using DevTeamUtils.Api.Assertions;
using DevTeamUtils.Api.Models;
using DevTeamUtils.Api.Repository;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Controllers
{
    [Controller]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(MongoDbContext context)
        {
            _userRepository = context.GetUserRepository();
        }

        [HttpGet("api/[controller]")]
        [EnableCors("AllowAll")]
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("api/[controller]/{id}", Name = "GetUser")]
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
                var user = _userRepository.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                return new ObjectResult(user);
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
            User user = new User();//(((JValue)body.SelectToken("nome")).Value.ToString());
            user.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            UserAssertion userAssertion = new UserAssertion(user, true);
            if (userAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(userAssertion.Notifications.Notify());
            }

            _userRepository.Add(user);
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created;
            return new ObjectResult(user);
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
            var userFounded = _userRepository.Find(id);
            if (userFounded == null)
            {
                return NotFound();
            }

            User userNew = new User();
            userNew = userFounded;
            userNew.DeserializeJson(body); //Converte Json para o objeto Apoiado
            userNew.SetDataAlteracao();

            //Verifica se há inconsistência nos dados
            UserAssertion userAssertion = new UserAssertion(userNew);
            if (userAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(userAssertion.Notifications.Notify());
            }
            _userRepository.Update(userNew);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(userNew);
        }

        [HttpDelete("api/[controller]/{id}")]
        [EnableCors("AllowAll")]
        public IActionResult Delete(Guid id)
        {
            var user = _userRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Remove(id);
            //return new NoContentResult();
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(user);
        }

        [HttpPost("api/[controller]/Login")]
        [EnableCors("AllowAll")]
        public IActionResult Login([FromBody]JObject body)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            DTO.LoginDTO loginDTO = new DTO.LoginDTO();//(((JValue)body.SelectToken("nome")).Value.ToString());
            loginDTO.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            LoginAssertion loginAssertion = new LoginAssertion(loginDTO, true);
            if (loginAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(loginAssertion.Notifications.Notify());
            }

            var user = _userRepository.Login(loginDTO);
            if (user == null)
            {
                return NotFound();
            }
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(user);
        }

        [HttpPost("api/[controller]/Logout")]
        [EnableCors("AllowAll")]
        public IActionResult Logout([FromBody]JObject body)
        {
            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            DTO.LogoutDTO logoutDTO = new DTO.LogoutDTO();//(((JValue)body.SelectToken("nome")).Value.ToString());
            logoutDTO.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            LogoutAssertion logoutAssertion = new LogoutAssertion(logoutDTO, true);
            if (logoutAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(logoutAssertion.Notifications.Notify());
            }

            _userRepository.Logout(logoutDTO);

            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(logoutDTO);
        }

        [HttpPost("api/[controller]/online")]
        [EnableCors("AllowAll")]
        public IEnumerable<User> GetOnlineUsers([FromBody]JObject body)
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
                var user = _userRepository.Find(id);
                if (user == null)
                {
                    return NotFound();
                }
                return new ObjectResult(user);
            }


            if (string.IsNullOrEmpty(body.ToString()))
            {
                return BadRequest();
            }
            DTO.LoginDTO loginDTO = new DTO.LoginDTO();//(((JValue)body.SelectToken("nome")).Value.ToString());
            loginDTO.DeserializeJson(body); //Converte Json para o objeto Apoiado

            //Verifica se há inconsistência nos dados
            LoginAssertion loginAssertion = new LoginAssertion(loginDTO, true);
            if (loginAssertion.Notifications.HasNotifications())
            {
                Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                return new ObjectResult(loginAssertion.Notifications.Notify());
            }

            var user = _userRepository.Login(loginDTO);
            if (user == null)
            {
                return NotFound();
            }
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            return new ObjectResult(user);

            return _userRepository.GetAll();
        }
    }
}

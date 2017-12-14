using DevTeamUtils.DB.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DevTeamUtils.DB.Api.Controllers
{
    public class ConnectionController : ApiController
    {   
        // GET: api/Connection/5
        public async Task<string> Get(string connectionString)
        {
            return await Connection.TestConnection(connectionString);
        }
    }
}

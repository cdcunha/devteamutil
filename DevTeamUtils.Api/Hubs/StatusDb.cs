using DevTeamUtils.Api.Assertions;
using DevTeamUtils.Api.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DevTeamUtils.Api.Hubs
{
    public class StatusDb : Hub
    {
        private readonly IConexaoRepository _userRepository;
        private readonly IConfiguration _configuration;

        public StatusDb(MongoDbContext context, IConfiguration configuration)
        {
            _userRepository = context.GetConexaoRepository();
            _configuration = configuration;
        }

        /*public override async Task OnConnectedAsync()
        {
            //await Clients.Client(Context.ConnectionId).InvokeAsync("SetUsersOnline", await GetUsersOnline());

            await base.OnConnectedAsync();
        }*/

        public async Task GetStatusDB(Guid id)
        {
            
            Models.Conexao conexao = _userRepository.Find(id);
            if (conexao != null)
            {
                TestConnectionAssertion testConnectionAssertion = new TestConnectionAssertion(
                    conexao.Ip, conexao.Porta, conexao.NomeServidor,
                    conexao.Usuario, conexao.Senha
                    );
                if (!testConnectionAssertion.Notifications.HasNotifications())
                {
                    string connectionString = Utils.Connection.GetConnectionString(conexao);
                    string status = Utils.Connection.TestConnection(connectionString, _configuration["UrlStatusDb"]);
                    
                    conexao.DataStatus = DateTime.Now;
                    conexao.Status = status;

                    _userRepository.Update(conexao);
                    await Clients.All.InvokeAsync("StatusDBResponse", conexao.Id, conexao.Status, conexao.DataStatus.ToString());
                }
            }   
        }

        
    }
}

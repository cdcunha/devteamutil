using DevTeamUtils.Api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using DevTeamUtils.Api.DTO;

namespace DevTeamUtils.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
            /*var resultado = _context.Users.Find(FilterDefinition<User>.Empty).Skip(1);
            if (!resultado.Any())
                Add(new User());
            */
        }

        public void Add(User user)
        {
            _context.Users.InsertOne(user);

            //_context.Users.Add(apoiado);
            //_context.SaveChanges();
        }

        public User Find(Guid id)
        {
            var resultado = _context.Users.Find(Builders<User>.Filter.Eq("_id", id)).FirstOrDefault();
            return resultado;
        }

        public IEnumerable<User> GetAll()
        {
            var resultado = _context.Users.Find(FilterDefinition<User>.Empty).SortBy(it => it.Nome);//.Skip(0).Limit(50)
            return resultado.ToList();
        }

        public void Remove(Guid id)
        {
            _context.Users.DeleteOne(Builders<User>.Filter.Eq(p => p.Id, id));

            //var apoiado = _context.Users.First(t => t.Id == id);
            //_context.Users.Remove(apoiado);
            //_context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.ReplaceOne(Builders<User>.Filter.Eq(p => p.Id, user.Id), user);
            //_context.Users.Update(apoiado);
            //_context.SaveChanges();
        }

        private User CsvToUser(string csvLine)
        {
            string[] values = csvLine.Split(',');
            User user = new User();

            user.Nome = values[0];
            user.Apelido = values[1];
            user.Senha = values[2];
            user.Email = values[3];
            //user.Online = values[4];
            //conexao.DataStatus = values[8];

            return user;
        }

        public void Import(string pathAndFile)
        {
            List<User> users = System.IO.File.ReadAllLines(pathAndFile)
                                           .Skip(1)
                                           .Select(v => CsvToUser(v))
                                           .ToList();
            foreach (User item in users)
            {
                Add(item);
            }
        }

        public IEnumerable<OnlineUserDTO> GetAllOnlineUsers()
        {
            IEnumerable<User> users = _context.Users.Find(Builders<User>.Filter.Eq("Online", true)).ToList();

            List<OnlineUserDTO> onlineUsers = new List<OnlineUserDTO>();
            foreach (User user in users)
            {
                if (user.Online)
                {
                    OnlineUserDTO item = new OnlineUserDTO();
                    item.Apelido = user.Apelido;
                    item.ConnectionId = user.ConnectionId;

                    onlineUsers.Add(item);
                }
                
            }

            return onlineUsers;
        }

        public User Login(LoginDTO loginDTO)
        {   
            var builder = Builders<User>.Filter;
            var filter = builder.And(builder.Eq(e => e.Apelido, loginDTO.Apelido),
                                     builder.Eq(e => e.Senha, loginDTO.Senha));
            var resultado = _context.Users.Find(filter).FirstOrDefault();

            if (resultado != null)
            {
                resultado.Online = true;
                Update(resultado);
            }

            return resultado;
        }

        public void Logout(LogoutDTO logoutDTO)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq("apelido", logoutDTO.Apelido) & builder.Eq("connectionId", logoutDTO.ConnectionId);
            var resultado = _context.Users.Find(filter).FirstOrDefault();

            if (resultado != null)
            {
                resultado.Online = false;
                resultado.ConnectionId = "";
                Update(resultado);
            }
        }
    }
}

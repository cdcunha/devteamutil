using DevTeamUtils.Api.DTO;
using DevTeamUtils.Api.Models;
using System;
using System.Collections.Generic;

namespace DevTeamUtils.Api.Repository
{
    public interface IUserRepository
    {
        void Add(User contato);
        IEnumerable<User> GetAll();
        User Find(Guid id);
        void Remove(Guid id);
        void Update(User contato);
        void Import(string pathAndFile);
        IEnumerable<OnlineUserDTO> GetAllOnlineUsers();
        User Login(LoginDTO loginDTO);
        void Logout(LogoutDTO logoutDTO);
    }
}

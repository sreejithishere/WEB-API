using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Dtos;
using WEB_API.Models;

namespace WEB_API.Interface
{
    public interface IUser
    {
        Task<User> Authenticate(string userName, string password);
        void Register(string userName, string password);

        Task<bool> UserAlreadyExists(string userName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Interface
{
    public interface IUnitOfWork
    {
        ICityRepository cityRepository { get; }
        IUser userRepository { get; }
        Task<bool> SaveAsync();
    }
}

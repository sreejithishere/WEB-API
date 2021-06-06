using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Context;
using WEB_API.Dtos;
using WEB_API.Interface;
using WEB_API.Models;

namespace WEB_API.Repos
{
    public class UserRepository : IUser
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;

        }
        public async Task<User> Authenticate(LoginReqDto loginReqDto)
        {

            return await _dataContext.Users.FirstOrDefaultAsync(item => item.UserName == loginReqDto.UserName && item.Password == loginReqDto.PassWord);
        }
    }
}

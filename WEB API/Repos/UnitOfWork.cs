using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Context;
using WEB_API.Interface;

namespace WEB_API.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dc;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext dc,IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }
        public ICityRepository cityRepository => new CityRepository(_dc,_mapper);

        public async Task<bool> SaveAsync()
        {
            return await _dc.SaveChangesAsync() > 0;
        }
    }
}

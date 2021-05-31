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
        public UnitOfWork(DataContext dc)
        {
            _dc = dc;
        }
        public ICityRepository cityRepository => new CityRepository(_dc);

        public async Task<bool> SaveAsync()
        {
            return await _dc.SaveChangesAsync() > 0;
        }
    }
}

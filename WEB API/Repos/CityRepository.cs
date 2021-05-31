using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Context;
using WEB_API.Models;

namespace WEB_API.Interface
{
    public class CityRepository:ICityRepository
    {
        private readonly DataContext _dataContext;
        public CityRepository(DataContext dc)
        {
            _dataContext = dc;
        }
       public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _dataContext.Cities.ToListAsync();

        }
        public async Task AddCityAsync(City city)
        {
            await _dataContext.AddAsync(city);


        }
        public async Task DeleteCityAsync(int cityId)
        {
            var city = await _dataContext.Cities.FindAsync(cityId);
            _dataContext.Remove(city);
        }
    }
}

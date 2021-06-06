using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Context;
using WEB_API.Dtos;
using WEB_API.Models;

namespace WEB_API.Interface
{
    public class CityRepository:ICityRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public CityRepository(DataContext dc, IMapper mapper)
        {
            _dataContext = dc;
            _mapper = mapper;
        }
       public async Task<IEnumerable<CityDto>> GetCitiesAsync()
        {
            var city= await _dataContext.Cities.ToListAsync();
            var cityDto =  _mapper.Map <IEnumerable<CityDto>>(city);
            return cityDto;
        }

        public async Task AddCityAsync(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            await _dataContext.AddAsync(city);
        }
        public async Task DeleteCityAsync(int cityId)
        {
            var city = await _dataContext.Cities.FindAsync(cityId);
            _dataContext.Remove(city);
        }

        public async Task<City> FindAsync(int id)
        {
            return await _dataContext.Cities.FindAsync(id);
        }
        public async Task UpdateCityAsync(City city, CityDto cityDto)
        {
            city.LastUpdatedOn = DateTime.Now;
            city.LastUpdatedBy = "Sree";
            var cityM =   _mapper.Map(cityDto, city);
            await Task.CompletedTask;        
        }
    }
}

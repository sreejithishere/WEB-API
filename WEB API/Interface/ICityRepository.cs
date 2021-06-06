using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Dtos;
using WEB_API.Models;

namespace WEB_API.Interface
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityDto>> GetCitiesAsync();
        Task AddCityAsync(CityDto cityDto);
        Task DeleteCityAsync(int cityId);
        Task<City> FindAsync(int id);
        Task UpdateCityAsync(City city,CityDto cityDto);
    }
}

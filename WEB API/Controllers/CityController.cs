using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB_API.Models;
using WEB_API.Repos;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        public CityController(ICityRepository cr)
        {
            _cityRepository = cr;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _cityRepository.GetCitiesAsync();
            return Ok(cities);
        }

        //[HttpPost("addCity")]
        //[HttpPost("addCity/{name}")] 
        //public async Task<IActionResult> AddCities(string name)
        //{
        //    City city = new City() { Name = name };
        //    await _dataContext.AddAsync(city);
        //    await _dataContext.SaveChangesAsync();
        //    return Ok(city);
        //}

        [HttpPost("addCity")]
        public async Task<IActionResult> AddCities(City city)
        {
            await _cityRepository.AddCityAsync(city);
            await _cityRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("deleteCity/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _cityRepository.DeleteCityAsync(id);
            await _cityRepository.SaveAsync();
            return Ok(id);
        }
    }
}

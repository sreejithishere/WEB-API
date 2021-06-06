using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB_API.Models;
using WEB_API.Interface;
using WEB_API.Dtos;
using System;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        public CityController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _uow.cityRepository.GetCitiesAsync();
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
        public async Task<IActionResult> AddCities(CityDto cityDto)
        {
 
            await _uow.cityRepository.AddCityAsync(cityDto);
            await _uow.SaveAsync();
            return Ok();
        }
        [HttpPut("updateCity/{id}")]
        public async Task<IActionResult> UpdateCity(int id,CityDto cityDto)
        {
           var cityFromDB=  await _uow.cityRepository.FindAsync(id);
           await _uow.cityRepository.UpdateCityAsync(cityFromDB,cityDto);
           await _uow.SaveAsync();
           return Ok();
        }

        [HttpDelete("deleteCity/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _uow.cityRepository.DeleteCityAsync(id);
            await _uow.SaveAsync();
            return Ok(id);
        }
    }
}

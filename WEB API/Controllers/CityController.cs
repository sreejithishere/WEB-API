using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Context;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public CityController(DataContext dc)
        {
            _dataContext = dc;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _dataContext.Cities.ToListAsync();
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
            await _dataContext.AddAsync(city);
            await _dataContext.SaveChangesAsync();
            return Ok(city);
        }

        [HttpDelete("deleteCity/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _dataContext.Cities.FindAsync(id);
            _dataContext.Remove(city);
            await _dataContext.SaveChangesAsync();
            return Ok(id);
        }
    }
}

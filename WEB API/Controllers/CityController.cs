using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Context;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext dataContext;
        public CityController(DataContext dc)
        {
            dataContext = dc;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cities = dataContext.Cities.ToList();
            return Ok(cities);
        }
    }
}

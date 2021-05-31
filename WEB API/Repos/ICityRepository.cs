﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_API.Models;

namespace WEB_API.Repos
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task AddCityAsync(City city);
        Task DeleteCityAsync(int cityId);
        Task<bool> SaveAsync();
    }
}
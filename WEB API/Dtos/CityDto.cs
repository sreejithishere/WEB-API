using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage ="Required")]
        public string Country { get; set; }
    }
}

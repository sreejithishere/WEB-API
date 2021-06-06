using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API.Dtos
{
    public class LoginResDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WEB_API.Dtos;
using WEB_API.Errors;
using WEB_API.Extensions;
using WEB_API.Interface;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration configuration;
        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginReqDto loginReqDto)
        {
            var user = await _unitOfWork.userRepository.Authenticate(loginReqDto.UserName, loginReqDto.PassWord);

            ApiError apiError = new ApiError();

            if (user == null)
            {
                apiError.ErrorCode = Unauthorized().StatusCode;
                apiError.ErrorMessage = "Invalid user name or password";
                apiError.ErrorDetails = "This error appear when provided user id or password does not exists";
                return Unauthorized(apiError);
            }

            var loginRes = new LoginResDto();
            loginRes.UserName = user.UserName;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            ApiError apiError = new ApiError();

            if (loginReq.UserName.IsEmpty() || loginReq.PassWord.IsEmpty())
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User name or password can not be blank";
                return BadRequest(apiError);
            }

            if (await _unitOfWork.userRepository.UserAlreadyExists(loginReq.UserName))
            {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User already exists, please try different user name";
                return BadRequest(apiError);
            }

            _unitOfWork.userRepository.Register(loginReq.UserName, loginReq.PassWord);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                //new Claim(ClaimTypes.Role,"User")
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

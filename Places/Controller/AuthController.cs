﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using Places.Data;
using Places.Dto;
using Places.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;

namespace Places.Controller
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PlacesContext _context;

        public AuthController(IConfiguration configuration, PlacesContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
          
            var foundUser = _context.UserProfile.Include(u => u.Company).SingleOrDefault(u => u.Email == userLoginDto.Email);

           if (foundUser == null) { return NotFound(); }
          
                if(userLoginDto.Password != foundUser.Password) { return Unauthorized(); }
                    var tokenString = GenerateJSONWebToken(foundUser);

            return Ok(new
            {
                loggedUser = foundUser,
                token = tokenString
            });

        }

        private string GenerateJSONWebToken(UserProfile user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("PhoneNumber", user.PhoneNumber),
                };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }


}

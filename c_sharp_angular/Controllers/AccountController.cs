using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using c_sharp_angular.Data;
using c_sharp_angular.DTOs;
using c_sharp_angular.Entities;
using c_sharp_angular.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_angular.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        //private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
            ITokenService tokenService, IMapper mapper)
        {
            //_context = context;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser(RegisterDto registerDto)
        {

            if (await UserExists(registerDto.Username))
                return BadRequest("Username is taken");


            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x =>
                x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid credentials");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Ivalid Credentials");

            return Ok(new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,

            });
        }


        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x =>
                x.UserName == username.ToLower());
        }

    }
}


using System;
using System.Security.Claims;
using AutoMapper;
using c_sharp_angular.Data;
using c_sharp_angular.DTOs;
using c_sharp_angular.Entities;
using c_sharp_angular.Extensions;
using c_sharp_angular.Helpers;
using c_sharp_angular.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace c_sharp_angular.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UserController(IUserRepository userRepository,
            IMapper mapper, IPhotoService photoService)
        {
            _repository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        // Task make it async/await
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers(
            [FromQuery] UserParams userParams)
        {

            var currentUser = await _repository.GetUserByUsername(User.GetUsername());
            userParams.CurrentUsername = currentUser.UserName;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = currentUser.Gender == "male" ?
                    "female" : "male";
            }

            var users = await _repository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPages));

            //var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(users);
        }

        // Examples api/users/3
        // Authorize is logic about 
        //[Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _repository.GetMemeberByUsernameAsync(username);

            //return _mapper.Map<MemberDto>(user);
            return await _repository.GetMemeberByUsernameAsync(username);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            // Claim types It'is from Token
            var username = User.GetUsername();
            var user = await _repository.GetUserByUsername(username);
            Console.WriteLine(user);
            if (user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user);

            if (await _repository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var username = User.GetUsername();
            var user = await _repository.GetUserByUsername(username);

            if (user == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0) photo.IsMain = true;

            user.Photos.Add(photo);

            if (await _repository.SaveAllAsync())
            {

                return CreatedAtAction(nameof(GetUser),
                    new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Problem adding photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _repository.GetUserByUsername(User.GetUsername());

            if (user == null) return NotFound();

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("This is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;

            if (await _repository.SaveAllAsync()) return NoContent();

            return BadRequest("Problem setting main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeleteUserPhoto(int photoId)
        {
            var user = await _repository.GetUserByUsername(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You cannot delete main photo");

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);

                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _repository.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting photo in this case");
        }
    }


}


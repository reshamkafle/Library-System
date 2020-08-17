using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LMS.API.Models;
using LMS.Core.Constant;
using LMS.Service.Intefaces;
using LMS.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginViewModel model)
        {
            var mapped = _mapper.Map<LoginDto>(model);
            var response = await _userService.Authenticate(mapped);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModelWithoutPassword>> Get()
        {
            var users = await _userService.GetAll(AuthorizationConstants.Roles.User);
            return _mapper.Map<List<UserViewModelWithoutPassword>>(users);
        }

        [HttpPost("register")]
        public async Task Register(UserViewModel model)
        {
            var mapped = _mapper.Map<UserDto>(model);
            mapped.Roles = new List<string> { AuthorizationConstants.Roles.User };
            await _userService.Register(mapped);          
        }

        [HttpPut("changepassword")]
        public async Task ChangePassword(ChangePasswordViewModel password)
        {
            var mapped = _mapper.Map<ChangePasswordDto>(password);
            await _userService.ChangePassword(mapped);          
        }

        [HttpGet("{id}")]
        public async Task<UserViewModelWithoutPassword> Get(string id)
        {
            var user = await _userService.GetById(id);
            return _mapper.Map<UserViewModelWithoutPassword>(user);
        }

        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] UserViewModelWithoutPassword value)
        {
            var user = _mapper.Map<UserDto>(value);
            user.Id = id;

            await _userService.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _userService.Delete(id);
        }
    }
}

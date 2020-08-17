using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LMS.API.Models;
using LMS.Core.Constant;
using LMS.Service.Identity;
using LMS.Service.Intefaces;
using LMS.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        private IUserService _userService;
        private readonly IMapper _mapper;

        public EmployeeController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var users = await _userService.GetAll(AuthorizationConstants.Roles.ADMINISTRATORS);
            return _mapper.Map<List<UserViewModel>>(users);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task Post([FromBody] UserViewModel value)
        {
            var user = _mapper.Map<UserDto>(value);
            user.Roles = new List<string> { AuthorizationConstants.Roles.ADMINISTRATORS };
            await _userService.Register(user);
        }
        
    }
}

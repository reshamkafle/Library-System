using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LMS.API.Models;
using LMS.Core.Constant;
using LMS.Core.Entities;
using LMS.Service.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationConstants.Roles.User)]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService  _checkoutService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public CheckoutController(
            ICheckoutService checkoutService , 
            IMapper mapper,
            IUserService userService)
        {
            _checkoutService = checkoutService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<HistoryViewModel>> Get()
        {
            var logginUserId = this.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
            var libraryNumber = await _userService.GetLibraryNumber(logginUserId);

            var checkout = await _checkoutService.GetCheckout(libraryNumber);
            return _mapper.Map<List<HistoryViewModel>>(checkout);
        }

        // POST api/<CheckoutController>
        [HttpPost]
        public async Task Post([FromBody] CheckoutViewModel value)
        {
            var checkout = _mapper.Map<Checkout>(value);
            var logginUserId = this.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
            checkout.CheckoutBy = logginUserId;
            checkout.LibraryNumber = await  _userService.GetLibraryNumber(logginUserId);
            await _checkoutService.Checkout(checkout);
        }
    }
}

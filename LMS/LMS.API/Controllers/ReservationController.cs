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
using LMS.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationConstants.Roles.User)]

    public class ReservationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IReservationService _reservationService;

        public ReservationController(
           IReservationService reservationService,
           IMapper mapper,
           IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IEnumerable<HistoryViewModel>> Get()
        {
            var logginUserId = this.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
            var LibraryCardNumber = await _userService.GetLibraryNumber(logginUserId);
            var reservations = await _reservationService.GetReservation(LibraryCardNumber);
            return _mapper.Map<List<HistoryViewModel>>(reservations);
        }


        [HttpPost]
        public async Task Post([FromBody] ReservationViewModel value)
        {
            var reservation = _mapper.Map<Reservation>(value);
            var logginUserId = this.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
            reservation.LibraryCardNumber = await _userService.GetLibraryNumber(logginUserId);

            await _reservationService.Reserve(reservation);
        }

    }
}

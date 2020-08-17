using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LMS.API.Models;
using LMS.Core.Constant;
using LMS.Core.Entities;
using LMS.Service.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;

        public MediaController(IMediaService mediaService, IMapper mapper)
        {
            _mediaService = mediaService;
            _mapper = mapper;
        }

        // GET: api/<MediaController>
        [HttpGet]
        [Authorize(Roles = AuthorizationConstants.Roles.User + "," + AuthorizationConstants.Roles.ADMINISTRATORS)]

        public async Task<IEnumerable<MediaViewModel>> Get()
        {
            var medias = await _mediaService.GetMedias();
            return _mapper.Map<List<MediaViewModel>>(medias);
        }

        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        // GET api/<MediaController>/5
        [HttpGet("{id}")]
        public async Task<MediaViewModel> Get(string id)
        {
            var media = await _mediaService.GetMedia(id);
            return _mapper.Map<MediaViewModel>(media);
        }

        // POST api/<MediaController>
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        [HttpPost]
        public async Task Post(MediaViewModel value)
        {
            var media = _mapper.Map<Media>(value);
            media.Publisher = null;
            await _mediaService.AddMedia(media);
        }


        // PUT api/<MediaController>/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] MediaViewModel value)
        {
            var media = _mapper.Map<Media>(value);
            media.Id = id;
            await _mediaService.UpdateMedia(media);
        }

        // DELETE api/<MediaController>/5
        [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _mediaService.Delete(id);
        }
    }
}

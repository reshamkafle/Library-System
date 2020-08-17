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
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]

    public class MediaTypeController : ControllerBase
    {
        private readonly IMediaTypeService _mediaTypeService;
        private readonly IMapper _mapper;

        public MediaTypeController(IMediaTypeService mediaTypeService, IMapper mapper)
        {
            _mediaTypeService = mediaTypeService;
            _mapper = mapper;
        }

        // GET: api/<MediaTypeController>
        [HttpGet]
        public async Task<IEnumerable<MediaTypeViewModel>> Get()
        {
            var mediaTypes = await _mediaTypeService.GetMediaTypes();
            return _mapper.Map<List<MediaTypeViewModel>>(mediaTypes);
        }

        // GET api/<MediaTypeController>/5
        [HttpGet("{id}")]
        public async Task<MediaTypeViewModel> Get(string id)
        {
            var mediaType = await _mediaTypeService.GetMediaType(id);
            return _mapper.Map<MediaTypeViewModel>(mediaType);
        }

        // POST api/<MediaTypeController>
        [HttpPost]
        public async Task Post([FromBody] MediaTypeViewModel value)
        {
            var mediaType = _mapper.Map<MediaType>(value);
            await _mediaTypeService.AddMediaType(mediaType);
        }

        // PUT api/<MediaTypeController>/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] MediaTypeViewModel value)
        {
            var mediaType = _mapper.Map<MediaType>(value);
            mediaType.Id = id;
            await _mediaTypeService.UpdateMediaType(mediaType);
        }

        // DELETE api/<MediaTypeController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _mediaTypeService.DeleteType(id);
        }
    }
}

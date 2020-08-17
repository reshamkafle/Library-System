using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AutoMapper;
using LMS.API.Models;
using LMS.Core.Constant;
using LMS.Service.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]

    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }
        // GET: api/<PublisherController>
        [HttpGet]
        public async Task<IEnumerable<PublisherViewModel>> Get()
        {
            var publishers = await _publisherService.GetPublishers();
            return _mapper.Map<List<PublisherViewModel>>(publishers);
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public async Task<PublisherViewModel> Get(string id)
        {
            var publisher = await _publisherService.GetPublisher(id);
            return _mapper.Map<PublisherViewModel>(publisher);
        }

        // POST api/<PublisherController>
        [HttpPost]
        public async Task Post([FromBody] PublisherViewModel value)
        {
            var publisher = _mapper.Map<LMS.Core.Entities.Publisher>(value);
            await _publisherService.AddPublisher(publisher);
        }

        // PUT api/<PublisherController>/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] PublisherViewModel value)
        {
            var publisher = _mapper.Map<LMS.Core.Entities.Publisher>(value);
            publisher.Id = id;
            await _publisherService.UpdatePublisher(publisher);
        }

        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _publisherService.DeletePublisher(id);
        }
    }
}

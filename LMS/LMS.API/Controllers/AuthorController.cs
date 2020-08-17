using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<IEnumerable<AuthorViewModel>> Get()
        {
            var authors = await _authorService.GetAuthors();
            return _mapper.Map<List<AuthorViewModel>>(authors);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<AuthorViewModel> Get(string id)
        {
            var author = await _authorService.GetAuthor(id);
            return _mapper.Map<AuthorViewModel>(author);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task Post(AuthorViewModel value)
        {
            var mapped = _mapper.Map<Author>(value);
            await _authorService.AddAuthor(mapped);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task Put(string id, AuthorViewModel value)
        {
            var mapped = _mapper.Map<Author>(value);
            mapped.Id = id;
            await _authorService.UpdateAuthor(mapped);
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _authorService.DeleteAuthor(id);
        }
    }
}

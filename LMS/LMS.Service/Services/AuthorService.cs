using LMS.Core.Constant;
using LMS.Core.Entities;
using LMS.Service.Data;
using LMS.Service.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LMSContext _context;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(LMSContext context, ILogger<AuthorService> logger )
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddAuthor(Author author)
        {
            try
            {
                author.Id = Guid.NewGuid().ToString();

                _context.Author.Add(author);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Add Author {0}", MessageConstant.RecordSuccessful);

            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task DeleteAuthor(string authorId)
        {
            try
            {
                var author = await _context.Author.Where(w => w.Id == authorId).FirstOrDefaultAsync();

                if (author != null)
                {

                    _context.Remove(author);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Delete Author {0}", MessageConstant.RecordDeleteSuccessful);

                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task<Author> GetAuthor(string authorId)
        {
            return await _context.Author.Where(w => w.Id == authorId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task UpdateAuthor(Author author)
        {
            try
            {
                var myAuthor = await _context.Author.Where(w => w.Id == author.Id).FirstOrDefaultAsync();

                if (myAuthor != null)
                {

                    myAuthor.FirstName = author.FirstName;
                    myAuthor.LastName = author.LastName;

                    _context.Update(myAuthor);

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Update Author {0}", MessageConstant.RecordSuccessful);
                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }
    }
}

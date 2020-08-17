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
    public class MediaAuthorService : IMediaAuthorService
    {
        private readonly LMSContext _context;
        private readonly ILogger<MediaAuthorService> _logger;

        public MediaAuthorService(LMSContext context, ILogger<MediaAuthorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddMediaAuthor(MediaAuthor media)
        {
            try
            {
                _context.MediaAuthor.Add(media);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Add Media Author {0}", MessageConstant.RecordSuccessful);

            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

       public async Task DeleteAuthor(string mediaId, string authorId)
       {
            try
            {
                var myMediaAuthor = await _context.MediaAuthor.Where(w => w.AuthorId == authorId && w.BookId == mediaId).FirstOrDefaultAsync();

                if (myMediaAuthor != null)
                {

                    _context.Remove(myMediaAuthor);

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Delete Media Author {0}", MessageConstant.RecordSuccessful);
                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }
    }
}

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
    public class MediaTypeService : IMediaTypeService
    {
        private readonly LMSContext _context;
        private readonly ILogger<MediaTypeService> _logger;

        public MediaTypeService(LMSContext context, ILogger<MediaTypeService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddMediaType(MediaType mediaType)
        {
            try
            {
                mediaType.Id = Guid.NewGuid().ToString();

                _context.MediaType.Add(mediaType);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Add Media Type {0}", MessageConstant.RecordSuccessful);

            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task DeleteType(string mediaTypeId)
        {
            try
            {
                var mediaType = await _context.MediaType.Where(w => w.Id == mediaTypeId).FirstOrDefaultAsync();

                if (mediaType != null)
                {

                    _context.Remove(mediaType);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Delete Media Type {0}", MessageConstant.RecordDeleteSuccessful);

                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task<MediaType> GetMediaType(string mediaTypeId)
        {
            return await _context.MediaType.Where(w => w.Id == mediaTypeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MediaType>> GetMediaTypes()
        {
            return await _context.MediaType.ToListAsync();
        }

        public async Task UpdateMediaType(MediaType mediaType)
        {
            try
            {
                var myMediaType = await _context.MediaType.Where(w => w.Id == mediaType.Id).FirstOrDefaultAsync();

                if (myMediaType != null)
                {

                    myMediaType.Name = mediaType.Name;

                    _context.Update(myMediaType);

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Update Media Type {0}", MessageConstant.RecordSuccessful);
                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }
    }
}

using LMS.Core.Constant;
using LMS.Core.Entities;
using LMS.Service.Data;
using LMS.Service.Intefaces;
using LMS.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Services
{
    public class MediaService : IMediaService
    {
        private readonly LMSContext _context;
        private readonly ILogger<MediaService> _logger;

        public MediaService(LMSContext context, ILogger<MediaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddMedia(Media media)
        {
            try
            {
                media.Id = Guid.NewGuid().ToString();
                _context.Media.Add(media);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Add Media {0}", MessageConstant.RecordSuccessful);

            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task Delete(string mediaId)
        {
            try
            {
                var media = await _context.Media.Where(w => w.Id == mediaId).FirstOrDefaultAsync();

                if (media != null)
                {
                    _context.Remove(media);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Delete Media {0}", MessageConstant.RecordDeleteSuccessful);
                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task<Media> GetMedia(string mediaId)
        {
            return await _context.Media.Where(w => w.Id == mediaId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MediaDto>> GetMedias()
        {
            return await (from c in _context.Media
                          join d in _context.Author on c.AuthorId equals d.Id
                          join e in _context.Publisher on c.PublisherId equals e.Id
                          join f in _context.MediaType on c.MediaTypeId equals f.Id
                          select new MediaDto
                          {
                                Id = c.Id,
                                Title = c.Title,
                                AuthorName = d.LastName + " " + d.FirstName,
                                ISBN = c.ISBN,
                                Year = c.Year,
                                Cost =c.Cost,
                                PublisherName = e.Name,
                                MediaTypeName = f.Name,
                                TotalQty = c.TotalQty,
                                BalanceQty= (c.TotalQty - (from h in _context.Checkout where h.MediaId == c.Id select c).Count())
                          }).ToListAsync();
        }

        public async Task UpdateMedia(Media media)
        {
            try
            {
                var myMedia = await _context.Media.Where(w => w.Id == media.Id).FirstOrDefaultAsync();

                if (myMedia != null)
                {

                    myMedia.AuthorId = media.AuthorId;
                    myMedia.Cost = media.Cost;
                    myMedia.ISBN = media.ISBN;
                    myMedia.MediaTypeId = media.MediaTypeId;
                    myMedia.PublisherId = media.PublisherId;
                    myMedia.Title = media.Title;
                    myMedia.Year = media.Year;
                    myMedia.TotalQty = media.TotalQty;

                    _context.Update(myMedia);

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Update Media {0}", MessageConstant.RecordSuccessful);
                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }
    }
}

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
    public class PublisherService : IPublisherService
    {
        private readonly LMSContext _context;
        private readonly ILogger<PublisherService> _logger;

        public PublisherService(LMSContext context, ILogger<PublisherService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddPublisher(Publisher publisher)
        {
            try
            {
                publisher.Id = Guid.NewGuid().ToString();

                _context.Publisher.Add(publisher);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Add Publisher {0}", MessageConstant.RecordSuccessful);

            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task DeletePublisher(string publisherId)
        {
            try
            {
                var publisher = await _context.Publisher.Where(w => w.Id == publisherId).FirstOrDefaultAsync();

                if (publisher != null)
                {
                    _context.Remove(publisher);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Delete Publisher {0}", MessageConstant.RecordDeleteSuccessful);
                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }

        public async Task<Publisher> GetPublisher(string publisherId)
        {
            return await _context.Publisher.Where(w => w.Id == publisherId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            return await _context.Publisher.ToListAsync();
        }

        public async Task UpdatePublisher(Publisher publisher)
        {
            try
            {
                var myPublisher = await _context.Publisher.Where(w => w.Id == publisher.Id).FirstOrDefaultAsync();

                if (myPublisher != null)
                {

                    myPublisher.Name = publisher.Name;

                    _context.Update(myPublisher);

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Update Publisher {0}", MessageConstant.RecordSuccessful);
                }
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }
    }
}

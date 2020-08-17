using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Intefaces
{
    public interface IPublisherService
    {
        Task AddPublisher(Publisher publisher);
        Task UpdatePublisher(Publisher publisher);
        Task<Publisher> GetPublisher(string publisherId);
        Task<IEnumerable<Publisher>> GetPublishers();
        Task DeletePublisher(string publisherId);
    }
}

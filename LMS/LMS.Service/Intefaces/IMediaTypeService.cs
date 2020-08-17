using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Intefaces
{
    public interface IMediaTypeService
    {
        Task AddMediaType(MediaType mediaType);
        Task UpdateMediaType(MediaType mediaType);
        Task<MediaType> GetMediaType(string mediaTypeId);
        Task<IEnumerable<MediaType>> GetMediaTypes();
        Task DeleteType(string mediaTypeId);
    }
}

using LMS.Core.Entities;
using LMS.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Intefaces
{
    public interface IMediaService
    {
        Task AddMedia(Media media);
        Task UpdateMedia(Media media);
        Task<Media> GetMedia(string mediaId);
        Task<IEnumerable<MediaDto>> GetMedias();
        Task Delete(string mediaId);

    }
}

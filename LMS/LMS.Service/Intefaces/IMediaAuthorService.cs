using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Intefaces
{
    public interface IMediaAuthorService
    {
        Task AddMediaAuthor(MediaAuthor media);
        Task DeleteAuthor(string mediaId, string authorId);
    }
}

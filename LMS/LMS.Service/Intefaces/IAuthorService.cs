using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Intefaces
{
    public interface IAuthorService
    {
        Task AddAuthor(Author author);
        Task UpdateAuthor(Author author);
        Task<Author> GetAuthor(string authorId);
        Task<IEnumerable<Author>> GetAuthors();
        Task DeleteAuthor(string authorId);
    }
}

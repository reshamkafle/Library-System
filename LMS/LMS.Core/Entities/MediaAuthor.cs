using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class MediaAuthor
    {
        public Media Book { get; set; }
        public string BookId { get; set; }
        public Author Author { get; set; }
        public string AuthorId { get; set; }
    }
}

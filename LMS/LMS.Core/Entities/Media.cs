using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LMS.Core.Entities
{
    public class Media: BaseEntity
    {
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public decimal Cost { get; set; }
        public string MediaTypeId { get; set; }
        public int TotalQty { get; set; }
        public ICollection<MediaAuthor> Book_Author { get; set; }

    }
}

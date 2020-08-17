using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class Author: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<MediaAuthor> Book_Author { get; set; }

    }
}

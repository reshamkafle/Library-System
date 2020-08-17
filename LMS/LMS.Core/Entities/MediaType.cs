using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class MediaType: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Media> Media { get; set; }

    }
}

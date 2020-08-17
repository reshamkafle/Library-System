using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class Publisher: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Media> Book { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}

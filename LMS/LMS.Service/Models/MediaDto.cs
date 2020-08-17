using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Service.Models
{
    public class MediaDto
    {
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public int PublisherId { get; set; }
        public decimal Cost { get; set; }
        public string MediaTypeId { get; set; }
        public string Id { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }

        public int TotalQty { get; set; }
        public int BalanceQty { get; set; }
        public string MediaTypeName { get; set; }
    }
}

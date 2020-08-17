using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Models
{
    public class MediaViewModel
    {
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public string PublisherId { get; set; }
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

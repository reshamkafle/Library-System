using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public class Checkout
    {
        public string LibraryNumber { get; set; }
        public DateTime CheckoutDate { get; set; } = DateTime.UtcNow;
        public string MediaId { get; set; }
        public string CheckoutBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Entities
{
    public enum ReservationStatus
    {
        Hold = 1,
        OnLoad
    }
    public class Reservation
    {
        public DateTime DateRequest { get; set; } = DateTime.UtcNow;
        public string MediaId { get; set; }
        public string LibraryCardNumber { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Hold;
    }
}

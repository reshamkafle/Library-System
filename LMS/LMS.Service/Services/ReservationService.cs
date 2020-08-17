using LMS.Core.Constant;
using LMS.Core.Entities;
using LMS.Service.Data;
using LMS.Service.Intefaces;
using LMS.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Services
{
    public class ReservationService : IReservationService
    {
        private readonly LMSContext _context;
        private readonly ILogger<ReservationService> _logger;

        public ReservationService(LMSContext context, ILogger<ReservationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<HistoryDto>> GetReservation(string libraryCard)
        {
            return await (from c in _context.Reservation
                          join d in _context.Media on c.MediaId equals d.Id
                          where c.LibraryCardNumber == libraryCard
                          select new HistoryDto
                          {
                              DateRequest = c.DateRequest.ToShortDateString(),
                              MediaName = d.Title,
                              Status = c.Status.ToString()
                          }).ToListAsync();
        }

        public async Task Reserve(Reservation reservation)
        {
            try
            {
                _context.Reservation.Add(reservation);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Reserve media {0}", MessageConstant.RecordSuccessful);

            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }
    }
}

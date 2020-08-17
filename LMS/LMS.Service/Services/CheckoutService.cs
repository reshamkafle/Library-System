using LMS.Core.Constant;
using LMS.Core.Entities;
using LMS.Service.Data;
using LMS.Service.Intefaces;
using LMS.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Service.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly LMSContext _context;
        private readonly ILogger<CheckoutService> _logger;

        public CheckoutService(LMSContext context, ILogger<CheckoutService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<HistoryDto>> GetCheckout(string libraryCard)
        {
            return await (from c in _context.Checkout
                          join d in _context.Media on c.MediaId equals d.Id
                          where c.LibraryNumber == libraryCard
                          select new HistoryDto
                          {
                              DateRequest = c.CheckoutDate.ToShortDateString(),
                              MediaName = d.Title,
                          }).ToListAsync();
        }

        public async Task Checkout(Checkout checkout)
        {
            try
            {
                _context.Checkout.Add(checkout);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Checkout {0}", MessageConstant.RecordSuccessful);
            }
            catch (Exception ce)
            {
                _logger.LogError(ce.Message);
            }
        }
    }
}

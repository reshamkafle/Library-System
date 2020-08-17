using LMS.Core.Entities;
using LMS.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Service.Intefaces
{
    public interface ICheckoutService
    {
        Task Checkout (Checkout checkout);
        Task<IEnumerable<HistoryDto>> GetCheckout(string libraryCard);
    }
}

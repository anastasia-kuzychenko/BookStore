using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> Get();
        Task<Order> GetById(Guid id);
        Task<decimal> GetTotalSum();
        Task<decimal> GetMonthlySum();
        Task<decimal> GetAverageSum();
        Task Create(Order model);
        Task Edit(Order model);
        Task Delete(Guid id);
        Task<decimal> GetYearlySum();
    }
}
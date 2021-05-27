using BookStore.Models;
using BookStore.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IOrdersService
    {
        Task<PaginatedListDTO<Order>> Get(
            string sortOrder = null, 
            string keyWord = null, 
            PaymentType? paymentType = null, 
            OrderState? orderState = null, 
            decimal? minPrise = null,
            decimal? maxPrise = null, 
            int? pageNumber = null);

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
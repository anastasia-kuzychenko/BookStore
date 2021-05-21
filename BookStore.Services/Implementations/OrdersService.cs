using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Implementations
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Order model)
        {
            await _unitOfWork.Orders.Create(model);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var model = await _unitOfWork.Orders.GetById(id);

            if (model != null)
            {
                await _unitOfWork.Orders.Delete(model);
                await _unitOfWork.Commit();
            }
        }

        public async Task Edit(Order model)
        {
            await _unitOfWork.Orders.Update(model);
            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Order>> Get()
        {
            return await _unitOfWork.Orders.Get();
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _unitOfWork.Orders.GetById(id);
        }

        public async Task<IEnumerable<Order>> GetTop()
        {
            return await _unitOfWork.Orders.Queryable
                .OrderByDescending(o => o.BookCount * o.Book.Prise)
                .Take(5).ToListAsync();
        }

        public async Task<decimal> GetTotalSum()
        {
            return await _unitOfWork.Orders.Queryable.SumAsync(x => x.BookCount * x.Book.Prise);
        }

        public async Task<decimal> GetAverageSum()
        {
            var totalSum = await GetTotalSum();
            var count = await _unitOfWork.Orders.Queryable.CountAsync();
            return totalSum / count;
        }

        public async Task<decimal> GetMonthlySum()
        {
            return await _unitOfWork.Orders.Queryable
                .Where(x => x.OrderDate.Month == DateTime.Now.Month)
                .SumAsync(x => x.BookCount * x.Book.Prise);
        }

        public async Task<decimal> GetYearlySum()
        {
            return await _unitOfWork.Orders.Queryable
               .Where(x => x.OrderDate.Year == DateTime.Now.Year)
               .SumAsync(x => x.BookCount * x.Book.Prise);
        }
    }
}
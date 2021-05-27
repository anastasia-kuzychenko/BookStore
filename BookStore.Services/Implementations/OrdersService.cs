using BookStore.Data;
using BookStore.Models;
using BookStore.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<PaginatedListDTO<Order>> Get(
            string sortOrder, 
            string keyWord, 
            PaymentType? paymentType, 
            OrderState? orderState, 
            decimal? minSum, 
            decimal? maxSum, 
            int? pageNumber)
        {
            Expression<Func<Order, bool>> filter = x =>
                x.BookCount * x.Book.Prise >= (minSum ?? 0)
                && x.BookCount * x.Book.Prise <= (maxSum ?? 100000)
                && (x.PaymentType == paymentType || paymentType == null)
                && (x.State == orderState || orderState == null)
                && (x.Id.ToString().Contains(keyWord ?? "")
                || x.Book.Name.Contains(keyWord ?? "")
                || x.Employee.Name.Contains(keyWord ?? "")
                || x.Customer.Name.Contains(keyWord ?? "")
                );

            Expression<Func<Order, object>> sort =
                sortOrder switch
                {
                    "Book.Name" => x => x.Book.Name,
                    "Employee.Name" => x => x.Employee.Name,
                    "Customer.Name" => x => x.Book.Name,
                    "DeliveryDate" => x => x.DeliveryDate,
                    "OrderDate" => x => x.OrderDate,
                    _ => x => x
                };

            var list = _unitOfWork.Orders.Queryable.Where(filter).OrderBy(sort);

            return await PaginatedListDTO<Order>.CreateAsync(list, pageNumber ?? 1, 10);
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
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Implementations
{
    public class CustomersService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Customer model)
        {
            await _unitOfWork.Customers.Create(model);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var model = await _unitOfWork.Customers.GetById(id);

            if (model != null)
            {
                await _unitOfWork.Customers.Delete(model);
                await _unitOfWork.Commit();
            }
        }

        public async Task Edit(Customer model)
        {
            await _unitOfWork.Customers.Update(model);
            await _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Customer>> Get()
        {
            return await _unitOfWork.Customers.Get();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _unitOfWork.Customers.GetById(id);
        }

        public async Task<IEnumerable<Customer>> GetTop()
        {
            return await _unitOfWork.Customers.Queryable
                .OrderByDescending(x => x.Orders.Sum(o => o.BookCount * o.Book.Prise))
                .Take(5).ToListAsync();
        }
    }
}
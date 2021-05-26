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

        public async Task<PaginatedListDTO<Customer>> Get(string sortOrder, string keyWord, int? pageNumber)
        {
            Expression<Func<Customer, bool>> filter = keyWord == null ? x => true :
               x => x.Id.ToString().Contains(keyWord)
                || x.Name.Contains(keyWord)
                || x.Surname.Contains(keyWord)
                || x.Email.Contains(keyWord)
                || x.Phone.Contains(keyWord);

            Expression<Func<Customer, object>> sort =
                sortOrder switch
                {
                    "FullName" => x => x.Name,
                    _ => x => x
                };

            var list = _unitOfWork.Customers.Queryable.Where(filter).OrderBy(sort);

            return await PaginatedListDTO<Customer>.CreateAsync(list, pageNumber ?? 1, 10);
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
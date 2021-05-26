using BookStore.Models;
using BookStore.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ICustomerService
    {
        Task<PaginatedListDTO<Customer>> Get(string sortOrder = null, string keyWord = null, int? pageNumber = null);
        Task<Customer> GetById(Guid id);
        Task Create(Customer model);
        Task Edit(Customer model);
        Task Delete(Guid id);
        Task<IEnumerable<Customer>> GetTop();
    }
}
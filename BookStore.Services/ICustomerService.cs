using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> Get();
        Task<Customer> GetById(Guid id);
        Task Create(Customer model);
        Task Edit(Customer model);
        Task Delete(Guid id);
        Task<IEnumerable<Customer>> GetTop();
    }
}
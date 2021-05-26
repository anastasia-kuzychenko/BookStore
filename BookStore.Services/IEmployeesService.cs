using BookStore.Models;
using BookStore.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IEmployeesService
    {
        Task<PaginatedListDTO<Employee>> Get(string sortOrder = null, string keyWord = null, int? pageNumber = null);
        Task<Employee> GetById(Guid id);
        Task<IEnumerable<Employee>> GetTop();
        Task<IEnumerable<Employee>> GetBirthdayEmployees();
        Task<IEnumerable<Employee>> GetNewEmployees();
        Task Create(Employee model);
        Task Edit(Employee model);
        Task Delete(Guid id);
    }
}
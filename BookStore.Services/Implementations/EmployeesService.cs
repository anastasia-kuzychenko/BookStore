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
    public class EmployeesService : IEmployeesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Employee model)
        {
            await _unitOfWork.Employees.Create(model);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var model = await _unitOfWork.Employees.GetById(id);

            if (model != null)
            {
                await _unitOfWork.Employees.Delete(model);
                await _unitOfWork.Commit();
            }
        }

        public async Task Edit(Employee model)
        {
            await _unitOfWork.Employees.Update(model);
            await _unitOfWork.Commit();
        }

        public async Task<PaginatedListDTO<Employee>> Get(string sortOrder, string keyWord, int? pageIndex = null)
        {
            Expression<Func<Employee, bool>> filter = keyWord == null? x => true :
                x => x.Id.ToString().Contains(keyWord)
                 || x.Name.Contains(keyWord)
                 || x.Surname.Contains(keyWord)
                 || x.Phone.Contains(keyWord);

            Expression<Func<Employee, object>> sort =
                sortOrder switch
                {
                    "FullName" => x => x.Name,
                    "Birthday" => x => x.Birthday,
                    "FirstDay" => x => x.FirstDay,
                    "Position" => x => x.Position,
                    _ => x => x
                };

            var list = _unitOfWork.Employees.Queryable.Where(filter).OrderBy(sort);

            return await PaginatedListDTO<Employee>.CreateAsync(list, pageIndex ?? 1, 10);
        }

        public async Task<Employee> GetById(Guid id)
        {
            return await _unitOfWork.Employees.GetById(id);
        }

        public async Task<IEnumerable<Employee>> GetTop()
        {
            return await _unitOfWork.Employees.Queryable
                .OrderByDescending(x => x.Orders.Sum(o => o.BookCount * o.Book.Prise))
                .Take(5).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetBirthdayEmployees()
        {
            var now = DateTime.Now;
            return await _unitOfWork.Employees.Get(x => x.Birthday.Month == now.Month && x.Birthday.Day == now.Day);
        }
        public async Task<IEnumerable<Employee>> GetNewEmployees()
        {
            var now = DateTime.Now;
            return await _unitOfWork.Employees.Get(x => x.FirstDay.Month == now.Month && x.FirstDay.Year == now.Year);
        }
    }
}
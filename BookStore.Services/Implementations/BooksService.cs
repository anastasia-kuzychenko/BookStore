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
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Book model)
        {
            await _unitOfWork.Books.Create(model);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var model = await _unitOfWork.Books.GetById(id);

            if (model != null) 
            {
                await _unitOfWork.Books.Delete(model);
                await _unitOfWork.Commit();
            }
        }

        public async Task Edit(Book model)
        {
            await _unitOfWork.Books.Update(model);
            await _unitOfWork.Commit();
        }

        public async Task<PaginatedListDTO<Book>> Get(string sortOrder, string keyWord, decimal? minPrise, decimal? maxPrise, int? pageNumber)
        {
            Expression<Func<Book, bool>> filter = x => 
                x.Prise >= (minPrise ?? 0) 
                && x.Prise <= (maxPrise ?? 100000)
                && (x.Id.ToString().Contains(keyWord ?? "")
                || x.Name.Contains(keyWord ?? "")
                || x.Publisher.Contains(keyWord ?? "")
                || x.Assessment.Contains(keyWord ?? "")
                || x.Author.Name.Contains(keyWord ?? ""));

            Expression <Func<Book, object>> sort =
                sortOrder switch
                {
                    "Name" => x => x.Name,
                    "Prise" => x => x.Prise,
                    "Date" => x => x.Date,
                    "Author.Name" => x => x.Author.Name,
                    _ => x => x
                };

            var list = _unitOfWork.Books.Queryable.Where(filter).OrderBy(sort);

            return await PaginatedListDTO<Book>.CreateAsync(list, pageNumber ?? 1, 10);
        }

        public async Task<Book> GetById(Guid id)
        {
            return await _unitOfWork.Books.GetById(id);
        }

        public async Task<IEnumerable<Book>> GetTop()
        {
            return await _unitOfWork.Books.Queryable
                .OrderByDescending(x => x.Orders.Count())
                .Take(5).ToListAsync();
        }
    }
}
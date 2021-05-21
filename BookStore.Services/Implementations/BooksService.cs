using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Book>> Get()
        {
            return await _unitOfWork.Books.Get();
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
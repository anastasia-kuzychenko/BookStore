using BookStore.Models;
using BookStore.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBooksService
    {
        Task<PaginatedListDTO<Book>> Get(string sortOrder = null, string keyWord = null, decimal? minPrise = null, decimal? maxPrise = null, int? pageNumber = null);
        Task<Book> GetById(Guid id);
        Task<IEnumerable<Book>> GetTop();
        Task Create(Book model);
        Task Edit(Book model);
        Task Delete(Guid id);
    }
}
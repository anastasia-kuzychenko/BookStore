using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> Get();
        Task<Book> GetById(Guid id);
        Task<IEnumerable<Book>> GetTop();
        Task Create(Book model);
        Task Edit(Book model);
        Task Delete(Guid id);
    }
}
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IAuthorsService
    {
        Task<List<Author>> Get();
        Task<Author> GetById(int id);
        Task Create(Author model);
        Task Edit(Author model);
        Task Delete(Guid id);
    }
}
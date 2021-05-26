using BookStore.Models;
using BookStore.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IAuthorsService
    {
        Task<PaginatedListDTO<Author>> Get(string sortOrder = null, string keyWord = null, int? pageNumber = null);
        Task<Author> GetById(int id);
        Task Create(Author model);
        Task Edit(Author model);
        Task Delete(Guid id);
    }
}
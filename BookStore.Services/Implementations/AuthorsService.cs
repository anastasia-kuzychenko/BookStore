using BookStore.Data;
using BookStore.Models;
using BookStore.Services.DTOs;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Services.Implementations
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Author model)
        {
            await _unitOfWork.Authors.Create(model);
            await _unitOfWork.Commit();
        }

        public async Task Delete(Guid id)
        {
            var model = await _unitOfWork.Authors.GetById(id);
            
            if(model != null)
            {
                await _unitOfWork.Authors.Delete(model);
                await _unitOfWork.Commit();
            }
        }

        public async Task Edit(Author model)
        {
            await _unitOfWork.Authors.Update(model);
            await _unitOfWork.Commit();
        }

        public async Task<PaginatedListDTO<Author>> Get(string sortOrder, string keyWord, int? pageNumber)
        {
            Expression<Func<Author, bool>> filter = keyWord == null ? x => true :
               x => x.Id.ToString().Contains(keyWord)
                || x.Name.Contains(keyWord)
                || x.Surname.Contains(keyWord);

            Expression<Func<Author, object>> sort =
                sortOrder switch
                {
                    "FullName" => x => x.Name,
                    _ => x => x
                };

            var list = _unitOfWork.Authors.Queryable.Where(filter).OrderBy(sort);

            return await PaginatedListDTO<Author>.CreateAsync(list, pageNumber ?? 1, 10);
        }

        public async Task<Author> GetById(int id)
        {
            return await _unitOfWork.Authors.GetById(id);
        }
    }
}
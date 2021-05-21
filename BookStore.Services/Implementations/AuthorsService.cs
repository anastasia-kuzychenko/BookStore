using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
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

        public async Task<List<Author>> Get()
        {
            return await _unitOfWork.Authors.Get();
        }

        public async Task<Author> GetById(int id)
        {
            return await _unitOfWork.Authors.GetById(id);
        }
    }
}
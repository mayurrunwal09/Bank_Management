using Domain_Library.Models;
using Infrastructure_Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.GeneralServices
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        #region Property
        private readonly IRepository<T> _repository;

        #endregion

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(T entity)
        {
            return await _repository.Delete(entity);
        }

        public Task<T> Find(Expression<Func<T, bool>> match)
        {
            return _repository.Find(match);
        }

        public Task<ICollection<T>> FindAll(Expression<Func<T, bool>> match)
        {
            return _repository.FindAll(match);
        }

        public Task<T> Get(int Id)
        {
            return _repository.Get(Id);
        }

        public Task<ICollection<T>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<bool> Insert(T entity)
        {
            return _repository.Insert(entity);
        }

        public Task<bool> Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}

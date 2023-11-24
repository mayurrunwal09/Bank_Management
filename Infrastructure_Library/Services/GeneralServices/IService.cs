using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.GeneralServices
{
    public interface IService<T>
    {
        Task<ICollection<T>> GetAll();

        Task<T> Get(int Id);
        Task<bool> Insert(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(T entity);

        Task<T> Find(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindAll(Expression<Func<T, bool>> match);
    }
}


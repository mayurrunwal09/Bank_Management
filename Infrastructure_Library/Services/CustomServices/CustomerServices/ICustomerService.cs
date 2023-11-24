using Domain_Library.Models;
using Domain_Library.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.CustomerServices
{
    public interface ICustomerService
    {
        Task<ICollection<UserViewModel>> GetAll();
        Task<UserViewModel> Get(int Id);
        Task<bool> Insert(UserInsertModel userInsertModel);
        Task<bool> Update(UserUpdateModel userUpdateModel);
        Task<bool> Delete(int Id);
        Task<User> Find(Expression<Func<User, bool>> match);
    }
}

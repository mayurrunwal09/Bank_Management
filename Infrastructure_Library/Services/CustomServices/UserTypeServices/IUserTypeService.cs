using Domain_Library.Models;
using Domain_Library.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.UserTypeServices
{
    public interface IUserTypeService
    {
        Task<ICollection<UserTypeViewModel>> GetAll();
        Task<UserTypeViewModel> Get(int Id);
        Task<bool> Insert(UserTypeInsertModel userInsertModel);
        Task<bool> Update(UserTypeUpdateModel userUpdateModel);
        Task<bool> Delete(int Id);
        Task<UserType> Find(Expression<Func<UserType, bool>> match);
    }
}

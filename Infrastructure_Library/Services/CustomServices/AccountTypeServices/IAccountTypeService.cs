using Domain_Library.Models;
using Domain_Library.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.AccountTypeServices
{
    public interface IAccountTypeService
    {
        Task<ICollection<AccountTypeViewModel>> GetAll();
        Task<AccountTypeViewModel> Get(int Id);
        Task<bool> Insert(AccountTypeInsertModel userInsertModel);
        Task<bool> Update(AccountTypeUpdateModel userUpdateModel);
        Task<bool> Delete(int Id);
        Task<AccountType> Find(Expression<Func<AccountType, bool>> match);
    }
}

using Domain_Library.Models;
using Domain_Library.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.SavingServices
{
    public interface ISavingService
    {
        Task<ICollection<AccountViewModel>> GetAll();
        Task<AccountViewModel> Get(int Id);
        Task<bool> Insert(AccountInsertModel userInsertModel);
        Task<bool> Update(AccountUpdateModel userUpdateModel);
        Task<bool> Delete(int Id);
        Task<Account> Find(Expression<Func<Account, bool>> match);
    }
}

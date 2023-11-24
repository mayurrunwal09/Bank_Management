using Domain_Library.Models;
using Domain_Library.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.TransactionTypeServices
{
    public interface ITransactionTypeService
    {
        Task<ICollection<TransactionTypeViewModel>> GetAll();
        Task<TransactionTypeViewModel> Get(int Id);
        Task<bool> Insert(TransactionTypeInsertModel TransactionTypeInsertModel);
        Task<bool> Update(TransactionTypeUpdateModel TransactionTypeUpdateModel);
        Task<bool> Delete(int Id);
        Task<TransactionType> Find(Expression<Func<TransactionType, bool>> match);
    }
}

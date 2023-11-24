using Domain_Library.Models;
using Domain_Library.View_Model;
using Infrastructure_Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.TransactionTypeServices
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly IRepository<TransactionType> _userType;

        public TransactionTypeService(IRepository<TransactionType> userType)
        {
            _userType = userType;
        }

        public async Task<bool> Delete(int Id)
        {
            if (Id == null)
            {
                TransactionType userType = await _userType.Get(Id);
                if (userType != null)
                {
                    _ = _userType.Delete(userType);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public Task<TransactionType> Find(Expression<Func<TransactionType, bool>> match)
        {
            return _userType.Find(match);
        }

        public async Task<TransactionTypeViewModel> Get(int Id)
        {
            var result = await _userType.Get(Id);
            if (result == null)
                return null;
            else
            {
                TransactionTypeViewModel userTypeViewModel = new()
                {
                    Id = result.Id,
                    TransactionTypeName = result.TransactionTypeName
                };
                return userTypeViewModel;
            }
        }

        public async Task<ICollection<TransactionTypeViewModel>> GetAll()
        {
            ICollection<TransactionTypeViewModel> userTypeViewModels = new List<TransactionTypeViewModel>();
            ICollection<TransactionType> userTypes = await _userType.GetAll();
            foreach (TransactionType userType in userTypes)
            {
                TransactionTypeViewModel userTypeView = new()
                {
                    Id = userType.Id,
                    TransactionTypeName = userType.TransactionTypeName
                };
                userTypeViewModels.Add(userTypeView);
            }
            return userTypeViewModels;
        }

        public Task<bool> Insert(TransactionTypeInsertModel TransactionTypeInsertModel)
        {
            TransactionType userType = new()
            {
                TransactionTypeName = TransactionTypeInsertModel.TransactionTypeName,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                IsActive = true
            };
            return _userType.Insert(userType);
        }

        public async Task<bool> Update(TransactionTypeUpdateModel TransactionTypeUpdateModel)
        {
            TransactionType userType = await _userType.Get(TransactionTypeUpdateModel.Id);
            if (userType != null)
            {
                userType.TransactionTypeName = TransactionTypeUpdateModel.TransactionTypeName;
                userType.LastUpdated = System.DateTime.Now;
                var result = await _userType.Update(userType);
                return result;
            }
            else
                return false;
        }
    }
}

using Domain_Library.Models;
using Domain_Library.View_Model;
using Infrastructure_Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.AccountTypeServices
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IRepository<AccountType> _userType;

        public AccountTypeService(IRepository<AccountType> userType)
        {
            _userType = userType;
        }

        public async Task<bool> Delete(int Id)
        {
            if (Id == null)
            {
                AccountType userType = await _userType.Get(Id);
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

        public Task<AccountType> Find(Expression<Func<AccountType, bool>> match)
        {
            return _userType.Find(match);
        }

        public async Task<AccountTypeViewModel> Get(int Id)
        {
            var result = await _userType.Get(Id);
            if (result == null)
                return null;
            else
            {
                AccountTypeViewModel userTypeViewModel = new()
                {
                    Id = result.Id,
                    AccountTypeName = result.AccountTypeName
                };
                return userTypeViewModel;
            }
        }

        public async Task<ICollection<AccountTypeViewModel>> GetAll()
        {
            ICollection<AccountTypeViewModel> userTypeViewModels = new List<AccountTypeViewModel>();
            ICollection<AccountType> userTypes = await _userType.GetAll();
            foreach (AccountType userType in userTypes)
            {
                AccountTypeViewModel userTypeView = new()
                {
                    Id = userType.Id,
                    AccountTypeName = userType.AccountTypeName
                };
                userTypeViewModels.Add(userTypeView);
            }
            return userTypeViewModels;
        }

        public Task<bool> Insert(AccountTypeInsertModel userInsertModel)
        {
            AccountType userType = new()
            {

                AccountTypeName = userInsertModel.AccountTypeName,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                IsActive = true
            };
            return _userType.Insert(userType);
        }

        public async Task<bool> Update(AccountTypeUpdateModel userUpdateModel)
        {
            AccountType userType = await _userType.Get(userUpdateModel.Id);
            if (userType != null)
            {
                userType.AccountTypeName = userUpdateModel.AccountTypeName;
                userType.LastUpdated = System.DateTime.Now;
                var result = await _userType.Update(userType);
                return result;
            }
            else
                return false;
        }
    }
}

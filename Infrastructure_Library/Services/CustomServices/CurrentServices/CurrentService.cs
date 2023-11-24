using Domain_Library.Models;
using Domain_Library.View_Model;
using Infrastructure_Library.Repositories;
using Infrastructure_Library.Services.CustomServices.AccountTypeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.CurrentServices
{
    public class CurrentService : ICurrentService
    {
        private readonly IRepository<Account> _user;
        private readonly IAccountTypeService _userType;

        public CurrentService(IRepository<Account> user, IAccountTypeService userType)
        {
            _user = user;
            _userType = userType;
        }

        public async Task<bool> Delete(int Id)
        {
            if (Id != null)
            {
                Account userAsSupplier = await _user.Get(Id);
                if (userAsSupplier != null)

                {
                    _ = _user.Delete(userAsSupplier);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public Task<Account> Find(Expression<Func<Account, bool>> match)
        {
            return _user.Find(match);
        }

        public async Task<AccountViewModel> Get(int Id)
        {
            var result = await _user.Get(Id);
            var usertype = await _userType.Find(x => x.AccountTypeName == "Current");

            if (result == null)
                return null;
            else
            {
                if (result.AccountTypeId == usertype.Id)
                {
                    AccountViewModel userView = new()
                    {
                        Id = result.Id,
                        AccountId = result.AccountTypeId,
                        AccountNumber = result.AccountNumber,
                        Balance = result.Balance,
                        IsActive = result.IsActive
                        
                    };
                    AccountTypeViewModel view = new();
                    if (usertype != null)
                    {
                        view.Id = usertype.Id;
                        view.AccountTypeName = usertype.AccountTypeName;
                        userView.AccountType.Add(view);
                    }
                    return userView;
                }
                return null;
            }
        }

        public async Task<ICollection<AccountViewModel>> GetAll()
        {
            var usertype = await _userType.Find(x => x.AccountTypeName == "Current");
            ICollection<AccountViewModel> supplierViewModels = new List<AccountViewModel>();

            ICollection<Account> supplier = await _user.FindAll(x => x.AccountTypeId == usertype.Id);
            foreach (Account result in supplier)
            {
                AccountViewModel supplierView = new()
                {
                    Id = result.Id,
                    AccountId = result.AccountId,
                    UserId = result.UserId,
                    Balance = result.Balance,
                    AccountNumber = result.AccountNumber,
                    IsActive = result.IsActive
                   };
                AccountTypeViewModel userView = new();
                if (usertype != null)
                {
                    userView.Id = usertype.Id;
                    userView.AccountTypeName = usertype.AccountTypeName;
                    supplierView.AccountType.Add(userView);
                }
                supplierViewModels.Add(supplierView);
            }
            if (supplier == null)
                return null;
            return supplierViewModels;
        }

        public async Task<bool> Insert(AccountInsertModel userInsertModel)
        {
            var usertype = await _userType.Find(x => x.AccountTypeName == "Current");
            if (usertype != null)
            {

                Account supplier = new()
                {
                    Balance = userInsertModel.Balance,
                    AccountId = userInsertModel.AccountId,
                    AccountNumber = userInsertModel.AccountNumber,
                    UserId = userInsertModel.UserId,
                    AccountTypeId = usertype.Id,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    IsActive = userInsertModel.IsActive

                };
                var result = await _user.Insert(supplier);
                return result;
            }
            else
                return false;
        }

        public async Task<bool> Update(AccountUpdateModel userUpdateModel)
        {
            Account supplier = await _user.Get(userUpdateModel.Id);
            if (supplier != null)
            {
                supplier.Balance = userUpdateModel.Balance;
                supplier.UserId = userUpdateModel.UserId;
                supplier.AccountId = userUpdateModel.AccountId;
                supplier.AccountNumber = userUpdateModel.AccountNumber;
                supplier.Created = DateTime.Now;
                supplier.LastUpdated = DateTime.Now;
                supplier.IsActive = userUpdateModel.IsActive;

                var result = await _user.Update(supplier);
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}

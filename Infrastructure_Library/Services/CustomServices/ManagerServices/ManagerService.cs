using Domain_Library.Models;
using Domain_Library.View_Model;
using Infrastructure_Library.PasswordSecurity;
using Infrastructure_Library.Repositories;
using Infrastructure_Library.Services.CustomServices.UserTypeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.ManagerServices
{
    public class ManagerService : IManagerService
    {
        private readonly IRepository<User> _user;
        private readonly IUserTypeService _userType;

        public ManagerService(IRepository<User> user, IUserTypeService userType)
        {
            _user = user;
            _userType = userType;
        }

        public async Task<bool> Delete(int Id)
        {
            if (Id != null)
            {
                User userAsSupplier = await _user.Get(Id);
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

        public Task<User> Find(Expression<Func<User, bool>> match)
        {
            return _user.Find(match);
        }

        public async Task<UserViewModel> Get(int Id)
        {
            var result = await _user.Get(Id);
            var usertype = await _userType.Find(x => x.TypeName == "Manager");

            if (result == null)
                return null;
            else
            {
                if (result.UserTypeId == usertype.Id)
                {
                    UserViewModel userView = new()
                    {
                        Id = result.Id,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        UserId = result.UserId,
                        UserName = result.UserName,
                        Email = result.Email,
                        Password = Encryptor.DecryptString(result.Password),
                        PhoneNumber = result.PhoneNumber,

                    };
                    UserTypeViewModel view = new();
                    if (usertype != null)
                    {
                        view.Id = usertype.Id;
                        view.TypeName = usertype.TypeName;
                        userView.UserType.Add(view);
                    }
                    return userView;
                }
                return null;
            }
        }

        public async Task<ICollection<UserViewModel>> GetAll()
        {
            var usertype = await _userType.Find(x => x.TypeName == "Manager");
            ICollection<UserViewModel> supplierViewModels = new List<UserViewModel>();

            ICollection<User> supplier = await _user.FindAll(x => x.UserTypeId == usertype.Id);
            foreach (User result in supplier)
            {
                UserViewModel supplierView = new()
                {
                    Id = result.Id,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    UserId = result.UserId,
                    UserName = result.UserName,
                    Email = result.Email,
                    Password = Encryptor.DecryptString(result.Password),
                    PhoneNumber = result.PhoneNumber,
                };
                UserTypeViewModel userView = new();
                if (usertype != null)
                {
                    userView.Id = usertype.Id;
                    userView.TypeName = usertype.TypeName;
                    supplierView.UserType.Add(userView);
                }
                supplierViewModels.Add(supplierView);
            }
            if (supplier == null)
                return null;
            return supplierViewModels;
        }

        public async Task<bool> Insert(UserInsertModel supplierInsertModel)
        {
            var usertype = await _userType.Find(x => x.TypeName == "Manager");
            if (usertype != null)
            {

                User supplier = new()
                {
                    UserId = supplierInsertModel.UserId,
                    FirstName = supplierInsertModel.FirstName,
                    LastName = supplierInsertModel.LastName,
                    UserName = supplierInsertModel.UserName,
                    Email = supplierInsertModel.Email,
                    Password = Encryptor.EncryptString(supplierInsertModel.Password),
                    PhoneNumber = supplierInsertModel.PhoneNumber,
                    UserTypeId = usertype.Id,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    IsActive = supplierInsertModel.IsActive,

                };
                var result = await _user.Insert(supplier);
                return result;
            }
            else
                return false;
        }

        public async Task<bool> Update(UserUpdateModel userUpdateModel)
        {
            User supplier = await _user.Get(userUpdateModel.Id);
            if (supplier != null)
            {
                supplier.UserId = userUpdateModel.UserId;
                supplier.UserName = userUpdateModel.UserName;
                supplier.FirstName = userUpdateModel.FirstName;
                supplier.LastName = userUpdateModel.LastName;
                supplier.Password = Encryptor.EncryptString(userUpdateModel.Password);
                supplier.PhoneNumber = userUpdateModel.PhoneNumber;
                supplier.UserTypeId = supplier.UserTypeId;
                supplier.Created = supplier.Created;
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

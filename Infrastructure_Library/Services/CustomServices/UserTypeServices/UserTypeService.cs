using Domain_Library.Models;
using Domain_Library.View_Model;
using Infrastructure_Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Library.Services.CustomServices.UserTypeServices
{
    public class UserTypeService :IUserTypeService
    {
        private readonly IRepository<UserType> _userType;

        public UserTypeService(IRepository<UserType> userType)
        {
            _userType = userType;
        }

        public async Task<bool> Delete(int Id)
        {
            if (Id == null)
            {
                UserType userType = await _userType.Get(Id);
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

        public Task<UserType> Find(Expression<Func<UserType, bool>> match)
        {
            return _userType.Find(match);
        }

        public async Task<UserTypeViewModel> Get(int Id)
        {
            var result = await _userType.Get(Id);
            if (result == null)
                return null;
            else
            {
                UserTypeViewModel userTypeViewModel = new()
                {
                    Id = result.Id,
                    TypeName = result.TypeName
                };
                return userTypeViewModel;
            }
        }

        public async Task<ICollection<UserTypeViewModel>> GetAll()
        {
            ICollection<UserTypeViewModel> userTypeViewModels = new List<UserTypeViewModel>();
            ICollection<UserType> userTypes = await _userType.GetAll();
            foreach (UserType userType in userTypes)
            {
                UserTypeViewModel userTypeView = new()
                {
                    Id = userType.Id,
                    TypeName = userType.TypeName
                };
                userTypeViewModels.Add(userTypeView);
            }
            return userTypeViewModels;
        }

        public Task<bool> Insert(UserTypeInsertModel userInsertModel)
        {
            UserType userType = new()
            {
                TypeName = userInsertModel.TypeName,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                IsActive = true
            };
            return _userType.Insert(userType);
        }

        public async Task<bool> Update(UserTypeUpdateModel userUpdateModel)
        {
            UserType userType = await _userType.Get(userUpdateModel.Id);
            if (userType != null)
            {
                userType.TypeName = userUpdateModel.TypeName;
                userType.LastUpdated = System.DateTime.Now;
                var result = await _userType.Update(userType);
                return result;
            }
            else
                return false;
        }
    }
}

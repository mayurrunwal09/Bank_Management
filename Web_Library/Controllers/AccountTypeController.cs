using Domain_Library.View_Model;
using Infrastructure_Library.Services.CustomServices.AccountTypeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        #region Private Variables and Constructor
        private readonly IAccountTypeService _serviceAccType;
        public AccountTypeController(IAccountTypeService serviceUserType)
        {
            _serviceAccType = serviceUserType;
        }
        #endregion

        [Route("GetAllUserType")]
        [HttpGet]
        public async Task<ActionResult<AccountViewModel>> GetAllUserType()
        {
            var result = await _serviceAccType.GetAll();

            if (result == null)
                return BadRequest("No Records Found, Please Try Again After Adding them...!");

            return Ok(result);
        }
        [Route("GetUserType")]
        [HttpGet]
        public async Task<ActionResult<AccountViewModel>> GetUserType(int Id)
        {
            if (Id != null)
            {
                var result = await _serviceAccType.Get(Id);

                if (result == null)
                    return BadRequest("No Records Found, Please Try Again After Adding them...!");

                return Ok(result);
            }
            else
                return NotFound("Invalid UserType ID, Please Entering a Valid One...!");
        }

        [Route("InsertUserType")]
        [HttpPost]
        public async Task<IActionResult> InsertUserType(AccountTypeInsertModel AccountTypeInsertModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceAccType.Insert(AccountTypeInsertModel);
                if (result == true)
                    return Ok("UserType Inserted Successfully...!");
                else
                    return BadRequest("Something Went Wrong, UserType Is Not Inserted, Please Try After Sometime...!");
            }
            else
                return BadRequest("Invalid UserType Information, Please Provide Correct Details for UserType...!");
        }

        [Route("UpdateUserType")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserType(AccountTypeUpdateModel AccountTypeUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceAccType.Update(AccountTypeUpdateModel);
                if (result == true)
                    return Ok(AccountTypeUpdateModel);
                else
                    return BadRequest("Something went wrong, Please Try again later...!");
            }
            else
                return BadRequest("Invalid UserType Information, Please Provide Correct Details for UserType...!");
        }
        [Route("DeleteUserType")]
        [HttpDelete]

        public async Task<IActionResult> DeleteUserType(int Id)
        {
            var result = await _serviceAccType.Delete(Id);
            if (result == true)
                return Ok("UserType Deleted Successfully...!");
            else
                return BadRequest("UserType is not deleted, Please Try again later...!");

        }
    }
}

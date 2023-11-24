using Domain_Library.View_Model;
using Infrastructure_Library.Services.CustomServices.TransactionTypeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {

        #region Private Variables and Constructor
        private readonly ITransactionTypeService _serviceUserType;
        public TransactionTypeController(ITransactionTypeService serviceUserType)
        {
            _serviceUserType = serviceUserType;
        }
        #endregion

        [Route("GetAllUserType")]
        [HttpGet]
        public async Task<ActionResult<TransactionTypeViewModel>> GetAllUserType()
        {
            var result = await _serviceUserType.GetAll();

            if (result == null)
                return BadRequest("No Records Found, Please Try Again After Adding them...!");

            return Ok(result);
        }
        [Route("GetUserType")]
        [HttpGet]
        public async Task<ActionResult<TransactionTypeViewModel>> GetUserType(int Id)
        {
            if (Id != null)
            {
                var result = await _serviceUserType.Get(Id);

                if (result == null)
                    return BadRequest("No Records Found, Please Try Again After Adding them...!");

                return Ok(result);
            }
            else
                return NotFound("Invalid UserType ID, Please Entering a Valid One...!");
        }

        [Route("InsertUserType")]
        [HttpPost]
        public async Task<IActionResult> InsertUserType(TransactionTypeInsertModel TransactionTypeInsertModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceUserType.Insert(TransactionTypeInsertModel);
                if (result == true)
                    return Ok("Transaction Type Inserted Successfully...!");
                else
                    return BadRequest("Something Went Wrong, UserType Is Not Inserted, Please Try After Sometime...!");
            }
            else
                return BadRequest("Invalid Transaction Type Information, Please Provide Correct Details for UserType...!");
        }

        [Route("UpdateUserType")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserType(TransactionTypeUpdateModel TransactionTypeUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceUserType.Update(TransactionTypeUpdateModel);
                if (result == true)
                    return Ok(TransactionTypeUpdateModel);
                else
                    return BadRequest("Something went wrong, Please Try again later...!");
            }
            else
                return BadRequest("Invalid Transaction Type Information, Please Provide Correct Details for UserType...!");
        }
        [Route("DeleteUserType")]
        [HttpDelete]

        public async Task<IActionResult> DeleteUserType(int Id)
        {
            var result = await _serviceUserType.Delete(Id);
            if (result == true)
                return Ok("UserType Deleted Successfully...!");
            else
                return BadRequest("Transaction Type is not deleted, Please Try again later...!");

        }
    }
}

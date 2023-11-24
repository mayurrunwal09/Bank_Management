using Domain_Library.View_Model;
using Infra_Library._dbContext_main;
using Infrastructure_Library.Services.CustomServices.CustomerServices;
using Infrastructure_Library.Services.CustomServices.ManagerServices;
using Infrastructure_Library.Services.CustomServices.UserTypeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private Variables and Constructor
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IManagerService _manager;
        private readonly ICustomerService _serviceCustomer;
        private readonly IUserTypeService _serviceUserType;
        private readonly MainDbContext _context;
        public UserController(ILogger<UserController> logger, MainDbContext context, IManagerService manager, ICustomerService serviceCustomer, IUserTypeService serviceUserType, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _manager = manager;
            _serviceUserType = serviceUserType;
            _serviceCustomer = serviceCustomer;
            _environment = environment;
        }
        #endregion

        [HttpGet(nameof(GetAllCustomer))]
        public async Task<ActionResult<UserViewModel>> GetAllCustomer()
        {
            var result = await _serviceCustomer.GetAll();
            if (result == null)
                return BadRequest("No Records Found, Please Try Again After Adding them...!");
            return Ok(result);

        }

        [HttpGet(nameof(GetCustomer))]
        public async Task<IActionResult> GetCustomer(int Id)
        {
            if (Id != null)
            {
                var result = await _serviceCustomer.Get(Id);
                if (result == null)
                    return BadRequest("No Records Found, Please Try Again After Adding them...!");
                return Ok(result);
            }
            else
                return NotFound("Invalid Supplier Id, Please Entering a Valid One...!");
        }

        [HttpPut(nameof(EditCustomer))]
        public async Task<IActionResult> EditCustomer([FromForm] UserUpdateModel customerModel)
        {
            if (ModelState.IsValid)
            {
                var CheckUser = await _serviceCustomer.Find(x => x.UserId == customerModel.UserId && x.Id != customerModel.Id);

                if (CheckUser != null)
                    return BadRequest("User ID : " + customerModel.UserId + " already Exist...!");
                else
                {
                    var CheckUsername = await _serviceCustomer.Find(x => x.UserName == customerModel.UserName && x.Id != customerModel.Id);
                    if (CheckUsername != null)
                        return BadRequest(" UserName :" + customerModel.UserName + " already Exist...!");


                    else
                    {
                        var result = await _serviceCustomer.Update(customerModel);
                        if (result == true)
                            return Ok("Supplier Updated Successfully...!");
                        else
                            return BadRequest("Something Went Wrong, Supplier Is Not Updated, Please Try After Sometime...!");
                    }
                }
            }
            else
                return NotFound("User Not Found with id :" + customerModel.Id + ", Please Try Again After Sometime...!");
        }

        [HttpDelete(nameof(DeleteCustomer))]
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            var result = await _serviceCustomer.Delete(Id);
            if (result == true)
                return Ok("User Deleted Successfully...!");
            else
                return BadRequest("Something Went Wrong, Supplier Is Not Deleted, Please Try After Sometime...!");
        }



        [HttpGet(nameof(GetManager))]
        public async Task<IActionResult> GetManager(int Id)
        {
            if (Id != null)
            {
                var result = await _manager.Get(Id);
                if (result == null)
                    return BadRequest("No Records Found, Please Try Again After Adding them...!");
                return Ok(result);
            }
            else
                return NotFound("Invalid Supplier Id, Please Entering a Valid One...!");
        }

        [HttpPut(nameof(EditManager))]
        public async Task<IActionResult> EditManager([FromForm] UserUpdateModel customerModel)
        {
            if (ModelState.IsValid)
            {
                var CheckUser = await _manager.Find(x => x.UserId == customerModel.UserId && x.Id != customerModel.Id);

                if (CheckUser != null)
                    return BadRequest("User ID : " + customerModel.UserId + " already Exist...!");
                else
                {
                    var CheckUsername = await _manager.Find(x => x.UserName == customerModel.UserName && x.Id != customerModel.Id);
                    if (CheckUsername != null)
                        return BadRequest(" UserName :" + customerModel.UserName + " already Exist...!");


                    else
                    {
                        var result = await _manager.Update(customerModel);
                        if (result == true)
                            return Ok("User Updated Successfully...!");
                        else
                            return BadRequest("Something Went Wrong, Supplier Is Not Updated, Please Try After Sometime...!");
                    }
                }
            }
            else
                return NotFound("User Not Found with id :" + customerModel.Id + ", Please Try Again After Sometime...!");
        }


        [HttpDelete(nameof(DeleteManager))]
        public async Task<IActionResult> DeleteManager(int Id)
        {
            var result = await _manager.Delete(Id);
            if (result == true)
                return Ok("User Deleted Successfully...!");
            else
                return BadRequest("Something Went Wrong, Supplier Is Not Deleted, Please Try After Sometime...!");
        }

    }
}

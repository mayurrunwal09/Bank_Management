using Domain_Library.View_Model;
using Infra_Library._dbContext_main;
using Infrastructure_Library.Services.CustomServices.AccountTypeServices;
using Infrastructure_Library.Services.CustomServices.CurrentServices;
using Infrastructure_Library.Services.CustomServices.SavingServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Private Variables and Constructor
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly ICurrentService _manager;
        private readonly ISavingService _serviceCustomer;
        private readonly IAccountTypeService _serviceUserType;
        private readonly MainDbContext _context;
        public AccountController(ILogger<AccountController> logger, MainDbContext context, ICurrentService manager, ISavingService serviceCustomer, IAccountTypeService serviceUserType, IWebHostEnvironment environment)
        {
            _logger = logger;
            _manager = manager;
            _serviceUserType = serviceUserType;
            _serviceCustomer = serviceCustomer;
            _environment = environment;
            _context = context;
        }
        #endregion
        [HttpGet(nameof(GetAllCombinedAccounts))]
        public async Task<ActionResult<List<AccountViewModel>>> GetAllCombinedAccounts()
        {
            var currentAccounts = await _manager.GetAll();
            var savingAccounts = await _serviceCustomer.GetAll();
          

            if (savingAccounts == null || currentAccounts == null)
                return BadRequest("No Accounts Found, Please Try Again After Adding Them...!");

            var combinedAccounts = savingAccounts.Concat(currentAccounts).ToList();

            return Ok(combinedAccounts);
        }


        [HttpGet(nameof(GetAllCurrentAccount))]
        public async Task<ActionResult<AccountViewModel>> GetAllCurrentAccount()
        {
            var result = await _manager.GetAll();
            if (result == null)
                return BadRequest("No Records Found, Please Try Again After Adding them...!");
            return Ok(result);

        }

        [HttpGet(nameof(GetCurrentAccount))]

        public async Task<IActionResult> GetCurrentAccount(int Id)
        {
            if (Id != null)
            {
                var result = await _manager.Get(Id);
                if (result == null)
                    return BadRequest("No Records Found, Please Try Again After Adding them...!");
                return Ok(result);
            }
            else
                return NotFound("Invalid Account Id, Please Entering a Valid One...!");
        }

        [HttpPost(nameof(InsertCurrent))]
        public async Task<IActionResult> InsertCurrent([FromForm] AccountInsertModel AccountInsertModel)
        {
            if (ModelState.IsValid)
            {
                var usertype = await _serviceUserType.Find(x => x.AccountTypeName == "Current");
                if (usertype != null)
                {

                    var CheckUser = await _manager.Find(x => x.AccountId == AccountInsertModel.AccountId);

                    if (CheckUser != null)
                    {
                        return BadRequest("Account ID : " + AccountInsertModel.AccountId + " already Exist...!");
                    }
                    else
                    {
                        var CheckUsername = await _manager.Find(x => x.AccountNumber == AccountInsertModel.AccountNumber);
                        if (CheckUsername != null)
                        {
                            return BadRequest(" Account number :" + AccountInsertModel.AccountNumber + " already Exist...!");
                        }

                        var result = await _manager.Insert(AccountInsertModel);
                        if (result == true)
                            return Ok("Account Registered Successfully...!");
                        else
                            return BadRequest("Error While Registering Customer, Please Try again Later...!");
                    }


                }
                else
                {
                    return BadRequest("Something Went Wrong, Please try again later...!");
                }
            }
            else
            {
                _logger.LogWarning("Error: Invalid Account Information...!");
                return BadRequest("Invalid Customer Information, Please Enter Valid Credentials...!");
            }

        }

        [HttpPost(nameof(InsertSaving))]
        public async Task<IActionResult> InsertSaving([FromForm] AccountInsertModel AccountInsertModel)
        {
            if (ModelState.IsValid)
            {
                var usertype = await _serviceUserType.Find(x => x.AccountTypeName == "Saving");
                if (usertype != null)
                {

                    var CheckUser = await _serviceCustomer.Find(x => x.AccountId == AccountInsertModel.AccountId);

                    if (CheckUser != null)
                    {
                        return BadRequest("User ID : " + AccountInsertModel.AccountId + " already Exist...!");
                    }
                    else
                    {
                        var CheckUsername = await _serviceCustomer.Find(x => x.AccountNumber == AccountInsertModel.AccountNumber);
                        if (CheckUsername != null)
                        {
                            return BadRequest(" UserName :" + AccountInsertModel.AccountNumber + " already Exist...!");
                        }

                        var result = await _serviceCustomer.Insert(AccountInsertModel);
                        if (result == true)
                            return Ok("Account Registered Successfully...!");
                        else
                            return BadRequest("Error While Registering Customer, Please Try again Later...!");
                    }


                }
                else
                {
                    return BadRequest("Something Went Wrong, Please try again later...!");
                }
            }
            else
            {
                _logger.LogWarning("Error: Invalid Customer Information...!");
                return BadRequest("Invalid Customer Information, Please Enter Valid Credentials...!");
            }

        }


        [HttpDelete(nameof(DeleteCurrentAccount))]

        public async Task<IActionResult> DeleteCurrentAccount(int Id)
        {
            var result = await _serviceCustomer.Delete(Id);
            if (result == true)
                return Ok("Supplier Deleted Successfully...!");
            else
                return BadRequest("Something Went Wrong, Supplier Is Not Deleted, Please Try After Sometime...!");
        }

        [HttpGet(nameof(GetAllSavingAccount))]
        public async Task<ActionResult<AccountViewModel>> GetAllSavingAccount()
        {
            var result = await _serviceCustomer.GetAll();
            if (result == null)
                return BadRequest("No Records Found, Please Try Again After Adding them...!");
            return Ok(result);

        }

        [HttpGet(nameof(GetSavingAccount))]

        public async Task<IActionResult> GetSavingAccount(int Id)
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

        [HttpDelete(nameof(DeleteSavingAccount))]

        public async Task<IActionResult> DeleteSavingAccount(int Id)
        {
            var result = await _serviceCustomer.Delete(Id);
            if (result == true)
                return Ok("Supplier Deleted Successfully...!");
            else
                return BadRequest("Something Went Wrong, Supplier Is Not Deleted, Please Try After Sometime...!");
        }
    }
}

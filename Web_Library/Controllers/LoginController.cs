
using Domain_Library.Helper;
using Domain_Library.Models;
using Domain_Library.View_Model;
using Infrastructure_Library.PasswordSecurity;
using Infrastructure_Library.Services.CustomServices.CustomerServices;
using Infrastructure_Library.Services.CustomServices.ManagerServices;
using Infrastructure_Library.Services.GeneralServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_Library.Middleware.Auth;

namespace Web_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    { 
            private readonly ILogger _logger;
            private readonly IJWTAuthManager _authManager;
            private readonly IService<UserType> _serviceUserType;
            private readonly IWebHostEnvironment _environment;
            private readonly ICustomerService _customerService;
            private readonly IManagerService _managerService;

            public LoginController(ILogger<LoginController> logger, IJWTAuthManager authManager, IService<UserType> service, IWebHostEnvironment webHostEnvironment, ICustomerService customerService, IManagerService managerService)
            {
                _logger = logger;
                _authManager = authManager;
                _serviceUserType = service;
                _environment = webHostEnvironment;
                _customerService = customerService;
                _managerService = managerService;
            }

            [HttpPost("LoginUser")]
            [AllowAnonymous]
            public async Task<IActionResult> UserLogin(LoginModel Loginuser)
            {
                Response<string> response = new();
                if (ModelState.IsValid)
                {
                    var user = await _managerService.Find(x => x.UserName == Loginuser.UserName && x.Password == Encryptor.EncryptString(Loginuser.Password));
                    if (user == null)
                    {
                        response.Message = "Invalid Username / Password, Please Enter Valid Credentials...!";
                        response.Status = (int)HttpStatusCode.NotFound;
                        return NotFound(response);
                    }
                    response.Message = _authManager.GenerateJWT(user);
                    response.Status = (int)HttpStatusCode.OK;
                    return Ok(response);
                }
                else
                {
                    response.Message = "Invalid Login Information, Please Enter Valid Credentials...!";
                    response.Status = (int)HttpStatusCode.NotAcceptable;
                    return BadRequest(response);
                }

            }

            [HttpPost(nameof(RegisterCustomer))]
            public async Task<IActionResult> RegisterCustomer([FromForm] UserInsertModel supplierModel)
            {
                if (ModelState.IsValid)
                {
                    var usertype = await _serviceUserType.Find(x => x.TypeName == "Customer");
                    if (usertype != null)
                    {

                        var CheckUser = await _customerService.Find(x => x.UserId == supplierModel.UserId);
                        if (CheckUser != null)
                        {
                            return BadRequest("User ID : " + supplierModel.UserId + " already Exist...!");
                        }
                        else
                        {
                            var CheckUsername = await _customerService.Find(x => x.UserName == supplierModel.UserName);
                            if (CheckUsername != null)
                            {
                                return BadRequest(" UserName :" + supplierModel.UserName + " already Exist...!");
                            }
                        }
                        var result = await _customerService.Insert(supplierModel);
                        if (result == true)
                            return Ok("Customer Registered Successfully...!");
                        else
                            return BadRequest("Error While Registering Supplier, please Try again Later...!");

                    }
                    else
                        return BadRequest("Something Went Wrong, Please try again later...!");
                }
                else
                    return BadRequest("Invalid Customer Information, Please Enter Valid Credentials...!");

            }
            [HttpPost(nameof(RegisterManager))]
            public async Task<IActionResult> RegisterManager([FromForm] UserInsertModel supplierModel)
            {
                if (ModelState.IsValid)
                {
                    var usertype = await _serviceUserType.Find(x => x.TypeName == "Manager");
                    if (usertype != null)
                    {

                        var CheckUser = await _managerService.Find(x => x.UserId == supplierModel.UserId);
                        if (CheckUser != null)
                        {
                            return BadRequest("User ID : " + supplierModel.UserId + " already Exist...!");
                        }
                        else
                        {
                            var CheckUsername = await _managerService.Find(x => x.UserName == supplierModel.UserName);
                            if (CheckUsername != null)
                            {
                                return BadRequest(" UserName :" + supplierModel.UserName + " already Exist...!");
                            }
                        }
                        var result = await _managerService.Insert(supplierModel);
                        if (result == true)
                            return Ok("Manager Registered Successfully...!");
                        else
                            return BadRequest("Error While Registering Supplier, please Try again Later...!");

                    }
                    else
                        return BadRequest("Something Went Wrong, Please try again later...!");
                }
                else
                    return BadRequest("Invalid Manager Information, Please Enter Valid Credentials...!");

            }


        }
    }

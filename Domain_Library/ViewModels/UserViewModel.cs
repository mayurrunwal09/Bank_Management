
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Library.View_Model
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public List<UserTypeViewModel> UserType { get; set; } = new List<UserTypeViewModel>();

    }

    public class UserInsertModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User First Name is required")]
        [Display(Name = "Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "User Last Name is required")]
        [Display(Name = "Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Id is required")]
        [Display(Name = "Enter Email-Id")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Enter Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "Enter UserName")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "User Name must be between 3 and 20 characters")]
        public string UserName { get; set; }

        [DefaultValue(true)]
        [DisplayName("Active")]
        public bool? IsActive { get; set; }

    }

    public class UserUpdateModel : UserInsertModel
    {
        public int Id { get; set; }
    }

    public class LoginModel
    {

        [Required(ErrorMessage = "Please Enter UserName...!")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}

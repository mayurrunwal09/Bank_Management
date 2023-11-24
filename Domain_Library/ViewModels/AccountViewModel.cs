
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain_Library.View_Model
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public int Balance { get; set; }
        public int UserId { get; set; }
        public bool? IsActive { get; set; }


        public List<AccountTypeViewModel> AccountType { get; set; } = new List<AccountTypeViewModel>();
    }

    public class AccountInsertModel
    {
        [Required(ErrorMessage = "Enter valid Account ID")]
        [Display(Name = "Enter User Id")]
        public int AccountId { get; set; }


        [Required(ErrorMessage = "Enter valid Account Number")]
        [Display(Name = "Enter Account Number")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Enter valid Balance")]
        [Display(Name = "Enter Balance")]
        public int Balance { get; set; }


        [Required(ErrorMessage = "Enter valid User ID")]
        [Display(Name = "Enter User Id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter valid AccountType Id")]
        [Display(Name = "Enter AccountType Id")]
        public int AccountTypeId { get; set; }
        [DefaultValue(true)]
        [DisplayName("Active")]
        public bool? IsActive { get; set; }
    }

    public class AccountUpdateModel : AccountInsertModel
    {
        public int Id { get; set; }
    }

}

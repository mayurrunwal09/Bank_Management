
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Library.View_Model
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter valid UserID")]
        [Display(Name ="Enter User Id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter valid Account ID")]
        [Display(Name = "Enter Account Id")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Please Enter valid Transaction ID")]
        [Display(Name = "Enter Transaction Id")]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Please Enter Amount")]
        [Display(Name = "Enter Amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Please Enter Date")]
        [Display(Name = "Enter Date")]
        public DateTime TransactionDate { get; set; }
       
        public bool? IsActive { get; set; }

        public List<TransactionTypeViewModel> TransactionType { get; set; } = new List<TransactionTypeViewModel>();
        public List<AccountViewModel> Transactions { get; set; } = new List<AccountViewModel>();
        public List<UserViewModel> userViewModels { get; set; } = new List<UserViewModel>();

    }
    public class TransactionInsertModel
    {
        public int TransactionId { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; }

        public int UserId { get; set; }

        [DefaultValue(true)]
        [DisplayName("Active")]
        public bool? IsActive { get; set; }
    }

    public class TransactionUpdateModel : TransactionInsertModel
    {
        public int Id { get; set; }
    }



}

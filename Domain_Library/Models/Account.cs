using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain_Library.Models
{
    public class Account : BaseEntity
    {
        [Required(ErrorMessage = "Enter valid Account ID")]
        [Display(Name = "Enter User Id")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Enter valid User ID")]
        [Display(Name = "Enter User Id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter Balance")]
        [Display(Name = "Enter Balance Id")]
        public int Balance { get; set; }

        [Required(ErrorMessage = "Enter valid AccountType Id")]
        [Display(Name = "Enter AccountType Id")]
        public int AccountTypeId { get; set; }

        [Required(ErrorMessage = "Enter valid AccountNumber")]
        [Display(Name = "Enter Account Number")]
        public string AccountNumber { get; set; }


        [JsonIgnore]
        public User User { get; set; }
        public AccountType AccountType { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}

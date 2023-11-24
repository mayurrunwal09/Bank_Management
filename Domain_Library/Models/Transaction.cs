using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain_Library.Models
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }
        public const int TransferTransactionTypeId = 1; // Set the appropriate constant value for transfer transactions

        [Required(ErrorMessage ="Please Enter Amount")]
        [Display(Name ="Enter Amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Please Enter Date")]
        [Display(Name = "Enter Date")]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage ="Enter valid Account ID")]
        [Display(Name ="Enter Account Id")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Enter valid TransactionType ID")]
        [Display(Name = "Enter TransactionType ID")]
        public int TransactionTypeId { get; set; }

        [Required(ErrorMessage = "Enter valid User ID")]
        [Display(Name = "Enter User Id")]
        public int UserId { get; set; }
  


        [JsonIgnore]
         public User User { get; set; }
        public Account Account { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}

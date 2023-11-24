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
    public class TransactionType : BaseEntity
    {
        [Required(ErrorMessage = "Transaction Type is required")]
        [Display(Name = "Please enter Transaction Type")]
        public string TransactionTypeName { get; set; }
        [JsonIgnore]
        public virtual List<Transaction> Transactions { get; set; }
    }
}

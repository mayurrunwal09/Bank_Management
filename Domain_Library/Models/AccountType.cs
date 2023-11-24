using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain_Library.Models
{
    public class AccountType : BaseEntity
    {
        [Required(ErrorMessage ="Please enter account type ")]
        [Display(Name ="Enter Account type")]
        public string AccountTypeName { get; set; }


        [JsonIgnore]
        public List<Account> Accounts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain_Library.View_Model
{
    public class AccountTypeViewModel
    {
        public int Id { get; set; }
        public string AccountTypeName { get; set; }
    }

    public class AccountTypeInsertModel
    {
        [Required(ErrorMessage = "Please enter account type ")]
        [Display(Name = "Enter Account type")]
        public string AccountTypeName { get; set; }
    }

    public class AccountTypeUpdateModel : AccountTypeInsertModel
    {
        public int Id { get; set; }
    }
}

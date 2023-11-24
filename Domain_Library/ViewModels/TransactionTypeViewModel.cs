using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain_Library.View_Model
{
    public class TransactionTypeViewModel
    {
        public int Id { get; set; }
        public string TransactionTypeName { get; set; }
    }

    public class TransactionTypeInsertModel
    {
        [Required(ErrorMessage = "Transaction Type is required")]
        [Display(Name = "Please enter Transaction Type")]
        public string TransactionTypeName { get; set; }
    }

    public class TransactionTypeUpdateModel : TransactionTypeInsertModel
    {
        public int Id { get; set; }
    }
}

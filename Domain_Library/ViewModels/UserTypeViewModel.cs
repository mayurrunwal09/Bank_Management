using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain_Library.View_Model
{
    public class UserTypeViewModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }

    public class UserTypeInsertModel
    {
        [Required(ErrorMessage = "UserType is required")]
        [Display(Name = "Please enter UserType")]
        public string TypeName { get; set; }

    }

    public class UserTypeUpdateModel : UserTypeInsertModel
    {
        public int Id { get; set; }
    }

}

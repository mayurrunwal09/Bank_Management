using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain_Library.Models
{
    public class UserType :BaseEntity
    {
        [Required(ErrorMessage ="UserType is required")]
        [Display(Name ="Please enter UserType")]
        public string TypeName { get; set; }
        [JsonIgnore]
        public virtual List<User> Users { get; set; }
    }
}

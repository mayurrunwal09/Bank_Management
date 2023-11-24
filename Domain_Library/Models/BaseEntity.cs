using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Library.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        [DefaultValue(true)]
        [DisplayName("Active")]
        public bool? IsActive { get; set; }
    }
}

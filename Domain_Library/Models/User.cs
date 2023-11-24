﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace Domain_Library.Models
{
    public  class User : BaseEntity
    {

        public int UserId { get; set; }
        [Required(ErrorMessage ="User First Name is required")]
        [Display(Name ="Enter First Name")]
        public string FirstName { get; set; }

                
        [Required(ErrorMessage = "User Last Name is required")]
        [Display(Name = "Enter Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email Id is required")]
        [Display(Name = "Enter Email-Id")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Enter Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Display(Name ="Enter UserName")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "User Name must be between 3 and 20 characters")]
        public string UserName { get; set; }

        public int UserTypeId { get; set; }
        [JsonIgnore]
        public UserType UserType { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<Account> Accounts { get; set; }
    }
}


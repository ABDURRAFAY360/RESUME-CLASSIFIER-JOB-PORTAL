//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ipt_Project_Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int id { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "This Field is Required.")]
        public string First_name { get; set; }


        [Required(ErrorMessage = "This Field is Required.")]
        [DisplayName("Last Name")]
        public string Last_name { get; set; }


        [Required(ErrorMessage = "This Field is Required.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "This Field is Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "This Field is Required.")]
        [DisplayName("Current Education")]
        public string Current_education { get; set; }


        [Required(ErrorMessage = "This Field is Required.")]
        [DisplayName("Employement Status")]
        public string Employment_status { get; set; }



        [Required(ErrorMessage = "This Field is Required.")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number")]
        [DisplayName("Contact Number")]
        public string Phone_number { get; set; }


        [Required(ErrorMessage = "This Field is Required.")]
        public string City { get; set; }


        [Required(ErrorMessage = "This Field is Required.")]
        public string Country { get; set; }
    }
}

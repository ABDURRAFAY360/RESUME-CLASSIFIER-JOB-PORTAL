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
    using System.Web;
    public partial class Job_post
    {
        public int Job_id { get; set; }
        [DisplayName("Job Description")]
        [Required(ErrorMessage = "This Field is Required.")]
        [StringLength(500)]
        public string Job_description { get; set; }
        [DisplayName("Job Designation")]
        [Required(ErrorMessage = "This Field is Required.")]
        [StringLength(500)]
        public string Job_designation { get; set; }
        public int Employer_id { get; set; }
        [DisplayName("Upload Job Description")]
        public HttpPostedFileBase UploadFile { get; set; }
       
       
    }
}

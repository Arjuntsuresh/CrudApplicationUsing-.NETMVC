using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleCrudApplication.Models
{
    public class Account
    {
        [Display(Name = "Account ID")]
        public int id { get; set; }

        [Display(Name = "Account holder")]
        public string AccHolder { get; set; }

        [Display(Name = "Account number")]
        public string AccNumber { get; set; }

        [Display(Name = "IFSC number")]
        public string IFSCNumber { get; set; }

        [Display(Name = "Branch name")]
        public string Branch { get; set; }

    }
}
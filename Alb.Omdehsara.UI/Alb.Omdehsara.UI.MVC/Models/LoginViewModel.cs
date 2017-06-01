using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alb.Omdehsara.UI.MVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "نام")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "مرا به یاد داشته باش?")]
        public bool RememberMe { get; set; }
    }
}
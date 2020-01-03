using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PB.PLBS.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "用户登录标识必须填写")]
        [Display(Name = "登录标识")]
        public string LogginIdent { get; set; }

        [Required(ErrorMessage ="登录密码必须填写")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}

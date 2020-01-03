using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PB.PLBS.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "登录标识")]
        public string LogginIdent { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "密码 {0} 必须最少 {2} 最多 {1} 个字符.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不一致")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(70)]
        [Display(Name = "姓名")]
        public string FirstName { get; set; }

        [MaxLength(70)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "手机号码")]
        public string PhoneNumber { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Starshine.Authservice.Dto
{
    public class RegisterViewModel
    {
        /// <summary>
        /// 注册完成后跳转地址
        /// </summary>
        public string ReturnUrl { get; set; }

        [Required(ErrorMessage = "{0}是必填项")]
        [Display(Name = "昵称")]
        public string RealName { get; set; }

        [Required(ErrorMessage = "{0}是必填项")]
        [Display(Name = "登录名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}是必填项")]
        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// ^.*    指任意字符
        /// (?=.*[0-9]) 任意字符串后有一数字
        ///(?=.*[A-Z]) 任意字符串后有一大写字母
        ///(?=.*[a-z])  任意字符串后有一小写字母
        /// /(?=.*[!@#$%^&*?]) 任意字符串后有一括号里的特殊字符
        /// \w{ 6,}指长度要大于6位
        /// </summary>
        [Required(ErrorMessage = "{0}是必填项")]
        [DataType(DataType.Password)]
        [RegularExpression("^.*(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*?]).{6,16}",
            ErrorMessage = "密码必须包含数字、大小写字母、和一个特殊符号,且长度为6~16")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "确认密码和密码不一致")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "性别")]
        public int Sex { get; set; } = 0;

        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; } = DateTime.Now;

    }
}

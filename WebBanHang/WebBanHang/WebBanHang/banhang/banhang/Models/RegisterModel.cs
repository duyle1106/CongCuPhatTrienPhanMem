using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace banhang.Models
{
    public class RegisterModel
    {   
        [Key]
        public long ID { set; get; }

        [Display(Name="Tên Đăng Nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string UserName { set; get; }
        [Display(Name = "Mật Khẩu")]
        [StringLength(20,MinimumLength = 6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 kí tự.")]
        [Required(ErrorMessage ="Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password",ErrorMessage ="Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Nhập họ tên")]
        [Required(ErrorMessage ="Yêu cầu nhập tên chính xác")]
        public string Name { set; get; }
        [Display(Name = "Nhập địa chỉ")]
        public string Address { set; get; }
        [Display(Name = "Nhập email")]
        [Required(ErrorMessage = "Yêu cầu nhập email chính xác")]
        public string Email { set; get; }
        [Display(Name = "Nhập số điện thoại")]
        public string Phone { set; get; }
    }
}
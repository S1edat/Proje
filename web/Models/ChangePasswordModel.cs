using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ChangePasswordModel
    {
       [Required]  
       [DisplayName ("Mevcut Parolanızı giriniz")]
        public string OldPassword  { get; set; }
        [Required]
        [DisplayName("Yeni Parolanızı giriniz")]
        [StringLength (8,MinimumLength =5,ErrorMessage ="sifreniz en az 5 karakter en fazla 8 karakter olmalıdır..")]
        public string NewPassword { get; set; }

        [Required]
        [DisplayName("Parola tekrar giriniz")]
       [Compare("NewPassword",ErrorMessage="sifreler aynı değil...")]
        public string ConNewPassword { get; set; }
    }
}
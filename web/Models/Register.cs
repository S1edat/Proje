using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Adınız...")]
      public string Name { get; set; }

        [Required]
        [DisplayName ("Soyisminiz..")]
        public string Surname { get; set; }

        [Required]
        [DisplayName("Kullanıcı Adınızz....")]
        public string UserName { get; set; }

        [Required]
        [DisplayName ("Mail Adresi.." )]
        [ EmailAddress (ErrorMessage ="Geçersiz email adresimizdir....")]
        public string Email { get; set; }

        [Required]
        [DisplayName ("Şifre")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Şifre Tekrar....")] 
        [Compare ("Password",ErrorMessage ="Girdiğiniz şifreler eşleşmemektedir")]
        public string RePassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class Login
    {

        [Required]
        [DisplayName("Kullanıcı Adınızı giriniz....")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Sifrenizi giriniz....")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
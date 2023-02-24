using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class UserProfile
    {

        public string id { get; set; }

        [Required]
        [DisplayName("Adı..")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Soyadı..")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("KullanıcıAdı")]
        public string UserName { get; set; }
        
         [Required]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage ="uygun formatta e mail giriniz..")]
        public string Email { get; set; }
    }
}
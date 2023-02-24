using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.Models
{
    public class ShippingDetails
    {
        public string UserName { get; set; }

        [Required (ErrorMessage ="Lütfen adresinizi Giriniz. ")]
       
        public string Adres { get; set; }

        [Required(ErrorMessage = "Lütfen sehrinizi Giriniz")]
        public string Sehir { get; set; }
        [Required(ErrorMessage = "Lütfen Semtinizi Giriniz")]
        public string Semt { get; set; }
        [Required(ErrorMessage = "Lütfen Mahallenizi Giriniz")]
        public string Mahalle { get; set; }
        [Required(ErrorMessage = "Lütfen Posta Kodunuzu  Giriniz")]
        public string PostaKodu { get; set; }
    }
}
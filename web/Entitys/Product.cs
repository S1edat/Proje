using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Entitys
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string İmage { get; set; }
        public double Price { get; set; }
        public int  Stock { get; set; }
        public bool Slider { get; set; }
        public bool İsHome { get; set; }
        public bool İsFeatured { get; set; }
        public bool İsAproved { get; set; }

        //HER BİR ÜRÜNÜN bİR CATEGORİ İD OLDUGU BELİRTİYOR BİREBİR İŞİLKİ EGER Kİ COGA COK İLİSKİ İSE LİST YAP BİREBİR İSE İNT YAP
        public int CategoryId { get; set; }
        public Category Category  { get; set; }
    }
}
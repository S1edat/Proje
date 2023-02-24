using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Entitys
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            // database içine verleri ekleme

            var kategoriler = new List<Category>()// list içindeki categoriyi kategori olarak attı
            {
                new Category() {Name="KAMERA",Description="KAMERA ÜRÜNLERİ" },

                new Category() {Name="TELEFON",Description="TELEFON ÜRÜNLERİ" },

                new Category() {Name="BİLGİSAYAR",Description="BİLGİSAYAR ÜRÜNLERİ" },
            };

            foreach (var Kategori in kategoriler)
            {
                context.Categories.Add(Kategori);
            }
            context.SaveChanges();

            //kategoriler sınıfına teker teker ekleme yapıyor foreach içinde dolaşarak......

            //---------------------------------------------------------------------------------

            //ürünler için aynısını veritabına olusturacagız

            //---------------------------------------------------------------------------------aşağı
            var ürünler = new List<Product>()

            {
                new Product() {Name="Samsung Galaxy Note 9" ,Description="Daha Güçlü ve Daha Becerikli.",Price=8000, Stock=5,İsAproved=true,İsFeatured=true,İsHome=true,Slider=false,CategoryId=2,İmage="5.jpg"},
                new Product() {Name="lenovo" ,Description="Dahili Depolama: 32 GB.",Price=25000, Stock=4,İsAproved=true,İsFeatured=true,İsHome=true,Slider=false,CategoryId=3,İmage="1.jpg"},
                new Product() {Name="Canon EOS R5 Body" ,Description="Aynasız Fotoğraf Makinesi Canon Eurasia Garantili",Price=5000, Stock=10,İsAproved=true,İsFeatured=true,İsHome=true,CategoryId=1,Slider=true,İmage="3.jpg"},
                new Product() {Name="Nikon" ,Description="23.1 x 15.4 mm CMOS sensörü",Price=12000, Stock=2,İsAproved=true,İsFeatured=true,İsHome=true,CategoryId=1,Slider=true,İmage="4.jpg"}
            };

            foreach (var ürün in ürünler)
            {
                context.Products.Add(ürün);
            }
            base.Seed(context); //base seed  degerini bu classtan all yani class olarak özelliklerini burdan al

        }
    }
}
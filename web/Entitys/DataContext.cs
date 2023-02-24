using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.Entitys
{
    //dbContext sınıfından miras alıyor 
    public class DataContext:DbContext
    {
        public DataContext():base ("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }
        // burası olusturacagın isim databasedeki veri tabanı ismi ile aynı olacak ...
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderLine> orderLines { get; set; }
    }
}
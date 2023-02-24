using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Sql;
using web.Entitys;
namespace web.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home

        public PartialViewResult _FeaturedProductList()
        {
            return PartialView(db.Products.Where(x => x.İsAproved && x.İsFeatured).Take(5).ToList());
        }
        public ActionResult Konum()
        {
            return View();
        }
        public ActionResult Search(string q)
        {
            var p = db.Products.Where(x => x.İsAproved == true);
            if (!string.IsNullOrEmpty(q))
            {
                p = p.Where(x => x.Name.Contains(q) || x.Description.Contains(q));
                      
            }
            return View(p.ToList());
        }
        public PartialViewResult Slider()
        {
            return PartialView(db.Products.Where(x => x.İsAproved && x.Slider).Take(5).ToList());
        }
        public ActionResult Index()
        {
            return View(db.Products.Where(x=>x.İsAproved &&x.İsHome).ToList());
        }
        public ActionResult ProductDetails(int id)
        {
            //firstalldefault degeri geriye dönecek değer gerçekten bir tane oldugu için yazılır..
            return View(db.Products.Where(x=>x.Id==id).FirstOrDefault());
        }
        public ActionResult Product()
        {
            return View(db.Products.ToList());
        }
        
        public ActionResult ProductList(int id)
        {
            return View(db.Products.Where(i=>i.CategoryId==id).ToList());
        }
    }
}
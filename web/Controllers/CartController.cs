using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Entitys;
using web.Models;
using System.Data.Entity;

namespace web.Controllers
{
     
    public class CartController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        private void SaveOrder(Cart cart,ShippingDetails model)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(1111,9999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.UserName = User.Identity.Name;
            order.OrderState = OrderState.Bekleniyor;
            order.Adres = model.Adres;
            order.Sehir = model.Sehir;
            order.Semt = model.Semt;
            order.Mahalle = model.Mahalle;
            order.PostaKodu = model.PostaKodu;
            order.OrderLines = new List<OrderLine>();
            foreach (var item in cart.cartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Quantity;
                orderline.Price = (int)(item.Quantity * item.Product.Price);
                orderline.ProductId = item.Product.Id;
                order.OrderLines.Add(orderline);
            }
            db.orders.Add(order);
            db.SaveChanges();
      
        }
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails model)
        {
            // dogrulama yapıyorsan modelstate
            var cart = GetCart();
            if (cart.cartLines.Count==0)
            {
                ModelState.AddModelError("Ürün Yok", "Sepetinizde ürün bulunmamaktadır");
            }
            // doğrulama yapmaa
            //sepette ürün varsa
            if (ModelState.IsValid)
            {
                SaveOrder(cart, model);
                cart.Clear();
                return View("SiparisTamamlandı");

            }
            else 
            {
                return View(model);
            }
         
        }

        public ActionResult Summary()
        {
            return PartialView(GetCart());
        }

        public ActionResult Summary1()
        {
            return PartialView(GetCart());
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);
            if (product!=null)
            {
                GetCart().DeletProduct(product);
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);
            if (product!=null)
            {
                GetCart().AddProduct(product,1);

            }
            return RedirectToAction("Index");
        }
        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}
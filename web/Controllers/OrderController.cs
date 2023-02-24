using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Entitys;
using web.Models;

namespace web.Controllers
{
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {

            var orders = db.orders.Select(i => new AdminOrder()
            { 
            Id=i.Id,
            OrderNumber=i.OrderNumber,
            OrderName=i.OrderName,
            OrderDate=i.OrderDate,
            Total=i.Total,
            Count=i.OrderLines.Count

            
            }).OrderByDescending(i=>i.OrderDate);

            return View(orders);
        }
        public ActionResult Details(int id)
        {
            var model = db.orders.Where(k => k.Id == id).Select(i => new OrderDetails()
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                UserName = i.UserName,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Adres = i.Adres,
                Sehir = i.Sehir,
                Semt = i.Semt,
                Mahalle = i.Mahalle,
                PostaKodu = i.PostaKodu,
                OrderLines = i.OrderLines.Select(x => new OrderLineModel()
                {
                    ProductId = x.ProductId,
                    Image = x.Product.İmage,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()


            }).FirstOrDefault();

            return View(model);
        }
        public ActionResult UpdateOrderState(int orderId,OrderState orderState)
        {
            var order = db.orders.FirstOrDefault(i => i.Id == orderId);
            if (order!=null)
            {
                order.OrderState = orderState;
                db.SaveChanges();
                return RedirectToAction("Details", new {Id=orderId });
            }
            return RedirectToAction("Index");
        }
        public ActionResult BekleyenSiparisler()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Bekleniyor).ToList();

         
            return View(model);
        }
        public ActionResult KargolananSiparisler()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Kargolandı).ToList();


            return View(model);
        }
        public ActionResult TamamlananSiparisler()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Tamamlandı).ToList();


            return View(model);
        }

        public ActionResult PaketlenenSiparisler()
        {
            var model = db.orders.Where(i => i.OrderState == OrderState.Paketlendi).ToList();


            return View(model);
        }
    }
}
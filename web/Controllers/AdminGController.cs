 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Entitys;
using web.Models;

namespace web.Controllers
{
    public class AdminGController : Controller
    {
        // GET: AdminG

        DataContext db = new DataContext();
        [Authorize(Roles = "admin")] // bu şelide yazılarak sisteme giriş yapmadan producta erişemez.role admin olarak atarsak sadece admine yetki vermiş olduk
        public ActionResult Index()
        {
            StateModel model = new StateModel();
            model.BekleyenSiparisSayisi = db.orders.Where(i => i.OrderState == OrderState.Bekleniyor).ToList().Count();
            model.TamamlananSiparisSayisi = db.orders.Where(i => i.OrderState == OrderState.Tamamlandı).ToList().Count();
            model.PaketlenenSiparisSayisi = db.orders.Where(i => i.OrderState == OrderState.Paketlendi).ToList().Count();
            model.KargolananSiparisSayisi = db.orders.Where(i => i.OrderState == OrderState.Kargolandı).ToList().Count();
            model.UrünSayisi = db.Products.Count();
            model.SiparisSayisi = db.orders.Count();
            return View(model);
        }
        public PartialViewResult BildirimMenusu()
        {
            var bildirim = db.orders.Where(i => i.OrderState == OrderState.Bekleniyor).ToList();

            return PartialView(bildirim);
        }
    }
}
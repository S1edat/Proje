using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using web.Entitys;
using web.Models;

namespace web.Controllers
{
   
    public class CategoryyController : Controller
    {
        private DataContext db = new DataContext();

        //kategro tipinde patial vie olusturacaaz

        public PartialViewResult _CategoryList()
        {

            var kategoriler = db.Categories.Select(x => new CategoryModel() {
                Id = x.Id,
                Name = x.Name,
                Count = x.Products.Count

            }).ToList();

            return PartialView(kategoriler);
        }

        // GET: Categoryy
        [Authorize(Roles = "admin")] // bu şelide yazılarak sisteme giriş yapmadan producta erişemez.role admin olarak atarsak sadece admine yetki vermiş olduk
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categoryy/Details/5
        [Authorize(Roles = "admin")] // bu şelide yazılarak sisteme giriş yapmadan producta erişemez.role admin olarak atarsak sadece admine yetki vermiş olduk
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categoryy/Create
        [Authorize(Roles = "admin")] // bu şelide yazılarak sisteme giriş yapmadan producta erişemez.role admin olarak atarsak sadece admine yetki vermiş olduk
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoryy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categoryy/Edit/5
        [Authorize(Roles = "admin")] // bu şelide yazılarak sisteme giriş yapmadan producta erişemez.role admin olarak atarsak sadece admine yetki vermiş olduk
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categoryy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categoryy/Delete/5
        [Authorize(Roles = "admin")] // bu şelide yazılarak sisteme giriş yapmadan producta erişemez.role admin olarak atarsak sadece admine yetki vermiş olduk
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categoryy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

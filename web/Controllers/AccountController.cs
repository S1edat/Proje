using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using web.Entitys;
using web.Identity;
using web.Models;

namespace web.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();


        // uygulamanın hepsi üzerinde kullanıcı sınıflarını olusturulmasını yarar.
        private UserManager<ApplicationUser> UserManager;

        //sayfa üzerindeki kullanıcılarınbn rol işlemlerini role manager saglar
        private RoleManager<ApplicationRole> RoleManager;

        // kullanıcıların tüm veri işlemlerinin yöntemlerini sağlayan metod kullanıcıları tanıtıp yönetme

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            //roller içinde tanıtıp yönetilmesi için 

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        // Registerin Get metodu 
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var resul = UserManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                return View("Update");
            }
            return View(model);

        }

        public PartialViewResult UserCount()
        {
            var u = UserManager.Users;
            return PartialView(u);
        }
        public ActionResult UserList()
        {
            var u = UserManager.Users;
            return View(u);


        }

        public ActionResult UserProfil()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            var data = new UserProfile()
            {
                id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                UserName = user.UserName



            };
            return View(data);

        }
        [HttpPost]
        public ActionResult UserProfil(UserProfile model)
        {
            var user = UserManager.FindById(model.id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = user.Email;
            user.UserName = model.UserName;
            UserManager.Update(user);
            return View("Update");
        }
        public ActionResult LogOut()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model, string returnurl)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Username, model.Password);
                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var Identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, Identityclaims);
                    if (!string.IsNullOrEmpty(returnurl))
                    {

                        return Redirect(returnurl);
                    }


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Kullanıcı bilgileriniz yanlış lütfen tekrar kontrol ediniz...");
                }

            }
            return View(model);
        }



        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Register model)
        {


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = UserManager.Create(user, model.Password);


                //  userr managerin create diye bir metodu var bu metod yeni bir create olusur..


                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        UserManager.AddToRole(user.Id, "user");


                    }
                    return RedirectToAction("Login", "Account");
                }


                else
                {
                    ModelState.AddModelError("RegisterUserEror", "Kullanıcı ekleme hatası.....");
                }


            }
            return View(model);
        }
        // GET: Account
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = db.orders.Where(i => i.UserName == username).Select(i => new UserOrder
            {
                Id = i.Id,
                OrderNumber = i.OrderName,
                OrderState = i.OrderState,
                OrderDate = i.OrderDate,
                Total = i.Total



            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(orders);
        }
        public ActionResult Details(int id)
        {
            var model = db.orders.Where(i => i.Id == id).Select(i => new OrderDetails()
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.Total,
                OrderDate = i.OrderDate,
                OrderState = OrderState.Bekleniyor,
                Adres = i.Adres,
                Sehir = i.Sehir,
                Semt = i.Semt,
                Mahalle = i.Mahalle,
                PostaKodu = i.PostaKodu,
                OrderLines = i.OrderLines.Select(k => new OrderLineModel()
                {

                    ProductId = k.ProductId,
                    Image = k.Product.İmage,
                    ProductName = k.Product.Name,
                    Quantity = k.Quantity,
                    Price = k.Price

                }).ToList()


            }).FirstOrDefault();

            //view parantezine var model deki model döndürüldü...

            return View(model);
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using web.Entitys;
using web.Models;

namespace web.Controllers
{
    public class DolarController : Controller
    {
        // GET: Dolar
        public ActionResult Index()
        {
            return View();
        }



        public PartialViewResult kur()
        {

            List<DovızKur> KurList = null;
            WebClient client = new WebClient();
            var json = client.DownloadString("http://176.53.2.82:5047/DFN?User=atayatirim?CMD=TopBarJson1");

     
                KurList = JsonConvert.DeserializeObject<List<DovızKur>>(json);
      
       


            return PartialView(KurList);

        }
    }
}
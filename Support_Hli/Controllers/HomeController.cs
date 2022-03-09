using Support_Hli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Support_Hli.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Affiche()
        {
            return View();
        }
        public ActionResult Index()
        {

            return View();

        }

        public ActionResult About(Contact contact)
        {
            var resultList = new List<Contact>();
            using (var Client = new HttpClient())
            {
                var getDataTAsk = Client.GetAsync("https://localhost:44316/api/Contact")
                   .ContinueWith(Response =>
                   {
                       var result = Response.Result;
                       if (result.StatusCode == System.Net.HttpStatusCode.OK)
                       {
                           var readResult = result.Content.ReadAsAsync<List<Contact>>();
                           readResult.Wait();
                           resultList = readResult.Result;
                       }
                   });
                getDataTAsk.Wait();
            }
            for (int i = 0; i < resultList.Count; i++)
            {
                if (resultList[i].Email == contact.Email && resultList[i].password == contact.password)
                {
                    Session["Email"] = resultList[i].full_name;
                    return RedirectToAction("Dashbord");
                }

            }
            return View();
        }

        public ActionResult Dashbord()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
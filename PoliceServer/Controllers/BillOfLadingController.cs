using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliceServer.Controllers
{
    public class BillOfLadingController : Controller
    {

        [HttpGet]
        public ActionResult ShowBillOfLading()
        {
            ViewData["BOL"] = Session["BOL"];
            Session.Remove("BOL");
            return View();
        }

    }
}

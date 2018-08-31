using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoliceServer.Attribute;
using PoliceServer.CustomsValueDeclarationInformation;
using PoliceServer.Shared;

namespace PoliceServer.Controllers
{
    [PoliceAuthorize]
    public class ValueDeclearationController : Controller
    {
        //
        // GET: /ValueDeclearation/
        [HttpGet]
        public ActionResult SearchValueDeclearation()
        {
            Session.Remove("ValueDeclaration");
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "GetValueDeclearation")]
        public ActionResult GetValueDeclearation()
        {
            try
            {
                string parvaneSerial = Request.Form["parvaneSerial"];
                JsonResultWithObject<customsValueDeclaration[]> result =
                    Logics.BitaServices.CustomsValueDeclarationInformation(parvaneSerial);
                if (result.isSuccess)
                {
                    Session["ValueDeclaration"] = result.result.ToList();
                    return RedirectToAction("ShowValueDeclearation", "ValueDeclearation");
                }
                Session["Error"] = result.messages[0];
                return RedirectToAction("SearchValueDeclearation", "ValueDeclearation");
            }
            catch (Exception ex)
            {
                Session["Error"] = "ارتباط با گمرک برقرار نشد.";
                return RedirectToAction("SearchValueDeclearation", "ValueDeclearation");
            }
        }

        [HttpGet]
        public ActionResult ShowValueDeclearation()
        {
            ViewData["ValueDeclaration"] = Session["ValueDeclaration"];
            return View();
        }
    }
}

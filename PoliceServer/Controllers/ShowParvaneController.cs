using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Web.Mvc;
using PoliceServer.Attribute;
using PoliceServer.ParvaneBita;
using PoliceServer.Utilities;

namespace PoliceServer.Controllers
{
    [PoliceAuthorize]
    public class ShowParvaneController : Controller
    {
        internal readonly log4net.ILog Log = log4net.LogManager.GetLogger
           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /ShowParvane/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string n)
        {
            try
            {
                string parvaneSerial = Request.Form["parvaneSerial"];
                Session["CustomsDeclaration"] = Logics.BitaServices.GetParvane(parvaneSerial); ;
                return RedirectToAction("ShowResult", "ShowParvane");

            }
            catch (CryptographicException exx)
            {
                Session["Error"] = "خطای داخلی در ارتباط با مرکز.";
                return RedirectToAction("Index", "ShowParvane");
            }
            catch (FaultException fex)
            {
                Session["Error"] = "شماره سریال معتبر نیست.";
                return RedirectToAction("Index", "ShowParvane");
            }
            catch (Exception ex)
            {
                Session["Error"] = "ارتباط با گمرک برقرار نشد.";
                return RedirectToAction("Index", "ShowParvane");
            }
            
        }

        [HttpGet]
        public ActionResult ShowResult()
        {
            ViewData["CustomsDeclaration"] = Session["CustomsDeclaration"];
            Session.Remove("CustomsDeclaration");
            return View();
        }
    }
}

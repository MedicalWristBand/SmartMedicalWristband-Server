using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using PoliceServer.Attribute;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Repository;
using PoliceServer.Utilities;

namespace PoliceServer.Controllers
{
    [PoliceAuthorize]
    public class ConfirmationReportController : Controller
    {
        internal readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /ConfirmationReport/

        [HttpGet]
        public ActionResult ByUser()
        {
            ViewData["allConfirmationForUser"] = Session["allConfirmationForUser"];
            Session.Remove("allConfirmationForUser");
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SearchByUser")]
        public ActionResult SearchByUser()
        {
            string nationalCode = Request.Form["nationalCode"];
            try
            {
                List<ConfirmedPatte> result = ConfirmedPatteRepository.GetInstance().FindByUserNationalCode(nationalCode);
                if (result != null && result.Count > 0)
                {
                    Session["allConfirmationForUser"] = result;
                    return RedirectToAction("ByUser", "ConfirmationReport");
                }
                Session["Error"] = "شماره ملی وارد شده صحیح نیست و یا هیچ تاییدیه ای با این شماره ملی ثبت نشده است.";
                return RedirectToAction("ByUser", "ConfirmationReport");
            }
            catch (EntityNotFoundException ex)
            {
                Session["Error"] = ex.Message;
                return RedirectToAction("ByUser", "ConfirmationReport");
            }
            
        }

        [HttpGet]
        public ActionResult ByPasgah()
        {
            ViewData["allConfirmationForPasgah"] = Session["allConfirmationForPasgah"];
            Session.Remove("allConfirmationForPasgah");
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SearchByPasgah")]
        public ActionResult SearchByPasgah()
        {
            //TODO
            string pasgahName = Request.Form["ctl00$MainContent$Pasgah"];
            int pasgahId = PasgahHelper.GetAllPasgahNamesAndID(PasgahRepository.GetInstance().GetAllPasgahs())[pasgahName];
            try
            {
                List<ConfirmedPatte> result = ConfirmedPatteRepository.GetInstance().FindByPasgahId(pasgahId);
                if (result != null && result.Count > 0)
                {
                    Session["allConfirmationForPasgah"] = result;
                    return RedirectToAction("ByPasgah", "ConfirmationReport");
                }
                Session["Error"] = "هیچ تاییدیه ای در این مرکز ثبت نشده است.";
                return RedirectToAction("ByPasgah", "ConfirmationReport");
            }
            catch (EntityNotFoundException ex)
            {
                Session["Error"] = ex.Message;
                return RedirectToAction("ByPasgah", "ConfirmationReport");
            }
            catch (Exception ex)
            {
                Log.Error("در هنگام گزارش گیری بر اساس مرکز استعلام خطای نامشخص رخ داده است." , ex);
                Session["Information"] = "خطای داخلی رخ داده است.";
                return RedirectToAction("MessagePage", "Default");
            }
        }

        
    }
}

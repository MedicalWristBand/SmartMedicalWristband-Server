using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PoliceServer.Attribute;
using PoliceServer.BillOfLadingInformation;
using PoliceServer.CustomsValueDeclarationInformation;
using PoliceServer.GetUrbanWarehousePermit;
using PoliceServer.Models;
using PoliceServer.Repository;
using PoliceServer.Shared;
using PoliceServer.Utilities;
using WebGrease.Css.Extensions;

namespace PoliceServer.Controllers
{
    [PoliceAuthorize]
    public class PatteController : Controller
    {
        internal readonly log4net.ILog Log = log4net.LogManager.GetLogger
           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string patteNo;
        private string pCityCode;
        private string pThree;
        private string pAlphabet;
        private string pTwoLeft;

        //
        // GET: /Patte/
        [HttpGet]
        public ActionResult Index()
        {
            Session.Remove("patte");
            Session.Remove("ValueDeclaration");
            Session.Remove("infoException");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string n)
        {
            //TODO REFACTOR get element in post data should be refactored
            pTwoLeft = Request.Form.Get(CommonUtilities.CorrectPostedElement("UUPlaque$txtTwoLeft"));
            pAlphabet = Request.Form.Get(CommonUtilities.CorrectPostedElement("UUPlaque$txtAlphabet"));
            pThree = Request.Form.Get(CommonUtilities.CorrectPostedElement("UUPlaque$txtThree"));
            pCityCode = Request.Form.Get(CommonUtilities.CorrectPostedElement("UUPlaque$txtCityCode"));
            patteNo = Request.Form.Get(CommonUtilities.CorrectPostedElement("UUPlaque$txtPatteNo"));

            if (!IsPlaqueEnterd() && patteNo.IsNullOrWhiteSpace())
            {
                Session["Error"] = "برای دریافت اطلاعات پته، پلاک و یا شماره سریال پته را وارد نمایید.";
                return RedirectToAction("Index", "Patte");
            }

            JsonResultWithObject<Patte> response;
            if (IsPlaqueEnterd())
            {
                response = Logics.BitaServices.GetPatte(GetPlaqueCorrectFormat(), true);
            }
            else
            {
                if (patteNo.StartsWith("uwp"))
                {
                    JsonResultWithObject<Patte> uwPateResponse = Logics.BitaServices.GetUrbanWarehousePate(patteNo);
                    if (uwPateResponse.isSuccess)
                    {
                        Session["uwPate"] = uwPateResponse.result;
                        return RedirectToAction("ShowUwPate", "Patte");
                    }
                    Session["Error"] = uwPateResponse.messages[0];
                    return RedirectToAction("Index", "Patte");
                }
                response = Logics.BitaServices.GetPatte(patteNo, false);
            }
            if (response.isSuccess)
            {
                Session["patte"] = response.result;
//                JsonResultWithObject<customsValueDeclaration[]> result = Logics.BitaServices.CustomsValueDeclarationInformation(response.result.KotajNos.Split('*')[0]);
//                if (result.isSuccess)
//                {
//                    ICollection<Commodity> pateCommodities = response.result.Containers.ToList()[0].Commoditys;
//                    IQueryable<customsValueDeclaration> arzesh = result.result.ToList().AsQueryable().Where(v => pateCommodities.Any(c => c.CommodityHsCode.Equals(v.commodityHSCode)));
//                    arzesh.ForEach(a => a.commodityItemQuantity = pateCommodities.First(c => c.CommodityHsCode.Equals(a.commodityHSCode)).CommodityItemQuantity);
//                    arzesh.ForEach(a => a.commodityDescription = pateCommodities.First(c => c.CommodityHsCode.Equals(a.commodityHSCode)).CommodityTariffDescription);
//                    List<customsValueDeclaration> listArzesh = arzesh.DistinctBy(d=> d.commodityHSCode).ToList();
//                    Session["ValueDeclaration"] = listArzesh;
//                }
                return RedirectToAction("ShowPatte", "Patte");
            }
            else
            {
                Session["Error"] = response.messages[0];
                return RedirectToAction("Index", "Patte");
            }
        }

        private bool IsPlaqueEnterd()
        {
            if (pAlphabet.IsNullOrWhiteSpace() || pCityCode.IsNullOrWhiteSpace() || pThree.IsNullOrWhiteSpace()
                || pTwoLeft.IsNullOrWhiteSpace())
                return false;
            return true;
        }

        private string GetPlaqueCorrectFormat()
        {
            string plaque = pTwoLeft + pAlphabet + pThree + "ایران" + pCityCode;
            return plaque;
        }

        [HttpGet]
        public ActionResult ShowPatte()
        {
            ViewData["patte"] = Session["patte"];
            ViewData["infoException"] = Session["infoException"];
//            ViewData["ValueDeclaration"] = Session["ValueDeclaration"];
            try
            {
                ViewData["History"] = ConfirmedPatteRepository.GetInstance().FindByPatteSerial(((Patte)Session["Patte"]).PatteSerial);
            }
            catch (Exception ex)
            {
                //Noting
            }
            Session.Remove("infoException");
            return View();
        }

        [HttpGet]
        public ActionResult ShowUwPate()
        {
            ViewData["uwPate"] = Session["uwPate"];
            try
            {
                ViewData["History"] = ConfirmedPatteRepository.GetInstance().FindByPatteSerial(((Patte)Session["uwPate"]).PatteSerial);
            }
            catch (Exception ex)
            {
                //Noting
            }
            Session.Remove("uwPate");
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Confirm")]
        public ActionResult Confirm(FormCollection form)
        {
            int pasgahId = Int32.Parse(Request.Form["pasgahID"]);
            int pateDBId = Int32.Parse(Request.Form["patteID"]);
            string pateSerialFromBarcodeReader = Request.Form["pateSerialFromBarcodeReader"];
            string pateSerial = Request.Form["pateSerial"];

            if (!pateSerial.Equals(pateSerialFromBarcodeReader))
            {
                Session["Information"] = "برای تایید یک پته باید بارکد همان پته را مجددا با دستگاه بخوانید.";
                Session["Type"] = "Error";
                Session["redirect"] = "~/Patte/Index";
                return RedirectToAction("MessagePage", "Default");
            }

            User user = Utilities.CommonUtilities.GetUser();
            try
            {
                bool isSucceed =
                    ConfirmedPatteRepository.GetInstance()
                        .CheckIfNotRedundant(pasgahId, pateDBId);
                if (isSucceed)
                {
                    isSucceed = ConfirmedPatteRepository.GetInstance().Save(new ConfirmedPatte() { PasgahID = pasgahId, PatteID = pateDBId, UserID = user.Id, ConfirmationIp = Request.UserHostAddress });
                    if (isSucceed)
                    {
                        Session["Information"] = "پته با موفقیت تایید شد.";
                    }
                    else
                    {
                        Session["Information"] = "در ذخیره سازی تاییدیه پته خطا رخ داده است.";
                        Session["Type"] = "Error";
                    }
                }
                else
                {
                    Session["Information"] = "این پته قبلا در این مرکز ثبت شده است و امکان تایید مجدد آن وجود ندارد.";
                    Session["Type"] = "Error";

                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Uknown Error occured during saving patteConfirm with PatteID {0}", pateDBId));
                Log.Error(ex.Message);

                Session["Information"] = "این پته قبلا در این مرکز ثبت شده است و امکان تایید مجدد آن وجود ندارد.";
                Session["Type"] = "Error";

            }
            Session["redirect"] = "~/Patte/Index";
            return RedirectToAction("MessagePage", "Default");

        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ShowValueDeclearation")]
        public ActionResult ShowValueDeclearation()
        {
            try
            {
                string parvaneSerial = Request.Form["parvaneSerial"];
                JsonResultWithObject<customsValueDeclaration[]> result =
                    Logics.BitaServices.CustomsValueDeclarationInformation(parvaneSerial.Trim());
                if (result.isSuccess)
                {
                    Session["ValueDeclaration"] = result.result.ToList();
                    return RedirectToAction("ShowValueDeclearation", "ValueDeclearation");
                }
                Session["infoException"] = result.messages[0];
                return RedirectToAction("ShowPatte", "Patte");
            }
            catch (Exception ex)
            {
                Session["infoException"] = "ارتباط با گمرک برقرار نشد.";
                return RedirectToAction("ShowPatte", "Patte");
            }    
        }
        

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "showParvane")]
        public ActionResult ShowParvane()
        {
            try
            {
                string parvaneSerial = Request.Form["parvaneSerial"];
                Session["CustomsDeclaration"] = Logics.BitaServices.GetParvane(parvaneSerial);
                return RedirectToAction("ShowResult", "ShowParvane");

            }
            catch (CryptographicException exx)
            {
                Session["infoException"] = "خطای داخلی در ارتباط با مرکز.";
            }
            catch (FaultException fex)
            {
                Session["infoException"] = "شماره سریال معتبر نیست.";
            }
            catch (Exception ex)
            {
                Session["infoException"] = "ارتباط با گمرک برقرار نشد.";
            }
            return RedirectToAction("ShowPatte", "Patte");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "showBillOfLading")]
        public ActionResult ShowBillOfLading()
        {
            string driverNationalID = Request.Form["driverNationalID"];
            JsonResultWithObject<BillOfLading> bol = Logics.BitaServices.GetBillOfLading(driverNationalID);
            if (bol.isSuccess)
            {
                if (bol.result.billOfLadingNumber.Equals("چنين بارنامه اي يافت نشد"))
                {
                    Session["infoException"] = "بارنامه ای با شماره ملی راننده مورد نظر یافت نشد.";
                    return RedirectToAction("ShowPatte", "Patte");
                }
                Session["BOL"] = bol.result;
                return RedirectToAction("ShowBillOfLading", "BillOfLading");
            }
            Session["infoException"] = bol.messages[0];
            return RedirectToAction("ShowPatte", "Patte");
        }


        [HttpGet]
        public ActionResult ShwoAllPateBySerial()
        {
            ViewData["ListPate"] = Session["ListPate"];
            ViewData["pateDetailSelected"] = Session["pateDetailSelected"];
            Session.Remove("pateDetailSelected");
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "searchSerial")]
        public ActionResult ShowAllPateBySerialSerach()
        {
            string parvaneSerial = Request.Form["parvaneSerial"];
            JsonResultWithObject<List<Patte>> result = Logics.BitaServices.GetAllPateBySerial(parvaneSerial);
            if (result.isSuccess)
            {
                Session["ListPate"] = result.result;
            }
            else
            {
                Session["Error"] = result.messages[0];
            }
            return RedirectToAction("ShwoAllPateBySerial", "Patte");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "pateDatail")]
        public ActionResult ShwoAllPateBySerial(string n)
        {
            string pateId = Request.Form["pateID"];
            List<Patte> listPate = (List<Patte>)Session["ListPate"];
            Session["pateDetailSelected"] = listPate.FirstOrDefault(p => p.PatteSerial == pateId);
            return RedirectToAction("ShwoAllPateBySerial", "Patte");
        }


    }
}

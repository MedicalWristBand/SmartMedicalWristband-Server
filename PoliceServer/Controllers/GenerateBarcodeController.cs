using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoliceServer.Attribute;
using PoliceServer.Exceptions;
using PoliceServer.Repository;

namespace PoliceServer.Controllers
{
    [PoliceAuthorize]
    public class GenerateBarcodeController : Controller
    {
        //
        // GET: /GenerateBarcode/

        public ActionResult GenerateBarcode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateBarcode(string patientCode)
        {
            try
            {
                Session["PatientId"] = PatientRepository.GetInstance().FindPatientByPatientCode(patientCode).Id;
                return RedirectToAction("GenerateBarcode","GenerateBarcode");

            }
            catch (EntityNotFoundException )
            {
                return RedirectToAction("GenerateBarcode","GenerateBarcode");
            }
        }

    }
}

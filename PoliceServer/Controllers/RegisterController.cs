using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PoliceServer.AccessControl;
using PoliceServer.Attribute;
using PoliceServer.Enums;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Repository;
using PoliceServer.Utilities;

namespace PoliceServer.Controllers
{
    [PoliceAuthorize]
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(string s)
        {
            try
            {
                //TODO bayad betunam ba ye forme dorost atribute haye post ro begiram
                string name = Request.Form["ctl00$MainContent$Name"];
                string family = Request.Form["ctl00$MainContent$Family"];
                string nationalCode = Request.Form["ctl00$MainContent$NationalCode"];
                string organizationCode = Request.Form["ctl00$MainContent$OrganizationCode"];
                string password = Request.Form["ctl00$MainContent$Password"];
                string confirmPassword = Request.Form["ctl00$MainContent$ConfirmPassword"];
                List<RoleType> canAssignedRoles = AccessControl.RoleManager.GetInstance().CanAssignRoles(CommonUtilities.GetUser(false));
                List<int> indexes = new List<int>();
                Request.Form.AllKeys.Where(e => e.Contains("ctl00$MainContent$ctl16$")).ToList().ForEach(si => indexes.Add(Int32.Parse(si.Replace("ctl00$MainContent$ctl16$", ""))));
                List<RoleType> selectedRoles = new List<RoleType>();
                indexes.ForEach(sr=> selectedRoles.Add(canAssignedRoles[sr]));

                if (password.Length < 6)
                {
                    throw new UserInterfaceException("طول رمز عبور باید حداقل 6 کاراکتر باشد.");
                }
                if (!password.Equals(confirmPassword))
                {
                    throw new UserInterfaceException("رمز عبور با تکرار آن مطابقت ندارد");
                }

                Logics.UserRegistration.RegisterUser(name, family, nationalCode, organizationCode, password, selectedRoles /*, EnumHelper.ToEnum<MilitaryRank>(rank)*/);
                Session["Information"] = "ایجاد کاربر جدید با موفقیت انجام شد";
                return RedirectToAction("MessagePage", "Default");
            }
            catch (UserInterfaceException ex)
            {
                Session["Error"] = ex.Message;
                return RedirectToAction("RegisterUser", "Register");
//                ShowError(ex);
            }
        }

        [HttpGet]
        public ActionResult RegisterPasgah()
        {
            return View();
        }

        
    }
}
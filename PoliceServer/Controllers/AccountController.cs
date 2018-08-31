using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using PoliceServer.AccessControl;
using PoliceServer.Attribute;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Repository;
using PoliceServer.Utilities;

namespace PoliceServer.Controllers
{
    [PoliceAuthorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [HttpGet]
        public ActionResult ManagePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagePassword(string s)
        {
            string oldPass = Request.Form["oldPass"];
            string newPass = Request.Form["newPass"];
            string confirmPass = Request.Form["confirmPass"];
            if (!System.Text.RegularExpressions.Regex.IsMatch(newPass, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$"))
            {
                Session["Error"] = "رمز عبور میبایست به طول ۸ و شامل حروف کوچک، بزرگ و اعداد باشد.";
                return RedirectToAction("ManagePassword", "Account");
            }
            if (!newPass.Equals(confirmPass))
            {
                Session["Error"] = "رمزعبور جدید با تکرار آن مطابقت ندارد";
                return RedirectToAction("ManagePassword", "Account");
            }
            User user = Utilities.CommonUtilities.GetUser();
            if (!user.Password.Equals(oldPass))
            {
                Session["Error"] = "رمز عبور فعلی اشتباه است.";
                return RedirectToAction("ManagePassword", "Account");   
            }
            PoliceContext ctn = ContextCreator.GetInstance().GetContext();
            User userUpdate = ctn.Users.FirstOrDefault(u => u.Username.Equals(user.Username));
            if (userUpdate != null) userUpdate.Password = newPass;
            ctn.SaveChanges();
            Session["Information"] = "رمز با موفقیت تغییر کرد.";
            return RedirectToAction("MessagePage", "Default");
        }

        [HttpGet]
        public ActionResult ManageAccess()
        {
            ViewData["userToEdit"] = Session["userToEdit"];
            Session.Remove("userToEdit");
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ManageAccess")]
        public ActionResult ManageAccess(string s)
        {
            Session.Remove("userToEditController");
            string nationalCode = Request.Form["nationalCode"];
            try
            {
                User user = UserRepository.GetInstance().FindByNationalCode(nationalCode);
                if (user != null)
                {
                    Session["userToEdit"] = user;
                    Session["userToEditController"] = user;
                    return RedirectToAction("ManageAccess", "Account");    
                }
            }
            catch (EntityNotFoundException ex)
            {
                Session["Error"] = ex.Message;
                return RedirectToAction("ManageAccess", "Account"); 
            }
            return RedirectToAction("Index", "Default");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "UpdateResponsibilities")]
        public ActionResult UpdateResponsibilities(FormCollection result)
        {
            User userToEdit = (User)Session["userToEditController"];
            Session.Remove("userToEditController");
            userToEdit.Responsibilities.Clear();
            foreach (var parameter in Request.Form.AllKeys)
            {
                if (parameter.Contains("chk"))
                {
                    string role = parameter.Substring(parameter.IndexOf("chk", StringComparison.Ordinal));
                    userToEdit.Responsibilities.Add(new Responsibility((RoleType) Enum.Parse(typeof(RoleType), role.Replace("chk",""))));
                }
            }
            UserRepository.GetInstance().UpdateResponibilities(userToEdit);

            Session["Information"] = "تغییرات با موفقیت به روز رسانی شد.";
            return RedirectToAction("MessagePage", "Default");

        }
    }
}

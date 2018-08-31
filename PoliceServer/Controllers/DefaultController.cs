using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PoliceServer.Attribute;
using PoliceServer.Models;
using PoliceServer.Utilities;

namespace PoliceServer.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        [PoliceAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MessagePage()
        {
            return View();
        }

        public void LogOut()
        {
            Hashtable usersState = (Hashtable)System.Web.HttpContext.Current.Application["WEB_SESSIONS_OBJECT"];
            User user = Utilities.CommonUtilities.GetUser(false);
            if (user != null && usersState != null)
            {
                usersState.Remove(user.Id);
            }
            FormsAuthentication.SignOut();
            Utilities.CommonUtilities.Logout(System.Web.HttpContext.Current.Session);
            System.Web.HttpContext.Current.Response.Redirect("~/Account/Login.aspx", false);
        }

        
	}
}
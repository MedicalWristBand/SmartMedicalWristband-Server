using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PoliceServer.Messages;
using PoliceServer.Models;

namespace PoliceServer.Utilities
{
    public class SimplePage : System.Web.UI.Page
    {
        internal static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected SimplePage()
        {
//            Type type = GetType();
//            if (type.IsDefined(typeof(FarsiPage), true) == false)
//            {
//                throw new InvalidOperationException("Each Page Must Implement Farsi Page Interface");
//            }
//            this.Title = Utilities.FarsiPageHelper.GetFarsiPage(this).FarsiName;
        }

        protected override void OnPreInit(EventArgs e)
        {
            Log.DebugFormat(LogMessage.PageInitBegin, Request.Url);
            base.OnPreInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            Log.DebugFormat(LogMessage.PageLoadBegin, Request.Url);
            User user = GetUser();
            String url = Request.Path;

            if (Convert.ToBoolean(Session["LogOutByForce"]) == true)
            {
                FormsAuthentication.SignOut();
                Session.Remove("LogOutByForce");

                RedirectAndShowError("حساب کاربری شما بدلیل استفاده‌ی همزمان در دو سیستم خارج شد.");
            }

            //TODO
            if (GetUser() == null) 
            {
                Response.Clear();
                Response.Redirect("~/Account/Login.aspx", true);
                return;
            }
            else if (!AccessControl.RoleManager.GetInstance().HasAccessibility(user, url))
            {
                Response.Clear();
                Session["Type"] = "Error";
                Session["Information"] = "شما به این صفحه دسترسی ندارید.";
                Response.Redirect("~/Default/MessagePage");
                return;
            }
            base.OnLoad(e);
        }

        protected User GetUser(bool track = false)
        {
            return Utilities.CommonUtilities.GetUser(track);
        }

        /// <summary>
        /// کاربر را به صفحه ی جدید منتقل کرده و خطا را به وی نشان می دهد
        /// </summary>
        /// <param name="message">پیغام خطا</param>
        /// <param name="redirectUrl">آدرسی که کاربر پس از نمایش خطا به آن باز خواهد گشت</param>
        protected virtual void RedirectAndShowError(String message, string redirectUrl = null)
        {
            Session["Information"] = message;
            Session["Type"] = "Error";
            if (redirectUrl == null)
            {
                Session["redirect"] = "~/UI/Default.aspx";
            }
            else
            {
                Session["redirect"] = redirectUrl;
            }
            Response.Redirect("~/UI/Account/InformationPage.aspx");
        }
    }
}
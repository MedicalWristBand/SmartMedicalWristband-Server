using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;
using PoliceServer.Repository;

namespace PoliceServer.Account
{
    public partial class Login : Page
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string username = "";
        private string password = "";

        protected void Page_Load(object sender, EventArgs e)
        {

#if DEBUG
            String name = Environment.MachineName;
            if (name.Equals("SINA"))
            {
                username = "ssh";
                password = "zserdx";
                //                        captchaBox.Visible = false;
                LoginRequest(username, password);
            }
#endif
            if (!IsPostBack)
            {
                string[] keys = Request.Form.AllKeys;
                if (keys.Length > 0)
                {
                    username = Request.Form["username"];
                    password = Request.Form["password"];
                    LoginRequest(username, password);
                }
            }

        }

        private void LoginRequest(string username, string password)
        {
            Log.DebugFormat(LogMessage.LoginBegin, Request.UserHostAddress, username);
            try
            {
                if (UserRepository.GetInstance().CheckUsernamePassword(username, password))
                {

                    User user =UserRepository.GetInstance().FindByNationalCode(username);

                    if (Utilities.Configuration.GetInstance().IsSingleUser())
                    {

                        //getting the sessions objects from the Application
                        Hashtable usersState = (Hashtable)Application["WEB_SESSIONS_OBJECT"];
                        if (usersState == null)
                        {
                            usersState = new Hashtable();
                        }

                        //getting the pointer to the Session of the current logged in user
                        HttpSessionState existingUserSession = (HttpSessionState)usersState[user.Id];
                        if (existingUserSession != null && existingUserSession["userObj"] is User)
                        {
                            //TODO bayad log bendazam vase birun endakhtan yeki! tu hamin function paeen hast vali memarisho avaz kon.
                            //user.LogAttempt_logout(true);
                            Utilities.CommonUtilities.Logout(existingUserSession);
                            existingUserSession["LogOutByForce"] = true;
                        }

                        Session["userObj"] = user;
                        Session["UserSSN"] = user.Username;
                        //Session["stockID"] = user.pasgah;

                        usersState[user.Id] = Session;
                        Application.Lock(); //lock to prevent duplicate objects
                        Application["WEB_SESSIONS_OBJECT"] = usersState;
                        Application.UnLock();

                    }
                    else // System is NOT single user
                    {
                        Session["userObj"] = user;
                        Session["UserSSN"] = user.Username;
                    }

                    // فقط زمانی که کانفیگ مورد نظر فعال باشد برای لاگین شدن یک لاگ در دیتابیس مینویسد
                    //user.LogAttempt_login(true);

                    Log.DebugFormat(LogMessage.LoginFinished, username);
                    
                    Response.Redirect(ResolveUrl("~/Default/Index"));
                    
                }
                else
                {
                    //TODO
                    // اگر نام کاربری معتبر باشد برای ورود ناموفق لاگ می اندازیم
//                    User FailedUser = UserRepository.GetInstance().FindByUsername(username,false);
//                    if (FailedUser != null)
//                        FailedUser.LogAttempt_login(false);

                    ShowError("شناسه کاربری و یا رمز عبور صحیح نمی باشد.");
                   
                }
            }
            catch (UserInterfaceException ex)
            {
                Log.Warn(String.Format(LogMessage.LoginError, username), ex);
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(String.Format(LogMessage.LoginError, username), ex);
                ShowError("خطای نامشخص در ورود رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
            }
        }

        private void ShowError(String message)
        {
            lblError.Text = message;
            ErrorHandlerPanel.Visible = true;
        }
    }
}
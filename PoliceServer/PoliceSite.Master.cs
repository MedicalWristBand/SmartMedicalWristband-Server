using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceServer.Models;

namespace PoliceServer
{
    public partial class PoliceSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Type type = HttpContext.Current.ApplicationInstance.GetType().BaseType;
            if (type != null)
            {
                String majorVersion = type.Assembly.GetName().Version.Major.ToString();
                lblTechnichalVersion.Text = majorVersion + "." + Utilities.Configuration.GetInstance().GetVersion();
                lblBuildDate.Text = Utilities.Configuration.GetInstance().GetBuildDate();
            }
        }

        protected void HeadLoginStatus_OnLoggingOut(object sender, EventArgs eventArgs)
        {
            Hashtable usersState = (Hashtable)HttpContext.Current.Application["WEB_SESSIONS_OBJECT"];
            User user = Utilities.CommonUtilities.GetUser(false);
            if (user != null && usersState != null)
            {
                usersState.Remove(user.Id);
            }
            Utilities.CommonUtilities.Logout(HttpContext.Current.Session);
            HttpContext.Current.Response.Redirect("~/Account/Login.aspx", false);
        }

        
    }
}
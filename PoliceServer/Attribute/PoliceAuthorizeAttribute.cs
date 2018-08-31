using System.Web;
using System.Web.Mvc;
using PoliceServer.Models;

namespace PoliceServer.Attribute
{
    public class PoliceAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _authorize;

        public PoliceAuthorizeAttribute()
        {
            _authorize = true;
        }

        public PoliceAuthorizeAttribute(bool authorize)
        {
            _authorize = authorize;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = Utilities.CommonUtilities.GetUser();
            if (user == null) 
            {
                return false;
            }
            if(!AccessControl.RoleManager.GetInstance().HasAccessibility(user, httpContext.Request.Path) )
            {
                httpContext.Session["unAuthorizedRequestError"] = true;
                return false;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            
            if (filterContext.HttpContext.Session["unAuthorizedRequestError"] != null && (bool)filterContext.HttpContext.Session["unAuthorizedRequestError"])
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Session["Type"] = "Error";
                filterContext.HttpContext.Session["Information"] = "شما به این صفحه دسترسی ندارید.";
                filterContext.HttpContext.Response.Redirect("~/Default/MessagePage", true);
            }
            else
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Redirect("~/Account/Login.aspx", true);

            }
        }

    }
}
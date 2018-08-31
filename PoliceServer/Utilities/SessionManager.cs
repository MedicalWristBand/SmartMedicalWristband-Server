using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PoliceServer.Utilities
{
    public class SessionManager
    {
        private readonly HttpSessionState _sessionState;
        public static SessionManager GetInstance()
        {
            return new SessionManager();
        }
        private SessionManager()
        {
            _sessionState = HttpContext.Current.Session;
        }

        public void FreeRedirectSource()
        {
            _sessionState["redirectSource"] = null;
            _sessionState.Remove("redirectSource");
        }
    }
}
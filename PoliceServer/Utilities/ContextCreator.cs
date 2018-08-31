using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.Models;

namespace PoliceServer.Utilities
{
    public class ContextCreator : IDisposable
    {
        private ContextCreator() { }

        public static PoliceContext StaticContext { get; set; }

        public static ContextCreator GetInstance()
        {
            return new ContextCreator();
        }

        public PoliceContext GetContext()
        {
            PoliceContext context = FetchContext();
            context.Database.CommandTimeout = 600;
            context.Configuration.UseDatabaseNullSemantics = true;
            return context;
        }

        private PoliceContext FetchContext()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
#if DEBUG
                if (StaticContext == null)
                {
                    StaticContext = new PoliceContext();
                }
                return StaticContext;
#else
                return new PoliceContext();
#endif
            }

            const string key = "Police_PoliceContext";
            if (httpContext.Items[key] == null)
            {
                httpContext.Items.Add(key, new PoliceContext());
            }

            return httpContext.Items[key] as PoliceContext;
        }



        public void Dispose()
        {
            var entityContext = HttpContext.Current.Items["Police_PoliceContext"] as PoliceContext;
            if (entityContext != null)
            {
                entityContext.Dispose();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace PoliceServer.Utilities
{
    public class HexManager : IDisposable
    {
        private readonly HttpSessionState _sessionState;

        public const string Key = "USE_HEX";

        /// <summary>
        /// 
        /// </summary>
        public void DisableHex()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return;
            }

            if (httpContext.Items[Key] == null)
            {
                httpContext.Items.Add(Key, false);
            }
            else
            {
                httpContext.Items[Key] = false;
            }

        }

        /// <summary>
        /// مشخص می کند که آیا ورودی و خروجی ها باید به فرمت ۱۶ برده شوند یا خیر
        /// </summary>
        /// <remarks>
        /// این تابع به طور پیش فرض مقدار درست را بر می گرداند مگر این که واقعا خلاف آن مقداردهی شده باشد
        /// </remarks>
        /// <returns></returns>
        public bool UseCoding()
        {
            try
            {
                HttpContext httpContext = HttpContext.Current;

                if (httpContext == null)
                {
                    return true;
                }

                if (httpContext.Items[Key] == null)
                {
                    return true;
                }
                string value = httpContext.Items[Key].ToString();
                if (String.IsNullOrWhiteSpace(value))
                {
                    return true;
                }
                bool useHex = true;
                if (Boolean.TryParse(value, out useHex))
                {
                    return useHex;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return true;
        }


        public void Dispose()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return;
            }

            if (httpContext.Items[Key] == null)
            {
                return;
            }
            httpContext.Items.Remove(Key);
        }
    }
}
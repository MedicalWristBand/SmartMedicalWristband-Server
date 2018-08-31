using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceServer.Annotation
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class FarsiPage : System.Attribute
    {
        /// <summary>
        /// نام فارسی ستون
        /// </summary>
        public string FarsiName { get; set; }

        /// <summary>
        /// نام تیتر مربوط به این صفحه در منو
        /// </summary>
        /// <example>
        /// به عنوان مثال تیتر صفحه ی ثبت بیجک گزینه ی خروج کالا می باشد
        /// </example>
        public HeaderType Parent { get; set; }

        public string Icon { get; set; }

        public static FarsiPage GetFarsiPage(System.Web.UI.Page page)
        {
            Type type = page.GetType();
            System.Attribute attr = System.Attribute.GetCustomAttribute(type, typeof(FarsiPage));
            if (attr is FarsiPage)
            {
                FarsiPage farsiPage = attr as FarsiPage;
                return farsiPage;
            }
            throw new InvalidOperationException("Each Page Must Implement Farsi Page Interface");
        }
    }

    public enum HeaderType
    {
        Home,
        Report,
        Setting,
        notInMenu
    }
}
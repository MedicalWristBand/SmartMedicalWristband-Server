using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace PoliceServer.Utilities
{
    public class Configuration
    {
        internal readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly NameValueCollection _appSettings;

        public static Configuration GetInstance()
        {
            return new Configuration();
        }

        private Configuration()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }


        /// <summary>
        /// مشخص میکند که آیا با یک نام کاربری می‌توان همزمان دو بار ورود کرد یا خیر
        /// </summary>
        /// <returns></returns>
        public bool IsSingleUser()
        {
            try
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["SingleUser"]);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// مشخص میکند که آیا ماشین لوکال است یا خیر (سرور است)
        /// </summary>
        /// <returns></returns>
        public bool IsLocalMachin()
        {
            try
            {
                return ConfigurationManager.AppSettings["localMachine"].ToString().Equals("true");
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// شماره نسخه ی مربوط به این پروژه را باز می گرداند
        /// </summary>
        /// <returns></returns>
        public String GetVersion()
        {
            var version = HttpContext.Current.Application["version"];
            if (version != null)
            {
                if (!Utilities.CommonUtilities.IsEmpty(version.ToString()))
                {
                    return version.ToString();
                }
            }
            String newVersion = File.ReadLines(HttpRuntime.AppDomainAppPath + "/BUILD_NUMBER.txt").FirstOrDefault();
            HttpContext.Current.Application["version"] = newVersion;
            return newVersion;
        }

        /// <summary>
        /// تاریخ کامپایل پروژه را باز می گرداند
        /// </summary>
        /// <returns></returns>
        public String GetBuildDate()
        {
            var buildDate = HttpContext.Current.Application["buildDate"];
            if (buildDate != null)
            {
                if (!Utilities.CommonUtilities.IsEmpty(buildDate.ToString()))
                {
                    return buildDate.ToString();
                }
            }
            String newGeorgianBuildDate = File.ReadLines(HttpRuntime.AppDomainAppPath + "/BuildDate.txt").FirstOrDefault();
            String hijriBuildDate = Utilities.CommonUtilities.DateConverterMiladiToHijri(DateTime.Parse(newGeorgianBuildDate));
            HttpContext.Current.Application["buildDate"] = hijriBuildDate;
            return hijriBuildDate;
        }
    }
}
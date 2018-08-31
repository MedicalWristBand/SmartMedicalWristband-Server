using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Microsoft.Owin.Security;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Repository;

namespace PoliceServer.Utilities
{
    public class CommonUtilities
    {
        static private readonly string[] hexArray = {
        "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F",
        "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F",
        "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F",
        "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F",
        "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F",
        "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5A", "5B", "5C", "5D", "5E", "5F",
        "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6A", "6B", "6C", "6D", "6E", "6F",
        "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7A", "7B", "7C", "7D", "7E", "7F",
        "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8A", "8B", "8C", "8D", "8E", "8F",
        "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9A", "9B", "9C", "9D", "9E", "9F",
        "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "AA", "AB", "AC", "AD", "AE", "AF",
        "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA", "BB", "BC", "BD", "BE", "BF",
        "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "CA", "CB", "CC", "CD", "CE", "CF",
        "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "DA", "DB", "DC", "DD", "DE", "DF",
        "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "EA", "EB", "EC", "ED", "EE", "EF",
        "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "FA", "FB", "FC", "FD", "FE", "FF"};
        private static readonly Char[] PersianNumber = new Char[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
        private static readonly Char[] EnglishNumber = new Char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static bool CheckNationalNumber(String nationalNumber)
        {
            if (nationalNumber.Length != 10)
            {
                return false;
            }
            if (nationalNumber == "0000000000" || nationalNumber == "1111111111" || nationalNumber == "2222222222" ||
                nationalNumber == "3333333333" || nationalNumber == "4444444444" || nationalNumber == "5555555555" ||
                nationalNumber == "6666666666" || nationalNumber == "7777777777" || nationalNumber == "8888888888" ||
                nationalNumber == "9999999999")
            {
                return false;
            }

            int c = Convert.ToInt16(nationalNumber[9] - '0');
            var n = Convert.ToInt16(nationalNumber[0] - '0') * 10 +
                    Convert.ToInt16(nationalNumber[1] - '0') * 9 +
                    Convert.ToInt16(nationalNumber[2] - '0') * 8 +
                    Convert.ToInt16(nationalNumber[3] - '0') * 7 +
                    Convert.ToInt16(nationalNumber[4] - '0') * 6 +
                    Convert.ToInt16(nationalNumber[5] - '0') * 5 +
                    Convert.ToInt16(nationalNumber[6] - '0') * 4 +
                    Convert.ToInt16(nationalNumber[7] - '0') * 3 +
                    Convert.ToInt16(nationalNumber[8] - '0') * 2;
            var r = n - 11 * Convert.ToInt32(n / 11);
            return (r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r);
        }

//        public static String StringToHexString(String s, Encoding encoding)
//        {
//            if (!new HexManager().UseCoding())
//            {
//                return s;
//            }
//
//            byte[] bytes = encoding.GetBytes(s);
//            var sb = new StringBuilder();
//            for (int j = 0; j < bytes.Length; j++)
//            {
//                sb.Append((hexArray[bytes[j] & 0xFF]));
//            }
//            return sb.ToString();
//        }
        /// <summary>
        /// این تابع بررسی می کند که آیا یک استثناء از نوع استثناءهایی 
        /// است که توسط برنامه تولید شده اند یا خیر
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static Boolean IsUserInterfaceException(System.Exception exception)
        {
            if (exception == null)
            {
                return false;
            }
            System.Type exceptionType = exception.GetType();
            return exceptionType.IsSubclassOf(typeof(UserInterfaceException)) || exceptionType == typeof(UserInterfaceException);
        }


        /// <summary>
        /// کاربر را از 
        /// Session 
        /// خوانده و باز می گرداند
        /// </summary>
        /// <returns></returns>
        public static User GetUser(bool track = false)
        {
            if (HttpContext.Current == null)
            {
                return null;
            }
            HttpSessionState sessions = HttpContext.Current.Session;
            if (sessions == null)
            {
                return null;
            }
            User user = sessions["userObj"] as User;
            if (user == null)
            {
                return null;
            }
            if (track)
            {
                return UserRepository.GetInstance().FindById(user.Id);
            }
            else
            {
                return user;
            }
        }

        public static string CorrectPostedElement(string input)
        {
            if (!input.Contains("ctl00$MainContent$"))
            {
                return "ctl00$MainContent$" + input;
            }
            else
            {
                return input;
            }
        }

        public static TEntity GetItemObject<TEntity>(object dataItem) where TEntity : class
        {
            TEntity entity = dataItem as TEntity;
            if (entity != null)
            {
                return entity;
            }
            var td = dataItem as ICustomTypeDescriptor;
            if (td != null)
            {
                return (TEntity)td.GetPropertyOwner(null);
            }
            return null;
        }

        public static String StringToHexString(String s, Encoding encoding)
        {
            if (!new HexManager().UseCoding())
            {
                return s;
            }

            byte[] bytes = encoding.GetBytes(s);
            var sb = new StringBuilder();
            for (int j = 0; j < bytes.Length; j++)
            {
                sb.Append((hexArray[bytes[j] & 0xFF]));
            }
            return sb.ToString();
        }

        public static String HexStringToString(String s, Encoding encoding)
        {
            if (!new HexManager().UseCoding())
            {
                return s;
            }
            int discard;
            var d = HexEncoding.GetBytes(s, out discard);
            string res = encoding.GetString(d);
            return res;
        }

        public static Boolean IsEmpty(String text)
        {
            if (text == null)
            {
                return true;
            }
            if (text.Trim().Equals(""))
            {
                return true;
            }
            return false;
        }

        public static String DateConverterMiladiToHijri(DateTime date)
        {
            PersianCalendar perCal = new PersianCalendar();
            string Year = perCal.GetYear(date).ToString();
            string Month = perCal.GetMonth(date).ToString();
            string Day = perCal.GetDayOfMonth(date).ToString();
            if (Day.Length == 1)
            {
                Day = perCal.GetDayOfMonth(date).ToString().Insert(0, "0");
            }
            if (Month.Length == 1)
            {
                Month = perCal.GetMonth(date).ToString().Insert(0, "0");
            }
            string strDate = Year + '/' + Month + '/' + Day;
            return strDate;
        }

        /// <summary>
        /// تاریخ هجری به فرمت 
        /// YYYY/MM/DD
        /// را به تاریخ میلادی تبدیل می کند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime DateConverterHijriToMiladi(String date)
        {
            PersianCalendar calendar = new PersianCalendar();

            char[] chr = { '/' };

            int year = Utilities.CommonUtilities.ConvetToInt(date.Split(chr)[0]);
            int month = Utilities.CommonUtilities.ConvetToInt(date.Split(chr)[1]);
            int day = Utilities.CommonUtilities.ConvetToInt(date.Split(chr)[2].Split(' ')[0]);

            int hour;
            int minute;
            int sec;
            try
            {
                if (date.Contains(" "))
                {
                    string time = date.Split(' ')[1];
                    hour = CommonUtilities.ConvetToInt(time.Split(':')[0]);
                    minute = CommonUtilities.ConvetToInt(time.Split(':')[1]);
                    sec = CommonUtilities.ConvetToInt(time.Split(':')[2]);
                }
                else
                {
                    hour = minute = sec = 0;
                }
            }
            catch (Exception)
            {
                try
                {
                    string time = date.Split(' ')[1];
                    hour = CommonUtilities.ConvetToInt(time.Split(':')[0]);
                    minute = CommonUtilities.ConvetToInt(time.Split(':')[1]);
                    sec = 0;
                }
                catch (Exception)
                {
                    hour = minute = sec = 0;
                }
            }
            return calendar.ToDateTime(year, month, day, hour, minute, sec, 0);
        }

        public static Int32 ConvetToInt(String input)
        {
            String text = CorrectNumber(input);
            Int32 res;
            if (Int32.TryParse(text, out res))
            {
                return res;
            }
            else
            {
                throw new UserInterfaceException(99901, "فرمت متن ورودی صحیح نمی باشد.");
            }
        }

        /// <summary>
        /// اعداد فارسی رو به انگلیسی تبدیل می کند که در به نوع داده عددی قابل تبدیل باشند
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String CorrectNumber(String input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            for (int i = 0; i < PersianNumber.Length; i++)
            {
                input = input.Replace(PersianNumber[i], EnglishNumber[i]);
            }
            return input;
        }

        public static void Logout(HttpSessionState session)
        {
            session.Remove("Username");
            session.Remove("userObj");
            session.Remove("stockID");
            session.Remove("StockName");

        }

        public static String CorrectStringFarsi(String input)
        {
            var ret = input;
            ret = ret.Replace('ي', 'ی');
            ret = ret.Replace('ك', 'ک');
            return ret;
        }

        /// <summary>
        /// مقدار مربوط به ورودی را باز می گرداند
        /// در صورتی که ورودی خالی باشد هیچ مقدار باز می گرداند
        /// و گرنه مقدار آن را 
        /// Trim
        /// کرده و باز می گرداند
        ///  </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String GetString(String text)
        {
            if (text == null)
            {
                return null;
            }
            text = text.Trim();
            if (text.Equals(""))
            {
                return null;
            }
            return text;
        }
    }
}
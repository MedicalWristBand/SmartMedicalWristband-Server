using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.AccessControl;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Enums;
using PoliceServer.Utilities;

namespace PoliceServer.Logics
{
    public class UserRegistration
    {
        /// <summary>
        /// ایجاد کاربر جدید در سیستم
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="nationalCode"></param>
        /// <param name="password"></param>
        /// <param name="roles"></param>
        public static void RegisterUser(string fName, string lName, string username, string organizationCode, string password, List<RoleType> roles)
        {
            PoliceContext context = Utilities.ContextCreator.GetInstance().GetContext();
            try
            {
                if ( !Configuration.GetInstance().IsLocalMachin() && !CommonUtilities.CheckNationalNumber(username))
                {
                    throw new UserInterfaceException("کد ملی صحیح نمی باشد!");
                }
                if (roles.Count == 0)
                {
                    throw new UserInterfaceException("نقشی برای کاربر انتخاب نشده است!");
                }
                if (Repository.UserRepository.GetInstance().FindByNationalCode(username) != null)
                {
                    throw new UserInterfaceException(
                        "پیش از این کاربری با این شماره ملی ثبت شده است.");
                }
            }
            catch (EntityNotFoundException)
            {
                if (!Configuration.GetInstance().IsLocalMachin())
                {
                    if (context.Users.Any(u => u.Username.Equals(username)))
                    {
                        throw new UserInterfaceException("قبلا کاربری با این کد ملی ثبت شده است، لطفا کد ملی را اصلاح کرده و دوباره امتحان کنید.");
                    }
                }
                User savingUser = new User()
                    {
                        Name = fName,
                        Family = lName,
                        Username = username,
                        Password = password,
                        OrganizationCode = organizationCode,
                };
                foreach (var role in roles)
                {
                    savingUser.Responsibilities.Add(new Responsibility(role));
                }
                context.Users.Add(savingUser);
                context.SaveChanges();

                //                foreach (RoleType role in roles)
                //                {
                //                    LogEventHelper.InsertLogEvent(new LogEvent()
                //                    {
                //                        TableName = "User",
                //                        RecordID = savingUser.ID,
                //                        IsSuccuessful = true,
                //                        LogActionID = LogActionRepository.GetInstance().GetUserResponsibility_AddAction(),
                //                        Description = String.Format("دسترسی {0} در تاریخ {1} به {2} توسط {3} داده شده است.", Utilities.RoleManager.GetInstance().GetFarsiRole(role), CommonUtilities.DateConverterMiladiToHijri(DateTime.Now), savingUser.GetFullName(), Utilities.CommonUtilities.GetUser().GetFullName()),
                //                    }, EventPriority.High);
                //                }
                //                context.SaveChanges();
            }
        }
    }
}
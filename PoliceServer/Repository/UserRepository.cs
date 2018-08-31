using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using DotNetOpenAuth.Messaging;
using PoliceServer.Utilities;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;

namespace PoliceServer.Repository
{
    public class UserRepository : AbstractRepository<User>
    {
        private UserRepository(PoliceContext cnt = null) : base(cnt) { }

        public static UserRepository GetInstance(PoliceContext cnt = null)
        {
            return new UserRepository(cnt);
        }

        public bool CheckUsernamePassword(string username, string password)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByUsernameBegin, GetName(), username);

                User user = null;

                user = Context.Users.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));

                if (user == null)
                {
                    Log.WarnFormat(LogMessage.FetchByUsernameNotFound, GetName(), username);
                    return false;
                }
                Log.DebugFormat(LogMessage.FetchByUsernameFinished, GetName(), username);
                return true;
            }
            catch (UserInterfaceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format(LogMessage.FetchByUsernameError, GetName(), username), ex);
                throw new UserInterfaceException("هنگام دریافت اطلاعات کاربری خطای داخلی رخ داده است.", ex);
            }
        }

        
        public override User FindById(int id)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByIdBegin, GetName(), id);
                User user = Context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    Log.WarnFormat(LogMessage.FetchByIdNotFound, GetName(), id);
                    throw new EntityNotFoundException("کاربر مورد نظر در سیستم ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchByIdFinished, GetName(), id);
                return user;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByIdError, GetName(), id), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات کاربر از سیستم بر اساس شناسه");
                }
                throw;
            }
        }

        public override bool Save(User saving)
        {
            throw new NotImplementedException();
        }

        public void UpdateResponibilities(User saving)
        {
            User user = Context.Users.Include(u=>u.Responsibilities).FirstOrDefault(u => u.Username.Equals(saving.Username));
            if (user != null)
            {
                Context.Responsibilities.RemoveRange(user.Responsibilities.AsEnumerable());
                Context.SaveChanges();
                user.Responsibilities.AddRange(saving.Responsibilities);
                Context.Users.AddOrUpdate(user);
            }
            Context.SaveChanges();
        }


        /// <summary>
        /// یک کاربر را بر اساس شماره ملی برمیگرداند
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public User FindByNationalCode(String ssn)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByNationalCodeBegin, GetName(), ssn);

                User user = null;
                user = Context.Users.Include(user1 => user1.Responsibilities)
                            .FirstOrDefault(u => u.Username.Equals(ssn.Trim()));
               

                if (user == null)
                {
                    Log.WarnFormat(LogMessage.FetchByNationalCodeNotFound, GetName(), ssn);
                    throw new EntityNotFoundException(12002, "کاربر مورد نظر در سیستم ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchByNationalCodeFinished, GetName(), ssn);
                return user;
            }
            catch (UserInterfaceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format(LogMessage.FetchByNationalCodeError, GetName(), ssn), ex);
                throw new UserInterfaceException("هنگام دریافت اطلاعات کاربری خطای داخلی رخ داده است.", ex);
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.Utilities;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;

namespace PoliceServer.Repository
{
    public abstract class AbstractRepository<T> where T : class
    {

        internal readonly PoliceContext Context;

        internal readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        /// <error code ="10001"></error>
        /// <param name="cnt"></param>
        internal AbstractRepository(PoliceContext cnt = null)
        {
            try
            {
                Context = cnt ?? Utilities.ContextCreator.GetInstance().GetContext();
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format("Error Stoped Configuration in {0}'s constructor", GetName()), ex);
                    throw new UserInterfaceException(10001, ExceptionMessage.ConstructoreFaild);
                }
                throw;
            }
        }

        /// <summary>
        /// اسم کلاس فرزند را بر می گرداند
        /// </summary>
        /// <error code="10002"></error>
        /// <returns></returns>
        public String GetName()
        {
            return GetType().Name;
        }


        /// <summary>
        /// یک نوع موجودیت را بر اساس کلید آن باز می گرداند
        /// </summary>
        /// <param name="id"></param>
        /// <error code="10003"></error>
        /// <returns></returns>
        public abstract T FindById(Int32 id);

        /// <summary>
        /// یک نوع موجودیت را با توجه به تکرار نبودن کلید آن ذخیره میکند
        /// </summary>
        /// <param name="saving"></param>
        /// <error code="10003"></error>
        /// <returns></returns>
        public abstract bool Save(T saving);
    }
}
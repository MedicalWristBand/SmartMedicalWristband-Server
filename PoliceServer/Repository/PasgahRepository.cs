using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;
using PoliceServer.Utilities;

namespace PoliceServer.Repository
{
    public class PasgahRepository : AbstractRepository<Pasgah>
    {
        private PasgahRepository(PoliceContext cnt = null)
            : base(cnt)
        {

        }

        public static PasgahRepository GetInstance(PoliceContext cnt = null)
        {
            return new PasgahRepository(cnt);
        }

        public Pasgah FindByName(string name)
        {
            try
            {
                Log.Debug(String.Format(LogMessage.FetchByNameBegin, GetName(), name));
                var pasgah = Context.Pasgahs.FirstOrDefault(p => p.FarsiName.Equals(name.Trim()));
                if (pasgah == null)
                {
                    Log.Warn(String.Format(LogMessage.FetchByNameNotFound, GetName(), name));
                    throw new EntityNotFoundException(12020, "مرکزی با نام ذکر شده یافت نشد");
                }
                Log.Debug(String.Format(LogMessage.FetchByNameFinished, GetName(), name));
                return pasgah;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByNameError, GetName(), name), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات مزکز با استفاده نام مزکز");
                }
                throw;
            }
        }

        public Pasgah FindByPasgahId(String pasgahID)
        {
            try
            {
                Log.Debug(String.Format(LogMessage.FetchByNameBegin, GetName(), pasgahID));
                var pasgah = Context.Pasgahs.FirstOrDefault(p => p.PasgahId.Equals(pasgahID));
                if (pasgah == null)
                {
                    Log.Warn(String.Format(LogMessage.FetchByNameNotFound, GetName(), pasgahID));
                    throw new EntityNotFoundException(12003, "مرکزی با نام ذکر شده یافت نشد");
                }
                Log.Debug(String.Format(LogMessage.FetchByNameFinished, GetName(), pasgahID));
                return pasgah;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByNameError, GetName(), pasgahID), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات مزکز با استفاده نام مزکز");
                }
                throw;
            }
        }

        public override Pasgah FindById(int id)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByIdBegin, GetName(), id);
                Pasgah pasgah = Context.Pasgahs.FirstOrDefault(u => u.Id == id);
                if (pasgah == null)
                {
                    Log.WarnFormat(LogMessage.FetchByIdNotFound, GetName(), id);
                    throw new EntityNotFoundException("مرکز مورد نظر در سیستم ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchByIdFinished, GetName(), id);
                return pasgah;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByIdError, GetName(), id), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات مزکز از سیستم بر اساس شناسه");
                }
                throw;
            }
        }

        public List<Pasgah> GetAllPasgahs()
        {
            try
            {
                Log.Debug(String.Format(LogMessage.FetchAllBegin,GetName()));
                var list = Context.Pasgahs.ToList();
                if (list == null || list.Count < 1)
                {
                    Log.Debug(String.Format(LogMessage.FetchAllNotFound, GetName()));
                    return null;
                }
                Log.Debug(String.Format(LogMessage.FetchAllFinished, GetName()));
                return list;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format(LogMessage.FetchAllError, GetName()), ex);
                throw new UserInterfaceException("هنگام دریافت اطلاعات مراکز استعلام خطای داخلی رخ داده است.", ex);
            }
        }

        public override bool Save(Pasgah pasgah)
        {
            try
            {
                Log.Debug(String.Format(LogMessage.SaveObjectBegin, GetName(), pasgah.PasgahId));
                Context.Pasgahs.Add(pasgah);
                Context.SaveChanges();
                Log.Debug(String.Format(LogMessage.SaveObjectFinished, GetName(), pasgah.PasgahId));
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format(LogMessage.SaveObjectError, GetName(), pasgah.PasgahId), ex);
                throw;
            }
            
        }
    }
}
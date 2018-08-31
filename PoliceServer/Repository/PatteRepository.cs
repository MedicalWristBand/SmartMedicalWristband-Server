using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;
using PoliceServer.Utilities;

namespace PoliceServer.Repository
{
    public class PatteRepository : AbstractRepository<Patte>
    {
        private PatteRepository(PoliceContext cnt = null) : base(cnt) { }

        public static PatteRepository GetInstance(PoliceContext cnt = null)
        {
            return new PatteRepository(cnt);
        }

        public override Patte FindById(int id)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByIdBegin, GetName(), id);
                Patte patte = Context.Pattes.FirstOrDefault(p => p.Id == id);
                if (patte == null)
                {
                    Log.WarnFormat(LogMessage.FetchByIdNotFound, GetName(), id);
                    throw new EntityNotFoundException(1004,"پته مورد نظر در سیستم ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchByIdFinished, GetName(), id);
                return patte;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByIdError, GetName(), id), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات پته از سیستم بر اساس شناسه");
                }
                throw;
            }
        }

        public Patte FindByPatteSerial(string patteSerial)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchBySerialBegin, GetName(), patteSerial);
                Patte patte = Context.Pattes.Include(p=>p.Driver).FirstOrDefault(p => p.PatteSerial == patteSerial);
                if (patte == null)
                {
                    Log.WarnFormat(LogMessage.FetchBySerialNotFound, GetName(), patteSerial);
                    throw new EntityNotFoundException(1004, "پته مورد نظر در سیستم ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchBySerialFinished, GetName(), patteSerial);
                return patte;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchBySerialError, GetName(), patteSerial), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات پته از سیستم بر اساس سریال پته");
                }
                throw;
            }
        }


        public override bool Save(Patte saving)
        {
            try
            {
                Log.Debug(String.Format(LogMessage.SaveObjectBegin, GetName(), saving.PatteSerial));
                var patte = FindByPatteSerial(saving.PatteSerial);
                if(patte!=null)
                    Log.Debug(String.Format(LogMessage.SaveObjectRedundantStoped, GetName(), saving.PatteSerial));
                return false;
                
            }
            catch (EntityNotFoundException ex)
            {
                Context.Pattes.Add(saving);
                Context.SaveChanges();
                Log.Debug(String.Format(LogMessage.SaveObjectFinished, GetName(), saving.PatteSerial));
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format(LogMessage.SaveObjectError, GetName(), saving.PatteSerial), ex);
                throw;
            }
            
        }
    }
}
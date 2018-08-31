using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;
using PoliceServer.Utilities;

namespace PoliceServer.Repository
{
    public class ConfirmedPatteRepository : AbstractRepository<ConfirmedPatte>
    {
        private ConfirmedPatteRepository(PoliceContext cnt = null): base(cnt) {}

        public static ConfirmedPatteRepository GetInstance(PoliceContext cnt = null)
        {
            return new ConfirmedPatteRepository(cnt);
        }

        public override ConfirmedPatte FindById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Save(ConfirmedPatte saving)
        {
            try
            {
                saving.PatteSerial = PatteRepository.GetInstance().FindById(saving.PatteID).PatteSerial;
                Context.ConfirmedPattes.Add(saving);
                Context.SaveChanges();
                Log.Debug(String.Format(LogMessage.SaveObjectFinished, GetName(), saving.PatteSerial));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool CheckIfNotRedundant(int PasgahID, int PatteID)
        {
            try
            {
                Log.Debug(String.Format(LogMessage.SaveObjectBegin, GetName(), PatteID));
                var patte = FindByPasgahAndPatte(PasgahID, PatteID);
                if (patte != null)
                    Log.Debug(String.Format(LogMessage.SaveObjectRedundantStoped, GetName(), PatteID));
                return false;

            }
            catch (EntityNotFoundException ex)
            {
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format(LogMessage.SaveObjectError, GetName(), PatteID), ex);
                throw;
            }
        }

        public ConfirmedPatte FindByPasgahAndPatte(int pasgahId, int patteId)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByIdBegin, GetName(), "pasgahID:" + pasgahId + "-patteID:" + patteId);
                ConfirmedPatte patte = Context.ConfirmedPattes.FirstOrDefault(c => c.PatteID == patteId && c.PasgahID == pasgahId);
                if (patte == null)
                {
                    Log.WarnFormat(LogMessage.FetchByIdNotFound, GetName(), "pasgahID:" + pasgahId + "-patteID:" + patteId);
                    throw new EntityNotFoundException(1004, "تاییدیه پته مورد نظر در سیستم ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchBySerialFinished, GetName(), "pasgahID:" + pasgahId + "-patteID:" + patteId);
                return patte;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchBySerialError, GetName(), "pasgahID:" + pasgahId + "-patteID:" + patteId), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات تاییدیه پته از سیستم بر اساس شناسه پته و مرکز");
                }
                throw;
            }
        }

        public List<ConfirmedPatte> FindByPatteSerial(string serial)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchBySerialBegin, GetName(), serial);
                List<ConfirmedPatte> pattes = Context.ConfirmedPattes.Where(p => p.PatteSerial.Equals(serial)).ToList();
                if (pattes.Count == 0)
                {
                    Log.WarnFormat(LogMessage.FetchBySerialNotFound, GetName(), serial);
                    throw new EntityNotFoundException(1005, "هیج تاییدیه پته ای در سیستم ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchBySerialFinished, GetName(), serial);
                return pattes;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByIdError, GetName(), serial), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات تاییدیه پته از سیستم بر اساس شناسه");
                }
                throw;
            }
        }

        public List<ConfirmedPatte> FindByUserNationalCode(string userNationalCode)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByNationalCodeBegin, GetName(), userNationalCode);
                IQueryable<ConfirmedPatte> confirmPattes = Context.ConfirmedPattes.Include(y => y.Pasgah).Where(c => c.User.NationalCode.Equals(userNationalCode));
                if (!confirmPattes.Any())
                {
                    Log.WarnFormat(LogMessage.FetchByNationalCodeNotFound, GetName(), userNationalCode);
                    throw new EntityNotFoundException(1006, "هیج تاییدیه پته ای در سیستم با تاییدکننده‌ی شماره ملی مورد نظر ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchByNationalCodeFinished, GetName(), userNationalCode);
                return confirmPattes.ToList();
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByNationalCodeError, GetName(), userNationalCode), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات تاییدیه پته با شماره ملی کاربر مورد نظر ");
                }
                throw;
            }
        }

        public List<ConfirmedPatte> FindByPasgahId(int pasgahId)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByIdBegin, GetName(), pasgahId);
                DateTime now = DateTime.Now.AddDays(-1);
                
                List<ConfirmedPatte> confirmPattes = Context.ConfirmedPattes.Include(y => y.User).Where(c => c.PasgahID == pasgahId && DateTime.Compare(now, c.recordDate)<0).OrderByDescending(c=> c.recordDate).ToList();
                if (confirmPattes.Count<1)
                {
                    Log.WarnFormat(LogMessage.FetchByIdNotFound, GetName(), pasgahId);
                    throw new EntityNotFoundException(1007, "هیج تاییدیه پته ای در ۲۴ ساعت گذشته در مرکز مورد نظر ثبت نگردیده است");
                }
                Log.DebugFormat(LogMessage.FetchByIdFinished, GetName(), pasgahId);
                return confirmPattes;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByIdError, GetName(), pasgahId), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات تاییدیه پته با شماره مرکز مورد نظر ");
                }
                throw;
            }
        } 
    }
}
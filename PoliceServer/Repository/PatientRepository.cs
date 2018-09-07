using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using PoliceServer.AccessControl;
using PoliceServer.Exceptions;
using PoliceServer.Messages;
using PoliceServer.Models;
using PoliceServer.Utilities;

namespace PoliceServer.Repository
{
    public class PatientRepository: AbstractRepository<Patient> { 
        private PatientRepository(PoliceContext cnt = null) : base(cnt) { }

        public static PatientRepository GetInstance(PoliceContext cnt = null)
        {
            return new PatientRepository(cnt);
        }

        public override Patient FindById(int id)
        {
            try
            {
                Log.DebugFormat(LogMessage.FetchByIdBegin, GetName(), id);
                Patient patient = Context.Patients.FirstOrDefault(p => p.Id == id);
                if (patient == null)
                {
                    Log.WarnFormat(LogMessage.FetchByIdNotFound, GetName(), id);
                    throw new EntityNotFoundException("بیمار مورد نظر در سیستم ثبت نشده است");
                }
                Log.DebugFormat(LogMessage.FetchByIdFinished, GetName(), id);
                return patient;
            }
            catch (Exception ex)
            {
                if (!CommonUtilities.IsUserInterfaceException(ex))
                {
                    Log.Error(String.Format(LogMessage.FetchByIdError, GetName(), id), ex);
                    throw new UserInterfaceException("خطا در دریافت اطلاعات بیمار از سیستم بر اساس شناسه");
                }
                throw;
            }
        }

        public void Add(Patient patient)
        {
            throw new NotImplementedException();
        }

        public override bool Save(Patient patient)
        {
            try
            {
                if (Context.Patients.Any(p => p.PatientCode == patient.PatientCode))
                {
                    throw new DuplicateNameException();
                }
                Context.Patients.Add(patient);
                Context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Error in saving patient", e);
                throw;
            }
        }

        public Patient FindPatientByPatientCode(string patientcode)
        {
            Patient patient = Context.Patients.FirstOrDefault(p => p.PatientCode == patientcode);
            if (patient == null)
            {
                throw  new EntityNotFoundException();
            }

            return patient;
        }

    }
}
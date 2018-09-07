using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Repository;
using PoliceServer.Shared;

namespace PoliceServer.Controllers.RestService
{
    public class PatientInputController : ApiController
    {

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // Post api/<controller>/
        [HttpGet]
        public string Get([FromUri] PatientInputResourceQuery query)
        {
            string authUser = query.authUser, authPass = query.authPass, data = query.data;
            if (authPass == null || authUser == null || data == null)
            {
                return ErrorGenerator.GetError("insufficient params");
            }
            try
            {
                User u = UserRepository.GetInstance().FindByNationalCode(authUser);

                if (u.Username != authPass)
                {
                    return ErrorGenerator.GetError("invalid auth");
                }
                JPatient patient = JsonConvert.DeserializeObject<JPatient>(data);

                if (patient.name == null || patient.lastname == null)
                {
                    return ErrorGenerator.GetError("name and lastname cannot be null");
                }

                if (patient.patientId == null)
                {
                    return ErrorGenerator.GetError("patientId is empty");
                }

                patient.jsonData = data;
                try
                {
                    PatientRepository.GetInstance().Save(patient.ConvertToPatient());
                    return "{'message':'patient data recorded successfuly!'}";
                }
                catch (DuplicateNameException)
                {
                    return ErrorGenerator.GetError("duplicate patient ID");
                }
                catch (Exception e)
                {
                    return ErrorGenerator.GetError(e.Message);
                }

            }
            catch (EntityNotFoundException e)
            {
                return ErrorGenerator.GetError("User or password is wrong");
            }
        }
    }
    public class PatientInputResourceQuery
    {
        public string authUser { get; set; }
        public string authPass { get; set; }
        public string data { get; set; }
    }

    public static class ErrorGenerator
    {
        public static string GetError(string detail)
        {
            return "{'error':'" + detail + "'}";
        }
    }
}

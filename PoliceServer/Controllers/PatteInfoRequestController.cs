using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using Newtonsoft.Json;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Repository;
using PoliceServer.Shared;

namespace PoliceServer.Controllers
{
    public class PatteInfoRequestController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        // GET api/patteinforequest
        public Patte Get()
        {
            return new Patte();
        }

        // GET api/patteinforequest/patteNo/username/password/type
        public JsonResultWithObject<JPatte> Get(string patteNo, string username, string password, string type)
        {
            Log.Debug("Service Getting Patte started!");
            JsonResultWithObject<JPatte> Jresponse = new JsonResultWithObject<JPatte>();
            try
            {
                if (!UserRepository.GetInstance().CheckUsernamePassword(username.Trim(), password.Trim()))
                    throw new EntityNotFoundException(74401, "کاربر مورد نظر در سیستم ثبت نگردیده است");
                var response = Logics.BitaServices.GetPatte(patteNo, type.Equals("plq"));
                Jresponse.isSuccess = response.isSuccess;
                Jresponse.messages = response.messages;
                if (response.isSuccess)
                {
                    Jresponse.result = JPatte.ConvertToJPatte(response.result);
                    return Jresponse;
                }
                return Jresponse;
            }
            catch (UserInterfaceException ex)
            {
                Log.Error(ex.Message);
                Jresponse.messages = new[] { "نام کاربری یا رمز عبور اشتباه است" };
            }
            Jresponse.isSuccess = false;
            return Jresponse;

            
        }

        // POST api/patteinforequest
        public void Post([FromBody]string value)
        {
        }

        // PUT api/patteinforequest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/patteinforequest/5
        public void Delete(int id)
        {
        }
    }
}

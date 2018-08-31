using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PoliceServer.Repository;
using PoliceServer.Shared;

namespace PoliceServer.Controllers
{
    public class AuthenticationController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET api/authentication
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/authentication/username/password
        public JsonResultWithObject<bool> Get(string username, string password)
        {
            JsonResultWithObject<bool> result = new JsonResultWithObject<bool>();
            try
            {
                result.result = UserRepository.GetInstance().CheckUsernamePassword(username.Trim(), password.Trim());
                result.isSuccess = true;
                result.messages = null;
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("1201 خطای داخلی رخ داده است.");
                Log.Error(ex.Message);
                result.messages = new object[] { "در دریافت اطلاعاتی خطا رخ داده است." };
            }
            result.isSuccess = false;
            result.result = false;
            return result;
        }

        // POST api/authentication
        public void Post([FromBody]string value)
        {
        }

        // PUT api/authentication/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/authentication/5
        public void Delete(int id)
        {
        }
    }
}

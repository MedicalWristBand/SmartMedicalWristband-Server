using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using PoliceServer.AccessControl;
using PoliceServer.Exceptions;
using PoliceServer.Models;
using PoliceServer.Repository;

namespace PoliceServer.Controllers.RestService
{
    public class TicketGrantingController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET api/<controller>/administrator/administrator
        public string Get([FromUri] ResourceQuery query)
        {
            string nationalCode = query.NationalCode, hashPassword = query.HashedPassword;
            try
            {
                Models.User u = UserRepository.GetInstance().FindByNationalCode(nationalCode);
                using (System.Security.Cryptography.MD5 md5 =
                    System.Security.Cryptography.MD5.Create())
                {
                    byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes(u.Password));
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }

                    string serial = TicketRepository.GetInstance().Save(u);
                    return sb.ToString() == hashPassword
                        ? "{'ticket':'" + serial + "'}"
                        : "{'error':'User or password is wrong'}";
                }
            }
            catch (EntityNotFoundException e)
            {
                Log.Info("user not found",e);
                return "User or password is wrong";
            }
        }
    }
    public class ResourceQuery
    {
        public string NationalCode { get; set; }
        public string HashedPassword { get; set; }
    }
}
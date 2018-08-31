using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using PoliceServer.Models;
using PoliceServer.Repository;
using PoliceServer.Shared;
using PoliceServer.Utilities;

namespace PoliceServer.Controllers.RestService
{

    public class PatteReportController : ApiController
    {
        internal readonly log4net.ILog Log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET api/<controller>/5
        public string Get(string id)
        {
            return "require pasgah";
        }

        public JsonResultWithObject<Patte> Get(string hexPelaq, string hexPasgah)
        {
            string plaq = CommonUtilities.HexStringToString(hexPelaq, Encoding.UTF8);
            string pasgahName = CommonUtilities.HexStringToString(hexPasgah, Encoding.UTF8);
            Pasgah pasgah = PasgahRepository.GetInstance().FindByName(pasgahName);
            JsonResultWithObject<Patte> response = Logics.BitaServices.GetPatte(plaq, true);
            if (!response.isSuccess)
            {
                return response;
            }
            try
            {
                bool isSucceed =
                    ConfirmedPatteRepository.GetInstance()
                        .CheckIfNotRedundant(pasgah.Id, response.result.Id);
                if (isSucceed)
                {
                    isSucceed = ConfirmedPatteRepository.GetInstance().Save(new ConfirmedPatte() { PasgahID = pasgah.Id, PatteID = response.result.Id, UserID = pasgah.Users.FirstOrDefault().Id, ConfirmationIp = "WEB SERVICE" });
                    if (isSucceed)
                    {
                        response.messages = new []{"پته با موفقیت تایید شد."};
                    }
                    else
                    {
                        response.messages = new[] { "در ذخیره سازی تاییدیه پته خطا رخ داده است."};
                        response.isSuccess = false;
                    }
                }
                else
                {
                    response.messages = new[] { "این پته قبلا در این مرکز ثبت شده است و امکان تایید مجدد آن وجود ندارد."};
                    response.isSuccess = false;

                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Uknown Error occured during saving patteConfirm with plaq {0}", plaq));
                Log.Error(ex.Message);

                response.messages = new[] { "این پته قبلا در این مرکز ثبت شده است و امکان تایید مجدد آن وجود ندارد" };
                response.isSuccess = false;

            }

            return response;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
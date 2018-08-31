using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Owin.Security;
using PoliceServer.Exceptions;
using PoliceServer.GetUrbanWarehousePermit;
using PoliceServer.Models;
using PoliceServer.ParvaneBita;
using PoliceServer.GetCustomsPermit;
using PoliceServer.StockInformation;
using PoliceServer.Repository;
using PoliceServer.Shared;
using PoliceServer.CustomsValueDeclarationInformation;
using PoliceServer.BillOfLadingInformation;
using Configuration = PoliceServer.Utilities.Configuration;

namespace PoliceServer.Logics
{
    public class BitaServices
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ClientBase<T> InitialClient<T>(System.ServiceModel.ClientBase<T> client) where T : class
        {
            string pfxKeyPath = "";
            string cerKeyPath = "";
            try
            {
                string root = System.Web.HttpContext.Current.Server.MapPath("~");
                pfxKeyPath = root + "/bin/cert.pfx";
                cerKeyPath = root + "/bin/bitaServicebusCert.cer";
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = new X509Certificate2(cerKeyPath);
                Log.Debug("bitaServicebusCert.cer load done");

                if (Configuration.GetInstance().IsLocalMachin())
                {
                    client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(pfxKeyPath,
                        "DpQXmjdDBJif2K76qaEkS", X509KeyStorageFlags.PersistKeySet);
                }
                else
                {
                    client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(pfxKeyPath,
                        "DpQXmjdDBJif2K76qaEkS", X509KeyStorageFlags.MachineKeySet);
                }
                Log.Debug("cert.pfx load done");
                client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
                    X509CertificateValidationMode.None;
                Log.Debug("certificate mode set successfully!");

                return client;

            }
            catch (CryptographicException ex)
            {

                Log.Error("Public or Private Key Not Found; Path:" + pfxKeyPath + " OR " + cerKeyPath);
                Log.Error(ex.StackTrace);
                Log.Debug(ex.Message);
                if (ex.InnerException != null)
                {
                    Log.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        Log.Error(ex.InnerException.InnerException.Message);
                }
                throw new UserInterfaceException(1002, "خطای داخلی رخ داده است.");
            }

        }

        /// <summary>
        /// ErrorCode = 10001
        /// </summary>
        /// <param name="patteNoOrPlaque"></param>
        /// <param name="byPlaque"></param>
        /// <returns></returns>
        public static JsonResultWithObject<Patte> GetPatte(string patteNoOrPlaque, bool byPlaque)
        {
            JsonResultWithObject<Patte> response = new JsonResultWithObject<Patte>();
            try
            {
                try
                {
                    if (!byPlaque)
                    {
                        Patte existingPatte =
                            PatteRepository.GetInstance().FindByPatteSerial(patteNoOrPlaque);
                        response.isSuccess = true;
                        response.messages = null;
                        response.result = existingPatte;
                        return response;
                    }
                }
                catch (Exception)
                {
                    //do nothing because it means that this patte not found in dataBase.
                }

                GetCustomsPermitClient client = (GetCustomsPermitClient) InitialClient(new GetCustomsPermitClient());
                customsPermit resp;
                if (byPlaque)
                    resp = client.getCustomsPermitByPlaque(patteNoOrPlaque);
                else
                    resp = client.getCustomsPermitByCustomsPermitNumber(patteNoOrPlaque.Trim());


                Patte patte = Patte.ConvertToPatte(resp);
                //GetBillOfLading(patte.Driver.NationalCode);
                if(!Configuration.GetInstance().IsLocalMachin())
                    PatteRepository.GetInstance().Save(patte);

                response.isSuccess = true;
                response.messages = null;
                response.result = patte;

                return response;
            }
            catch (EndpointNotFoundException epe)
            {
                Log.Error("Service can not connect Bita.");
                response.messages = new object[] { "1003 خطا در ارتباط با سامانه های همکار." };
            }
            catch (UserInterfaceException ex)
            {
                Log.Error(ex.Message);
                response.messages = new[] { ex.Message };
            }
            catch (Exception ex)
            {
                Log.Error("خطای داخلی در دریافت پته 10001");
                Log.Error(ex.Message);
                response.messages = new object[] { "اطلاعات پته درخواستی نامعتبر و یا هنوز در گمرک دریافت نشده است." };
            }
            response.isSuccess = false;
            return response;

        }

        /// <summary>
        /// ErrorCode = 10003
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static JsonResultWithObject<List<Patte>> GetAllPateBySerial(string serial)
        {
            JsonResultWithObject<List<Patte>> response = new JsonResultWithObject<List<Patte>>();
            try
            {
                GetCustomsPermitClient client = (GetCustomsPermitClient) InitialClient(new GetCustomsPermitClient());
                customsPermit[] permitList = client.getCustomsPermitsBySerialNumber(serial);
                if (permitList.Length == 0)
                    throw new UserInterfaceException("پته‌ای دریافت نشد.");

                List<Patte> pateList = new List<Patte>();
                foreach (customsPermit permit in permitList)
                {
                    pateList.Add(Patte.ConvertToPatte(permit));
                }

                response.isSuccess = true;
                response.messages = null;
                response.result = pateList;

                return response;
            }
            catch (EndpointNotFoundException epe)
            {
                Log.Error("Service can not connect Bita.");
                response.messages = new object[] {"1003 خطا در ارتباط با سامانه های همکار."};
            }
            catch (UserInterfaceException ex)
            {
                Log.Error(ex.Message);
                response.messages = new[] {ex.Message};
            }
            catch (Exception ex)
            {
                Log.Error("10003 خطای داخلی در دریافت همه پته های یک پروانه رخ داده است.");
                Log.Error(ex.Message);
                response.messages = new object[] {"اطلاعات پته درخواستی نامعتبر و یا هنوز در گمرک دریافت نشده است."};
            }
            response.isSuccess = false;
            return response;

        }

        /// <summary>
        /// ErrorCode = 10002
        /// </summary>
        /// <param name="parvaneSerial"></param>
        /// <returns></returns>
        public static customsDeclaration GetParvane(string parvaneSerial)
        {
            string pfxKeyPath = "";
            string cerKeyPath = "";
            try
            {

                ParvaneBita.CustomsDeclarationInformationClient client = new CustomsDeclarationInformationClient();
                string root = System.Web.HttpContext.Current.Server.MapPath("~");
                pfxKeyPath = root + "/bin/cert.pfx";
                cerKeyPath = root + "/bin/bitaServicebusCert.cer";
                client.ClientCredentials.ServiceCertificate.DefaultCertificate = new X509Certificate2(cerKeyPath);
                Log.Debug("ShowParvane bita.cer load done");

                bool isLocalMachine = ConfigurationManager.AppSettings["localMachine"].ToString().Equals("true");
                if (isLocalMachine)
                {
                    client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(pfxKeyPath,
                        "DpQXmjdDBJif2K76qaEkS", X509KeyStorageFlags.PersistKeySet);
                }
                else
                {

                    client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(pfxKeyPath,
                        "DpQXmjdDBJif2K76qaEkS", X509KeyStorageFlags.MachineKeySet);
                }
                Log.Debug("ShowParvane cert.pfx load done");
                client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
                    X509CertificateValidationMode.None;
                Log.Debug("ShowParvane Certificate-Mode set successfully!");


                customsDeclaration result = client.getCustomsDeclarationBySerialNumber(parvaneSerial);
                return result;
            }
            catch (CryptographicException exx)
            {
                Log.Error("Public or Private Key Not Found; Path:" + pfxKeyPath + " OR " + cerKeyPath);
                throw;
            }
            catch (FaultException fex)
            {
                Log.Debug("showParvane FaultException (Invalid Serial) !");
                throw;
            }
            catch (Exception ex)
            {
                Log.Debug("ShowParvane EXCEPTION");
                Log.Debug("در هنگام دریافت اطلاعات پروانه خطای نامشخص رخ داده است. 10002");
                Log.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// ErrorCode = 10004
        /// </summary>
        /// <param name="pateNo"></param>
        /// <returns></returns>
        public static JsonResultWithObject<Patte> GetUrbanWarehousePate(string pateNo)
        {
            string pfxKeyPath = "";
            string cerKeyPath = "";
            JsonResultWithObject<Patte> response = new JsonResultWithObject<Patte>();
            try
            {
                try
                {
                        Patte existingPatte =PatteRepository.GetInstance().FindByPatteSerial(pateNo);
                        response.isSuccess = true;
                        response.messages = null;
                        response.result = existingPatte;
                        return response;
                }
                catch (Exception)
                {
                    //do nothing because it means that this patte not found in dataBase.
                }
                
                GetUrbanWarehousePermitClient client = (GetUrbanWarehousePermitClient) InitialClient(new GetUrbanWarehousePermitClient());
                uwPate uwPate = client.getUrbanWarehousePermitByPermitNumber(pateNo);
                Patte pate =  Patte.ConvertFromUwPate(uwPate);
                PatteRepository.GetInstance().Save(pate);
                response.isSuccess = true;
                response.result = pate;
                return response;
            }
            catch (EndpointNotFoundException epe)
            {
                Log.Error("Service can not connect Bita.");
                response.messages = new object[] { "1003 خطا در ارتباط با سامانه های همکار." };
            }
            catch (CryptographicException exx)
            {

                Log.Error("Public or Private Key Not Found; Path:" + pfxKeyPath + " OR " + cerKeyPath);
                Log.Error(exx.StackTrace);
                Log.Debug(exx.Message);
                if (exx.InnerException != null)
                {
                    Log.Error(exx.InnerException.Message);
                    if (exx.InnerException.InnerException != null)
                        Log.Error(exx.InnerException.InnerException.Message);
                }

                response.messages = new object[] { "1002 خطای داخلی رخ داده است." };

            }
            catch (Exception ex)
            {
                Log.Error("در هنگام دریافت پته انبارشهری خطای نامشخص رخ داده است. 10004");
                Log.Error(ex.Message);
                response.messages = new object[] { "اطلاعات پته درخواستی نامعتبر و یا هنوز در گمرک دریافت نشده است." };
            }
            response.isSuccess = false;
            return response;

        }

        /// <summary>
        /// ErrorCode = 10005
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public static JsonResultWithObject<customsValueDeclaration[]> CustomsValueDeclarationInformation(string serial)
        {
            string pfxKeyPath = "";
            string cerKeyPath = "";
            JsonResultWithObject<customsValueDeclaration[]> result = new JsonResultWithObject<customsValueDeclaration[]>();
            try
            {
                CustomsValueDeclarationInformationClient client =  (CustomsValueDeclarationInformationClient) InitialClient(new CustomsValueDeclarationInformationClient());
                //99000-21318340
                customsValueDeclaration[] customsValueDeclarations = client.getCustomsValueDeclarationBySerialNumber(serial);
                result.isSuccess = true;
                result.result = customsValueDeclarations;
                return result;
            }
            catch (CryptographicException ex)
            {
                Log.Error("Public or Private Key Not Found; Path:" + pfxKeyPath + " OR " + cerKeyPath);
                Log.Error(ex.StackTrace);
                Log.Debug(ex.Message);
                if (ex.InnerException != null)
                {
                    Log.Error(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                        Log.Error(ex.InnerException.InnerException.Message);
                }
                result.messages = new object[] { "1002 خطای داخلی رخ داده است." };
            }
            catch (Exception ex)
            {
                Log.Error("هنگام دریافت اظهارنامه ارزش خطای نامشخص رخ داده است. 10005");
                Log.Error(ex.Message);
                result.messages = new object[] { "اطلاعات اظهارنامه ارزش درخواستی نامعتبر و یا برقراری ارتباط با مشکل مواجه شده است." };
            }
            result.isSuccess = false;
            return result;
            
        }

        /// <summary>
        ///  ErrorCode = 10060 , 10061
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        public static JsonResultWithObject<BillOfLading> GetBillOfLading(string nationalId)
        {
            JsonResultWithObject<BillOfLading> result = new JsonResultWithObject<BillOfLading>();
            BillOfLadingInformationClient client = (BillOfLadingInformationClient) InitialClient(new BillOfLadingInformationClient());
            try
            {
                BillOfLading bol = client.getBOLByDriverNationalId(nationalId);
                result.result = bol;
                result.isSuccess = true;
                return result;
            }
            catch (EndpointNotFoundException ex)
            {
                Log.Error("10060 Service can not connect Bita.");
                Log.Error(ex);
                result.messages = new object[] { "10060 در هنگام دریافت بارنامه از سامانه همکار خطا رخ داده است." };
            }
            catch (Exception ex)
            {
                Log.Error("10061 BillOfLading Exception.");
                Log.Error(ex);
                result.messages = new object[] { "10061 خطا نامشخص در فرآیند دریافت بارنامه از مرکز." };
            }
            result.isSuccess = false;
            return result;
        }

        /// <summary>
        /// ErrorCode = 10060 , 10061
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public static JsonResultWithObject<stock[]> GetStockInformation(string stockId)
        {
            JsonResultWithObject<stock[]> result =  new JsonResultWithObject<stock[]>();
            StockInformationClient client = (StockInformationClient) InitialClient(new StockInformationClient());
            try
            {
                stock[] stockInformation = client.getStockInformationByStockId(new string[]{stockId});
                result.result = stockInformation;
                result.isSuccess = true;
                return result;
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                // انبار مورد نظر یافت نشد
                result.messages = new[] {ex.Message};
            }
            result.isSuccess = false;
            return result;
        }
    }
}
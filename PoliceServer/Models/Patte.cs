using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using PoliceServer.Enums;
using PoliceServer.GetCustomsPermit;
using PoliceServer.GetUrbanWarehousePermit;
using PoliceServer.Logics;
using PoliceServer.Shared;
using PoliceServer.StockInformation;

namespace PoliceServer.Models
{
    [Table("Patte")]
    public class Patte : AbstractEntity
    {
        public string PatteSerial { get; set; }
        public string CustomsGateCode { get; set; }
        public string CityOfOrigin { get; set; }
        public string DestinationCode { get; set; }
        public string DestinationName { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationBossName { get; set; }
        public string DestinationBossPhoneNo { get; set; }
        public double Weight { get; set; }
        public string PlaqueLeftTwoDigits { get; set; }
        public string PlaqueMiddleCharacter { get; set; }
        public string PalqueMiddleThreeDigits { get; set; }
        public string PlaqueCityCode { get; set; }
        public string KotajNos { get; set; }
        public bool IsToUrbanWarehouse { get; set; }
        public string ConsigneeNationalId { get; set; }
        public string OperatorNationalId { get; set; }
        public string ForeignPlaque { get; set; }


        [Required]
        public int DriverID { get; set; }
        [ForeignKey("DriverID")]
        public virtual Driver Driver { get; set; }



        [Column("IssuanceDate", TypeName = "smalldatetime")]
        public DateTime issuanceDate { get; private set; }

        [NotMapped]
        public String IssuanceDate
        {
            get { return Utilities.CommonUtilities.DateConverterMiladiToHijri(this.issuanceDate) + " " + this.issuanceDate.Hour + ":" + this.issuanceDate.Minute; }
            private set { this.issuanceDate = Utilities.CommonUtilities.DateConverterHijriToMiladi(value); }
        }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<Container> Containers { get; set; }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<ConfirmedPatte> ConfirmedPattes { get; set; }



        public Patte()
        {
            Containers = new HashSet<Container>();
            DestinationCode = "";
            DestinationCity = "";
            DestinationState = "";
            DestinationName = "نامشخص";
        }

        public Patte(string gatecode)
        {
            CustomsGateCode = gatecode;
        }

        public static Patte ConvertToPatte(customsPermit customsPermit)
        {
            Patte pate = new Patte();

            pate.PatteSerial = customsPermit.iranUniqueConsignmentReference.Replace("-", "");
            if (customsPermit.iranUniqueConsignmentReference.Split('-').Length > 1)
            {
                pate.CustomsGateCode = customsPermit.iranUniqueConsignmentReference.Split('-')[0];
                try
                {
                    pate.CityOfOrigin = EnumHelper.ToEnumString((CustomsCode)Int32.Parse(pate.CustomsGateCode));
                }
                catch (Exception)
                {
                    pate.CityOfOrigin = pate.CustomsGateCode;
                }
            }
            pate.ForeignPlaque = customsPermit.vehicleInformation.foreignPlaque;
            pate.IsToUrbanWarehouse = (customsPermit.destinationStocksIDs != null) && customsPermit.destinationStocksIDs.Length > 0;
            pate.PlaqueLeftTwoDigits = customsPermit.vehicleInformation.plaqueLeftTwoDigits;
            pate.PlaqueMiddleCharacter = customsPermit.vehicleInformation.plaqueMiddleCharacter;
            pate.PalqueMiddleThreeDigits = customsPermit.vehicleInformation.palqueMiddleThreeDigits;
            pate.PlaqueCityCode = customsPermit.vehicleInformation.plaqueSerial;
            pate.Weight = customsPermit.weightInKg;
            pate.Containers = Models.Container.ConvertToContainers(customsPermit.container);
            pate.issuanceDate = customsPermit.issuanceDate;
            pate.ConsigneeNationalId = customsPermit.consigneeNationalID;

            if (customsPermit.destinationStocksIDs != null && customsPermit.destinationStocksIDs.Length > 0)
            {
                pate.DestinationCode = customsPermit.destinationStocksIDs[0];
                if (customsPermit.destinationStocksIDs[0].Length == 5)
                    try
                    {
                        pate.DestinationCity = EnumHelper.ToEnumString((CustomsCode)Int32.Parse(customsPermit.destinationStocksIDs[0]));

                    }
                    catch (Exception)
                    {
                        pate.CityOfOrigin = customsPermit.destinationStocksIDs[0];
                    }
                if (Regex.IsMatch(customsPermit.destinationStocksIDs[0], @"\p{IsArabic}"))
                {
                    pate.DestinationCity = customsPermit.destinationStocksIDs[0];
                    pate.IsToUrbanWarehouse = false;
                }
                else if (customsPermit.destinationStocksIDs[0].Equals("unknown"))
                {
                    pate.DestinationCity = "نامشخص";
                    pate.DestinationName = "نامشخص";
                }
                else
                {
                    JsonResultWithObject<stock[]> stockInfos = BitaServices.GetStockInformation(customsPermit.destinationStocksIDs[0]);
                    try
                    {
                        if (stockInfos.isSuccess)
                        {
                            pate.DestinationName = stockInfos.result[0].name;
                            pate.DestinationCity = stockInfos.result[0].city;
                            pate.DestinationState = stockInfos.result[0].state;
                            pate.DestinationAddress = stockInfos.result[0].address;
                            pate.DestinationBossPhoneNo = stockInfos.result[0].manager != null
                                ? stockInfos.result[0].manager.phoneNo
                                : "";
                            pate.DestinationBossName = stockInfos.result[0].manager != null
                                ? stockInfos.result[0].manager.name
                                : "";

                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }


            foreach (string serial in customsPermit.orderCode)
            {
                //                if (!serial.Contains("99000"))
                //                    pate.KotajNos += pate.CustomsGateCode + "-" + serial + "*";
                //                else
                {
                    pate.KotajNos += serial + "*";
                }
            }
            pate.Driver = Driver.ConvertToDriver(customsPermit.driver);
            return pate;
        }

        public string getPlaque()
        {
            if (!ForeignPlaque.IsNullOrWhiteSpace())
                return "پلاک خارجی: " + ForeignPlaque;
            return this.PlaqueLeftTwoDigits + this.PlaqueMiddleCharacter + this.PalqueMiddleThreeDigits + "ایران" +
                   PlaqueCityCode;
        }

        public static Patte ConvertFromUwPate(uwPate uwPate)
        {
            Patte pate = new Patte();
            pate.PatteSerial = uwPate.urbanWarehousePermitID;
            pate.ConsigneeNationalId = uwPate.consigneeNationalID;
            pate.Driver = Models.Driver.ConvertToDriver(uwPate.driver);
            pate.issuanceDate = uwPate.exitFromOriginDate;
            pate.Containers = Container.ConvertToContainers(uwPate.exitBills);
            pate.OperatorNationalId = uwPate.operatorNationalID;
            pate.CityOfOrigin = uwPate.permitPlaceOfIssueID;
            JsonResultWithObject<stock[]> stockInfos = BitaServices.GetStockInformation(uwPate.permitPlaceOfIssueID);
            if (stockInfos.isSuccess)
            {
                pate.CityOfOrigin = stockInfos.result[0].name + " (" + stockInfos.result[0].state + "-" + stockInfos.result[0].city + ")";
            }

            pate.DestinationCity = uwPate.permitDestinationID.Equals("-")? "مقصد معرفی نشده است": uwPate.permitDestinationID;
            pate.PlaqueLeftTwoDigits = uwPate.vehicleInformation.plaqueLeftTwoDigits;
            pate.PlaqueMiddleCharacter = uwPate.vehicleInformation.plaqueMiddleCharacter;
            pate.PalqueMiddleThreeDigits = uwPate.vehicleInformation.palqueMiddleThreeDigits;
            pate.PlaqueCityCode = uwPate.vehicleInformation.plaqueSerial;

            return pate;
        }

        public string GetAllCommoditiesInString()
        {
            StringBuilder str = new StringBuilder();
            foreach (Container con in Containers)
            {
                foreach (Commodity com in con.Commoditys)
                {
                    str.Append(com.CommodityTariffDescription).Append("-");
                }
            }
            return str.ToString();
        }

        public string GetOstanAndShahr()
        {
            if (this.DestinationState.IsNullOrWhiteSpace() || this.DestinationCity.IsNullOrWhiteSpace())
                return "نامشخص";
            return this.DestinationState + "-" + this.DestinationCity;
        }
    }

}
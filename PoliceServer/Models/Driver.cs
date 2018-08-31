using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PoliceServer.Models
{
    [Table("Driver")]
    public class Driver : AbstractEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }
        public string CardNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string PhoneNo { get; set; }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<Patte> Pattes { get; set; } 

        public Driver()
        {
        }

        public static Driver ConvertToDriver(GetCustomsPermit.driver dr)
        {
            Driver driver = new Driver()
            {
                Name = dr.firstName,
                Family = dr.lastName,
                NationalCode = dr.nationalID,
                CardNumber = dr.driverCardNumber,
                LicenseNumber = dr.driverLicenseNumber,
                PhoneNo = dr.cellPhoneNumber
            };

            return driver;
        }

        public static Driver ConvertToDriver(GetUrbanWarehousePermit.driver dr)
        {
            Driver driver = new Driver()
            {
                Name = dr.firstName,
                Family = dr.lastName,
                NationalCode = dr.nationalID,
                CardNumber = dr.driverCardNumber,
                LicenseNumber = dr.driverLicenseNumber,
                PhoneNo = dr.cellPhoneNumber
            };

            return driver;
        }

        public string GetFullName()
        {
            if (Name.Trim().Equals(Family.Trim()))
                return Name;
            else
            {
                return Name + " " + Family;                       
            }
        }
    }
}
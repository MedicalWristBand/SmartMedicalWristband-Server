using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.Models;

namespace PoliceServer.Shared
{
    public class JDriver
    {
        public string Name;
        public string Family;
        public string NationalCode;
        public string CardNumber;
        public string LicenseNumber;
        public string PhoneNo;

        public static JDriver ConvertToJDriver(Driver dr)
        {
            JDriver jdriver = new JDriver()
            {
                Name = dr.Name,
                Family = dr.Family,
                NationalCode = dr.NationalCode,
                CardNumber = dr.CardNumber,
                LicenseNumber = dr.LicenseNumber,
                PhoneNo = dr.PhoneNo
            };

            return jdriver;
        }
    }
}
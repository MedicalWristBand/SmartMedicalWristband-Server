using System;
using System.Collections.Generic;
using System.Linq;
using PoliceServer.Models;

namespace PoliceServer.Shared
{
    public class JPatte
    {
            public string GateCode;
            public string PatteSerial;
            public string CityOfOrigin;
            public string DestinationCity;
            public JDriver Driver;
            public double Weight;
            public string PlaqueLeftTwoDigits;
            public string PlaqueMiddleCharacter;
            public string PalqueMiddleThreeDigits;
            public string PlaqueCityCode;

            public string[] KotajNo;
            public List<JContainer> Container;
            public string IssuanceDate;
            
            public static JPatte ConvertToJPatte(Patte input)
            {
                JPatte jPatte = new JPatte()
                {
                    GateCode = input.CustomsGateCode,
                    PatteSerial = input.PatteSerial,
                    CityOfOrigin = input.CityOfOrigin,
                    DestinationCity = input.DestinationCity,
                    Driver = JDriver.ConvertToJDriver(input.Driver),
                    PlaqueLeftTwoDigits = input.PlaqueLeftTwoDigits,
                    PlaqueMiddleCharacter = input.PlaqueMiddleCharacter,
                    PalqueMiddleThreeDigits = input.PalqueMiddleThreeDigits,
                    PlaqueCityCode = input.PlaqueCityCode,
                    Weight = input.Weight,
                    Container = JContainer.ConvertToJContainers(input.Containers.ToList()),
                    KotajNo = input.KotajNos.Split('*'),
                    IssuanceDate = input.IssuanceDate
                };
                return jPatte;
            }
        
    }
}
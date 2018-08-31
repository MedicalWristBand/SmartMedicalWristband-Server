using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PoliceServer.Models;

namespace PoliceServer.Shared
{
    public class JCommodity
    {
        public string CommodityDescription;

        public string CommodityHsCode;

        public double CommodityItemQuantity;

        public string CommodityTariffDescription;

        public double FreightValue;

        public double GrossWeight;

        public double InsuranceValue;

        public bool IsSelected;

        public double NetWeight;

        public string PackageType;

        public string WeightOrNumber;

        public double IRRValue;

        public static List<JCommodity> ConvertToJCommodities(List<Commodity> commodityInformations)
        {
            List<JCommodity> result = new List<JCommodity>();
            for (int i = 0; i < commodityInformations.Count; i++)
            {
                result.Add(new JCommodity()
                {
                    CommodityDescription = commodityInformations[i].CommodityDescription,
                    CommodityHsCode = commodityInformations[i].CommodityHsCode,
                    CommodityItemQuantity = commodityInformations[i].CommodityItemQuantity,
                    CommodityTariffDescription = commodityInformations[i].CommodityTariffDescription,
                    FreightValue = commodityInformations[i].FreightValue,
                    GrossWeight = commodityInformations[i].GrossWeight,
                    InsuranceValue = commodityInformations[i].InsuranceValue,
                    IsSelected = commodityInformations[i].IsSelected,
                    NetWeight = commodityInformations[i].NetWeight,
                    PackageType = commodityInformations[i].PackageType,
                    WeightOrNumber = commodityInformations[i].WeightOrNumber,
                    IRRValue = commodityInformations[i].IRRValue
                });
            }
            return result;
        }

    }
}
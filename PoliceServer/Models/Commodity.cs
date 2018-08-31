using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceServer.Models
{
    [Table("Commodity")]
    public class Commodity : AbstractEntity
    {
        public string CommodityDescription { get; set; }
        public string CommodityHsCode { get; set; }
        public double CommodityItemQuantity { get; set; }
        public string CommodityTariffDescription { get; set; }
        public double FreightValue { get; set; }
        public double GrossWeight { get; set; }
        public double InsuranceValue { get; set; }
        public bool IsSelected { get; set; }
        public double NetWeight { get; set; }
        public string PackageType { get; set; }
        public string WeightOrNumber { get; set; }
        public double IRRValue { get; set; }

        [Required]
        public int ContainerID { get; set; }
        [ForeignKey("ContainerID")]
        public virtual Container Container { get; set; }

        public static List<Commodity> ConvertToCommodities(GetCustomsPermit.commodityInformation[] commodityInformations)
        {
            List<Commodity> result = new List<Commodity>();
            for (int i = 0; i < commodityInformations.Length; i++)
            {
                result.Add(new Commodity()
                {
                    CommodityDescription = commodityInformations[i].commodityDescription,
                    CommodityHsCode = commodityInformations[i].commodityHSCode,
                    CommodityItemQuantity = commodityInformations[i].commodityItemQuantity,
                    CommodityTariffDescription = commodityInformations[i].commodityTariffDescription,
                    FreightValue = commodityInformations[i].freightValue,
                    GrossWeight = commodityInformations[i].grossWeight,
                    InsuranceValue = commodityInformations[i].insuranceValue,
                    IsSelected = commodityInformations[i].isSelected,
                    NetWeight = commodityInformations[i].netWeight,
                    PackageType = commodityInformations[i].packageType,
                    WeightOrNumber = commodityInformations[i].weightOrNumber,
                    IRRValue = commodityInformations[i].iRRValue
                });
            }
            return result;
        }

        public static List<Commodity> ConvertToCommodities(GetUrbanWarehousePermit.commodityInformation[] commodityInformations)
        {
            List<Commodity> result = new List<Commodity>();
            for (int i = 0; i < commodityInformations.Length; i++)
            {
                result.Add(new Commodity()
                {
                    CommodityDescription = commodityInformations[i].commodityDescription,
                    CommodityHsCode = commodityInformations[i].commodityHSCode,
                    CommodityItemQuantity = commodityInformations[i].commodityItemQuantity,
                    CommodityTariffDescription = commodityInformations[i].commodityTariffDescription,
                    FreightValue = commodityInformations[i].freightValue,
                    GrossWeight = commodityInformations[i].grossWeight,
                    InsuranceValue = commodityInformations[i].insuranceValue,
                    IsSelected = commodityInformations[i].isSelected,
                    NetWeight = commodityInformations[i].netWeight,
                    PackageType = commodityInformations[i].packageType,
                    WeightOrNumber = commodityInformations[i].weightOrNumber,
                    IRRValue = commodityInformations[i].iRRValue
                });
            }
            return result;
        }
        
    }
}
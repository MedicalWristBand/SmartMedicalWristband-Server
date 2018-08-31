using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PoliceServer.Models
{
    [Table("Container")]
    public class Container : AbstractEntity
    {
        
        public string ContainerNumber { get; set; }

        public string ContainerType { get; set; }

        [Required]
        public int PatteID { get; set; }
        [ForeignKey("PatteID")]
        public virtual Patte Patte { get; set; }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<Commodity> Commoditys { get; set; }


        public static List<Container> ConvertToContainers(GetCustomsPermit.container[] container)
        {
            List<Container> result = new List<Container>();
            for (int i = 0; i < container.Length; i++)
            {
                result.Add(new Container(){
                Commoditys = Commodity.ConvertToCommodities(container[i].commodityInformation),
                ContainerNumber = container[i].containerNumber,
                ContainerType = container[i].containerType
            });
            }
            return result;
        }

        public static List<Container> ConvertToContainers(GetUrbanWarehousePermit.exitBill[] exitBills)
        {
            List<Container> result = new List<Container>();
            for (int i = 0; i < exitBills.Length; i++)
            {
                result.Add(new Container()
                {
                    Commoditys = Commodity.ConvertToCommodities(exitBills[i].commodities)
                });
            }
            return result;
        }

    }
}
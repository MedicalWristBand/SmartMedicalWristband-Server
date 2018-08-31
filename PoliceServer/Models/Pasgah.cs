using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using PoliceServer.Enums;

namespace PoliceServer.Models
{
    [Table("Pasgah")]
    public class Pasgah : AbstractEntity
    {
        [Required]
        public string PasgahId { get; set; }
        public string FarsiName { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public Ostan Ostan
        {
            get
            {
                Ostan res = Ostan.NotSelected;
                if (Enum.TryParse(this.OstanName, out res))
                {
                    return res;
                }
                return Ostan.NotSelected;
            }
            set { this.OstanName = value.ToString(); }
        }
        public string OstanName { get; set; } 
        
        public string BossNationalCode { get; set; }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<User> Users { get; set; }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<ConfirmedPatte> ConfirmedPattes { get; set; }
    }
}
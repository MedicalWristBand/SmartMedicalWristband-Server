using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PoliceServer.AccessControl;

namespace PoliceServer.Models
{
    [Table("Ticket")]
    public class Ticket : AbstractEntity
    {
        public string Serial { get; set; }
        public DateTime DateTime { get; set; }
        public int UserID { get; set; }
        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual User User { get; set; }
        public RoleType RoleType { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}
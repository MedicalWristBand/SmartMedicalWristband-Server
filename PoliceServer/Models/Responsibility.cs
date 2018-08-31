using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PoliceServer.AccessControl;

namespace PoliceServer.Models
{
    [Table("Responsibilities")]
    public class Responsibility : AbstractEntity
    {
        public Responsibility()
        {

        }

        public Responsibility(RoleType role)
        {
            this.RoleType = role;
        }

        [NotMapped]
        public RoleType RoleType
        {
            get
            {
                RoleType res = RoleType.None;
                if (Enum.TryParse(this.RoleName, out res))
                {
                    return res;
                }
                return RoleType.None;
            }
            set { this.RoleName = value.ToString(); }
        }

        public int UserID { get; set; }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual User User { get; set; }

        [Required]
        public String RoleName { get; private set; }


        public override bool Equals(object obj)
        {
            Responsibility r = (Responsibility) obj;
            return this.RoleType.ToString().Equals(r.RoleType.ToString());
        }

        public override int GetHashCode()
        {
            return RoleType.GetHashCode();
        }
    }
}
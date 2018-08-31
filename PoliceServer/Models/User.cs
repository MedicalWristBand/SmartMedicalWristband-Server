using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PoliceServer.AccessControl;
using PoliceServer.Exceptions;
using PoliceServer.Enums;

namespace PoliceServer.Models
{
    [Table("User")]
    public class User : AbstractEntity
    {
        public User()
        {
            Responsibilities = new HashSet<Responsibility>();
            Patients = new HashSet<Patient>();
        }

        public string Name { get; set; }
        public string Family { get; set; }
        public string Username { get; set; }
        public string OrganizationCode { get; set; }
        public string Password { get; set; }


        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<Responsibility> Responsibilities { get; set; }
        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<Ticket> Tickets { get; set; }

        [XmlIgnore, ScriptIgnore, JsonIgnore]
        public virtual ICollection<Patient> Patients { get; set; }

        public string GetFullName()
        {
            return Name + " " + Family;
        }

        public RoleType GetHighestResponsibility()
        {
            RoleType r = RoleType.None;
            if (Responsibilities.Any(i => i.RoleType.Equals(RoleType.Staff)))
                r = RoleType.Staff;
            if (Responsibilities.Any(i => i.RoleType.Equals(RoleType.Nurse)))
                r = RoleType.Nurse;
            if (Responsibilities.Any(i => i.RoleType.Equals(RoleType.Doctor)))
                r = RoleType.Doctor;
            if (Responsibilities.Any(i => i.RoleType.Equals(RoleType.SystemAdmin)))
                r = RoleType.SystemAdmin;
            return r;
        }

        /// <summary>
        /// پسورد کاربر را تغییر می دهد
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <remarks>
        /// </remarks>
        public void ChangePassword(String oldPassword, String newPassword)
        {
            if (!this.Password.Equals(oldPassword))
            {
                throw new UserInterfaceException("خطای 8001: رمز عبور قدیمی معتبر نمی باشد. رمز عبور قدیمی را بررسی کرده و دوباره امتحان کنید.");
            }
            this.Password = newPassword;
        }
    }
}
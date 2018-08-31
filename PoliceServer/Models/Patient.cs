using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PoliceServer.Enums;

namespace PoliceServer.Models
{
    [Table("Patient")]
    public class Patient : AbstractEntity
    {
        public Patient()
        {
            LastUpdateDateTime = DateTime.Now;
            
        }

        public string Name { get; set; }
        public string Family { get; set; }
        public string PatientCode { get; set; }
        public string Status { get; set; }


        public int? DoctorID { get; set; }

        public User Dortor { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}
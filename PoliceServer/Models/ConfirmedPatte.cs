using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceServer.Models
{
    [Table("ConfirmedPatte")]
    public class ConfirmedPatte : AbstractEntity
    {
        public ConfirmedPatte()
        {
            recordDate = DateTime.Now;
        }

        public string PatteSerial { get; set; }

        [Column("RecordDate", TypeName = "smalldatetime")]
        public DateTime recordDate { get; private set; }

        [NotMapped]
        public String RecordDateString
        {
            get { return Utilities.CommonUtilities.DateConverterMiladiToHijri(this.recordDate) + " " + this.recordDate.Hour + ":" + ((this.recordDate.Minute)>10 ? this.recordDate.Minute.ToString():"0"+this.recordDate.Minute); }
            private set { this.recordDate = Utilities.CommonUtilities.DateConverterHijriToMiladi(value); }
        }

        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [Required]
        public int PatteID { get; set; }
        [ForeignKey("PatteID")]
        public virtual Patte Patte { get; set; }

        [Required]
        public int PasgahID { get; set; }
        [ForeignKey("PasgahID")]
        public virtual Pasgah Pasgah { get; set; }

        [Required]
        public string ConfirmationIp { get; set; }
    }
}
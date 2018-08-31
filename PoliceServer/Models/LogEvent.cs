using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoliceServer.Models
{
//    [Table("LogEvent")]
    public class LogEvent
    {
        #region PrimaryKey          // This Function Should Not Inheritance From AbstractEntity
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Key]
        [Column(Order = 0)]
        public int PasgahCode { get; private set; }

        #endregion PrimaryKey

        public LogEvent()
        {
            User user = Utilities.CommonUtilities.GetUser();
            if (user != null)
            {
                this.UserID = user.Id;
            }
            Date = DateTime.Now;
        }

        public int UserID { get; private set; }

//        public int LogActionID { get; set; }
//
//        [ForeignKey("LogActionID")]
//        public virtual LogAction LogAction { get; set; }

        [NotMapped]
        private string _Description { get; set; }

        public string Description
        {
            get
            {
                return _Description??String.Empty;
            }
            set {
                _Description = value.Length > 1000 ? value.Substring(0, 999) : value;
            }
        }

        public string TableName { get; set; }

        public int? RecordID { get; set; }

        public DateTime Date { get; private set; }

        public string GetUserInfo()
        {
            try
            {
                return Repository.UserRepository.GetInstance().FindById(this.UserID).GetFullName();
            }
            catch (Exception)
            {
                return "نامشخص";
            }
        }

        public DateTime GetDate()
        {
            return Date;
        }

        public string RecordDate
        {
            get
            {
                return Utilities.CommonUtilities.DateConverterMiladiToHijri(this.Date);
            }
        }

    }
}

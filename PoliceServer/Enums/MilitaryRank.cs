using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PoliceServer.Enums
{
    public class UserTitleHelper
    {
        public static List<String> GetPersion(List<UserTitle> list )
        {
            List<string> result = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(EnumHelper.ToEnumString(list[i]));
            }
            return result;
        } 
    }
    public enum UserTitle
    {
        [EnumMember(Value = "پزشک")]
        Doctor,
        [EnumMember(Value = "مدیر سیستم")]
        Admin,

        [EnumMember(Value = "پرستار")]
        Nurse,
        [EnumMember(Value = "پرسنل")]
        HospitalEmployee
    }
}
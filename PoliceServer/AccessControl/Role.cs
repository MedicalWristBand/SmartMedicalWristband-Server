using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using PoliceServer.Enums;

namespace PoliceServer.AccessControl
{
    public class Role
    {
        private string _roleName;
        private string _farsiRole;
        private RoleType _roleType;
        private HashSet<RoleType> canAssign;  

        public Role(RoleType role)
        {
            this._roleType = role;
            this._roleName = role.ToString();
            this._farsiRole = EnumHelper.ToEnumString<RoleType>(role);
            canAssign = new HashSet<RoleType>();
        }

        public String GetFarsiRole()
        {
            return _farsiRole;
        }

        public RoleType GetRoleType()
        {
            return _roleType;
        }

        public String GetRoleName()
        {
            return _roleName;
        }

        public void CanAssignRole(Role role)
        {
            canAssign.Add(role.GetRoleType());
        }

        public List<RoleType> GetCanAssignRole()
        {
           return canAssign.ToList();
        }
 
    }

    public enum RoleType
    {
        [EnumMember(Value = "بدون نقش")]
        None,

        [EnumMember(Value = "پزشک")]
        Doctor,

        [EnumMember(Value = "پرستار")]
        Nurse,

        [EnumMember(Value = "خدمات")]
        Staff,

        [EnumMember(Value = "مدیر سیستم")]
        SystemAdmin
        
    }
}
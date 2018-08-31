using System;
using System.Collections.Generic;
using System.Linq;
using PoliceServer.AccessControl;
using PoliceServer.Models;


namespace PoliceServer.UserControl
{
    public partial class uPermission : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public List<RoleType> GetResponsibilities()
        {
            List<RoleType> responsibilities = new List<RoleType>();

            if (chkSystemAdmin.Checked)
                responsibilities.Add(RoleType.SystemAdmin);
            if (chkDoctor.Checked)
                responsibilities.Add(RoleType.Doctor);
            if (chkNurse.Checked)
                responsibilities.Add(RoleType.Nurse);
            if (chkStaff.Checked)
                responsibilities.Add(RoleType.Staff);
            return responsibilities;
        }

        public void SetResponsibilities(User user)
        {
            ICollection<Responsibility> responsibilities = user.Responsibilities;
            lblName.Text = user.Name;
            lblFamily.Text = user.Family;
            lblNationalCode.Text = user.Username;
            #region ALL FALSE
                chkSystemAdmin.Checked = false;
                chkDoctor.Checked = false;
                chkNurse.Checked = false;
                chkStaff.Checked = false;
            #endregion ALL FALSE

            User loginUser = PoliceServer.Utilities.CommonUtilities.GetUser();
            if (loginUser.Responsibilities.Any(r => r.RoleName.Equals(RoleType.SystemAdmin.ToString())))
            {
                chkSystemAdmin.Enabled = true;
            }

            if (responsibilities.Any(i => i.RoleType.Equals(RoleType.SystemAdmin)))
                chkSystemAdmin.Checked = true;
            if (responsibilities.Any(i => i.RoleType.Equals(RoleType.Doctor)))
                chkDoctor.Checked = true;
            if (responsibilities.Any(i => i.RoleType.Equals(RoleType.Nurse)))
                chkNurse.Checked = true;
            if (responsibilities.Any(i => i.RoleType.Equals(RoleType.Staff)))
                chkStaff.Checked = true;
        }

    }
}
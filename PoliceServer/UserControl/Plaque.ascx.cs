using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceServer.Exceptions;
using PoliceServer.Utilities;

namespace PoliceServer.UserControl
{
    public partial class Plaque : System.Web.UI.UserControl
    {
        internal static readonly log4net.ILog Log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Error"] != null)
            {
                var message = Session["Error"];
                if (null == message)
                {
                    Response.Redirect(ResolveUrl("~/Default/Index"), false);
                }
                else
                {
                    lblError.Text = message.ToString();
                    lblError.Visible = true;
                }
                Session.Remove("Error");
            }
            else
            {
                lblError.Visible = false;                       
            }
        }

        //TODO: Logger
        //TODO: foreign plaque and firefox problem
        public String GetPlaque()
        {
            String finalPlaque = "";
            try
            {
//                if (chkStrange.Checked)
//                {
//                    finalPlaque = CommonUtilities.CorrectStringFarsi(txtStrange.Text);
//                }
//                else
                {
                    finalPlaque = txtTwoLeft.Text + txtAlphabet.Text + txtThree.Text + "ایران" + txtCityCode.Text;
                }
                return CommonUtilities.CorrectStringFarsi(finalPlaque);
            }
            catch (FormatException ex)
            {
                Log.Debug("Entering wrong format data in baskool billID", ex);
                lblError.Text = "لطفا شماره پلاک و یا شماره قبض باسکول را صحیح وارد نمایید";
                lblError.Visible = true;
            }
            catch (UserInterfaceException ex)
            {
                Log.Warn("Getting exception in loading data for PatteNo:'" + txtPatteNo.Text + "' or plaque:'" + finalPlaque + "'", ex);
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
            catch (Exception ex)
            {
                Log.Fatal("Unexpected exception in loading information for PatteNo:'" + txtPatteNo.Text + "' or plaque:'" + finalPlaque + "'", ex);
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
            return null;
        }

        public String GetBaskoolBillId()
        {
            return CommonUtilities.GetString(txtPatteNo.Text);
        }

        /// <summary>
        /// مشخص می کند که آیا پلاک توسط کاربر وارد شده است یا خیر
        /// </summary>
        /// <returns></returns>
        public Boolean IsPlaqueEntered()
        {
//            if (chkStrange.Checked)
//            {
//                if (CommonUtilities.IsEmpty(txtStrange.Text))
//                {
//                    return false;
//                }
//            }
//            else
            {
                if (CommonUtilities.IsEmpty(txtThree.Text))
                {
                    return false;
                }
                if (CommonUtilities.IsEmpty(txtTwoLeft.Text))
                {
                    return false;
                }
                if (CommonUtilities.IsEmpty(txtCityCode.Text))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// مشخص می کند که آیا شماره باسکول توسط کاربر تولید شده است یا خیر
        /// </summary>
        /// <returns></returns>
        public Boolean IsBaskoolBillIdEntered()
        {
            return !CommonUtilities.IsEmpty(txtPatteNo.Text);
        }

        public void ShowError(UserInterfaceException userInterfaceException)
        {
            lblError.Text = userInterfaceException.Message;
            lblError.Visible = true;
        }

    }
}
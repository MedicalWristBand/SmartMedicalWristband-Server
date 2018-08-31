<%@ Page Title="تغییر رمز عبور" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
    }

</script>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel HorizontalAlign="Center" Style="text-align: center" DefaultButton="btnSubmit" runat="server">
        <asp:Panel ID="pnlErrorMain" runat="server">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#EE1111"
                Text="ErrorMessage" Visible="False" ClientIDMode="Static"></asp:Label>
        </asp:Panel>
        <br />
        <br />
        
         <div class="form-group col-md-6 col-md-offset-3">
                <form method="post" action="~/Account/ManagePassword">
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-4 control-label">رمز عبور فعلی</asp:Label>
                    <div class="col-md-6">
                        <input type="password" id="oldPass" class="col-md-12" name="oldPass" required=""/>
                    </div>
                        </div>
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-4 control-label">رمز عبور جدید</asp:Label>
                    <div class="col-md-6">
                        <input type="password" id="newPass" class="col-md-12" name="newPass" required=""/>
                    </div>
                        </div>
                    <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-4 control-label">تکرار رمز عبور جدید</asp:Label>
                    <div class="col-md-6">
                        <input type="password" id="confirmPass" class="col-md-12" name="confirmPass" required=""/>
                    </div>
                        </div>
                    <div class="col-md-4 col-md-offset-6" style="margin-top: 10px">
                    <asp:Button ID="btnSubmit" runat="server" Font-Names="B Nazanin" Text="تغییر رمز" CssClass="btn btn-success col-md-12" />
                        </div>
                </form>
            </div>
        
    </asp:Panel>

</asp:Content>




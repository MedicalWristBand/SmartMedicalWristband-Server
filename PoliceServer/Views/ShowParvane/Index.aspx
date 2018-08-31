<%@ Page Title="جستجوی پروانه" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>
<script runat="server">
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
</script>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel HorizontalAlign="Center" Style="text-align: center" DefaultButton="btnSubmit" runat="server">
        <asp:Panel ID="pnlErrorMain" runat="server">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#EE1111"
                Text="ErrorMessage" Visible="False" ClientIDMode="Static"></asp:Label>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="pnlDescription" Visible="True" runat="server">
            <span class="ui-state-active ui-corner-all" style="padding: 10px">برای مشاهده اطلاعات یک پروانه شماره سریال آن را وارد نمایید.</span>
        </asp:Panel>
        <br/>
        
            <div class="form-group col-md-4 col-md-offset-4">
                <form method="post" action="~/ShowParvane/Index">
                    <div class="col-md-8">
                        <input type="text" id="parvaneSerial" class="col-md-12" name="parvaneSerial" value="" />
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" Font-Names="B Nazanin" Text="جستجوی پروانه" CssClass="btn btn-success col-md-4" />
                </form>
            </div>
        
    </asp:Panel>

</asp:Content>




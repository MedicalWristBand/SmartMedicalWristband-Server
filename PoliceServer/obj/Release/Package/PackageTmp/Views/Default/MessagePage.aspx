<%@ Page Title="پیغام سیستم" Language="C#" MasterPageFile="~/PoliceSite.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Type"] != null)
            {
                if (Session["Type"].Equals("OnlyMessage"))
                {
                    imgLogo.Visible = false;
                }
                if (Session["Type"].Equals("Error"))
                {
                    imgLogo.ImageUrl = ResolveUrl("~/Content/Images/delete.png");
                }
                Session.Remove("Type");
            }
            var message = Session["Information"];
            if (null == message)
            {
                Response.Redirect(ResolveUrl("~/Default/Index"), false);
            }
            else
            {
                lblInfo.Text = message.ToString();
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        PoliceServer.Utilities.SessionManager.GetInstance().FreeRedirectSource();
        Session.Remove("Information");
    }

    public String GetUrl()
    {
        var redirect = Session["redirect"];
        Session.Remove("redirect");
        if (redirect == null)
        {
            return "~/Default/Index";
        }
        else
        {
            return redirect.ToString();
        }
    }
    

</script>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
        <br />
        <br />
        <br />
        <br />
        <asp:Image ID="imgLogo" ImageUrl="~/Content/images/check.png" runat="server" />
        <asp:Label ID="lblInfo" runat="server" Text="Label" Font-Bold="True"
            Font-Size="XX-Large"></asp:Label><br />
        <br/>
        <br/>
        <br/>
        <br/>
        <script type="text/javascript">
            window.onload = function () {
                var redirect = function () {
                    window.location = '<%= ResolveUrl(GetUrl()) %>';
                };
                setTimeout(redirect, 3000);
            };
    </script>
    </div>
</asp:Content>

<%@ Page Title="ایجاد مرکز استعلام" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Import Namespace="PoliceServer.Enums" %>
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
                    Response.Redirect("../Default/Index", false);
                }
                else
                {
                    ErrorMessage.Text = message.ToString();
                }
                Session.Remove("Error");
            }
        }
        PasgahOstan.DataSource = EnumHelper.GetPersionNameList(EnumHelper.GetAllEnumList<Ostan>());
        PasgahOstan.DataBind();
    }

</script>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PasgahID" CssClass="col-md-2 control-label">شناسه مرکز استعلام</asp:Label>
            <div class="col-md-6">
                <asp:TextBox type="text" runat="server" ID="PasgahID" CssClass=" form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasgahFarsiName"
                    CssClass="text-danger" ErrorMessage="شناسه مرکز استعلام نمی‌تواند خالی باشد." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PasgahFarsiName" CssClass="col-md-2 control-label">نام مرکز استعلام</asp:Label>
            <div class="col-md-6">
                <asp:TextBox type="text" runat="server" ID="PasgahFarsiName" CssClass=" form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasgahFarsiName"
                    CssClass="text-danger" ErrorMessage="نام مرکز استعلام نمی‌تواند خالی باشد." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PasgahOstan" CssClass="col-md-2 control-label">استان</asp:Label>
            <div class="col-md-6">
                <asp:DropDownList runat="server" ID="PasgahOstan" ClientIDMode="Static" CssClass=" form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasgahOstan"
                    CssClass="text-danger" ErrorMessage="استان را مشخص کنید." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PasgahAddress" CssClass="col-md-2 control-label">آدرس مرکز</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="PasgahAddress" ClientIDMode="AutoID" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PasgahAddress"
                    CssClass="text-danger" ErrorMessage="آدرس دقیق را وارد کنید." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="BossNationalCode" CssClass="col-md-2 control-label">کد ملی ریاست مرکز</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="BossNationalCode" ClientIDMode="Inherit" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="BossNationalCode"
                    CssClass="text-danger" ErrorMessage="کد ملی ریاست مرکز را وارد کنید." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-6 col-md-offset-2">
                <p class="text-danger col-md-8">
                    <asp:Literal runat="server" ID="ErrorMessage" /></p>
                <asp:Button runat="server" Text="ثبت" CssClass="btn btn-success btn-lg col-md-4" />
            </div>
        </div>
    </div>
</asp:Content>

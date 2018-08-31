<%@ Page Title="ایجاد کاربر جدید" Language="C#" MasterPageFile="~/PoliceSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="PoliceServer.Enums" %>
<%@ Import Namespace="PoliceServer.Repository" %>
<%@ Import Namespace="PoliceServer.Utilities" %>
<%@ Import Namespace="PoliceServer.AccessControl" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
//            rank.DataSource = EnumHelper.GetPersionNameList(EnumHelper.GetAllEnumList<MilitaryRank>());
//            rank.DataBind();


//            Pasgah.DataSource = PasgahHelper.GetAllPasgahNamesAndID(PasgahRepository.GetInstance().GetAllPasgahs()).Keys;
//            Pasgah.DataBind();

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
        CheckBoxList cbList = new CheckBoxList();
        RoleManager.GetInstance().CanAssignRoles(CommonUtilities.GetUser(false)).ForEach(r => cbList.Items.Add(new ListItem(EnumHelper.ToEnumString(r))));
        Roles.Controls.Add(cbList);
    }
</script>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="form-horizontal" style="margin-bottom: 50px">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">نام</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger" ErrorMessage="نام نمی‌تواند خالی باشد." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Family" CssClass="col-md-2 control-label">نام خانوادگی</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="Family" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Family"
                    CssClass="text-danger" ErrorMessage="نام خانوادگی نمی‌تواند خالی باشد." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="NationalCode" CssClass="col-md-2 control-label">کد ملی</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="NationalCode" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="NationalCode"
                    CssClass="text-danger" ErrorMessage="کد ملی خود را وارد کنید." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="OrganizationCode" CssClass="col-md-2 control-label">کد پرسنلی</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="OrganizationCode" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="OrganizationCode"
                    CssClass="text-danger" ErrorMessage="شماره کد خود را وارد کنید." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">رمز عبور(حداقل ۶ کاراکتر)</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="رمز عبور با طول حداقل ۶ کاراکتر وارد نمایید." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">تکرار رمزعبور</asp:Label>
            <div class="col-md-6">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Static" ErrorMessage="تکرار رمزعبور خود را وارد نمایید." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="رمز عبور و تکرار آن با هم مطابقت ندارد." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Roles" CssClass="col-md-2 control-label">نقش در سامانه</asp:Label>
            <div class="col-md-6">
                <asp:PlaceHolder runat="server" ID="Roles"></asp:PlaceHolder>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-6 col-md-offset-2">
                <p class="text-danger col-md-8">
                    <asp:Literal runat="server" ID="ErrorMessage" />
                </p>
                <asp:Button runat="server" Text="ثبت نام" CssClass="btn btn-success btn-lg  col-md-4" />
            </div>
        </div>
    </div>
</asp:Content>

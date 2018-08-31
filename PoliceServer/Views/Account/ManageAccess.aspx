<%@ Page Title="ویرایش دسترسی کاربران" Language="C#" EnableEventValidation="false" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Register TagPrefix="PoliceServer" TagName="uPermission" Src="~/UserControl/uPermission.ascx" %>
<%@ Import Namespace="PoliceServer.Models" %>

<script runat="server">

    protected void Page_Load()
    {
        userDataPanel.Visible = false;
        var userToEdit = (User)ViewData["userToEdit"];
        if (userToEdit != null && !userToEdit.Username.Equals(""))
        {

            uPermission.SetResponsibilities(userToEdit);
            // to log for previous responsibilities
            //grdLogEvents.DataBind();
            userDataPanel.Visible = true;
        }

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
//    private void EntityDataSourceAccessReport_OnQueryCreated(object sender, QueryCreatedEventArgs e)
//    {
//        if (true) ;
//    }
//
//    protected void EntityDataSourceAccessReport_OnContextCreating(object sender, Microsoft.AspNet.EntityDataSource.EntityDataSourceContextCreatingEventArgs e)
//    {
//        PoliceContext context = PoliceServer.Utilities.ContextCreator.GetInstance().GetContext();
//        e.Context = (context as IObjectContextAdapter).ObjectContext;
//    }

</script>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel HorizontalAlign="Center" Style="text-align: center" runat="server">

        <asp:Panel ID="pnlErrorMain" runat="server">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#EE1111"
                Text="ErrorMessage" Visible="False" ClientIDMode="Static"></asp:Label>
        </asp:Panel>
        <br />


        <div class="form-group col-md-4 col-md-offset-4">
            <form method="post" action="~/Account/ManageAccess">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">کد ملی کاربر</asp:Label>
                    <div class="col-md-5">
                        <input type="text" id="nationalCode" class="col-md-12" name="nationalCode" required="" />
                    </div>
                    <div class="col-md-4">
                        <input type="submit" value="جستجو" name="action:ManageAccess" class="btn btn-success col-md-12" />
                        <%--                        <asp:Button ID="btnSubmit" runat="server" Font-Names="B Nazanin" Text="جسنجو" CssClass="btn btn-success col-md-12" />--%>
                    </div>
                </div>
            </form>
        </div>

        <asp:Panel ID="userDataPanel" CssClass="col-md-4 col-md-offset-4" runat="server" Visible="False">

            <br />
            <br />
            <form method="post" action="ManageAccess">

                <PoliceServer:uPermission runat="server" ID="uPermission" />


                <%--            <ef:EntityDataSource ID="EntityDataSourceAccessReport" EntitySetName="LogEvents"  OnQueryCreated="EntityDataSourceAccessReport_OnQueryCreated" OnContextCreating="EntityDataSourceAccessReport_OnContextCreating" runat="server"></ef:EntityDataSource>    --%>
                <%--            <div id="Accordion" class="ui-accordion-header" clientidmode="Static" style="width: 800px">--%>
                <%--                <caption>--%>
                <%--                    <h3>تغییرات پیشین دسترسی ها:</h3>--%>
                <%--                    <div id="detailfield">--%>
                <%--                        <asp:GridView ID="grdLogEvents" AutoGenerateColumns="False" DataSourceID="EntityDataSourceAccessReport" Width="100%" BorderWidth="0" ShowHeader="False" runat="server" AllowPaging="True">--%>
                <%--                            <Columns>--%>
                <%--                                <asp:BoundField DataField="Description" />--%>
                <%--                            </Columns>--%>
                <%--                        </asp:GridView>--%>
                <%--                    </div>--%>
                <%--                </caption>--%>
                <%--            </div>--%>

                <br />
                <input type="submit" class="btn-success btn-large col-md-4 col-md-offset-4" value="ذخیره" name="action:UpdateResponsibilities" />
                <%--                <asp:Button ID="btnSubmit2" CssClass="btn btn-success" runat="server" Text="اعمال تغییرات" />--%>
            </form>
        </asp:Panel>
    </asp:Panel>
</asp:Content>





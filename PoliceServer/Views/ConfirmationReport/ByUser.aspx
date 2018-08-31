<%@ Page Title="گزارش تاییده های یک فرد" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>
<%@ Import Namespace="PoliceServer.Models" %>
<%@ Import Namespace="PoliceServer.Utilities" %>


<script runat="server">

    protected void Page_Load()
    {
        List<ConfirmedPatte> allConfirmedPatte = (List<ConfirmedPatte>)ViewData["allConfirmationForUser"];
        if (allConfirmedPatte != null)
        {
            grdHistory.DataSource = allConfirmedPatte;
            grdHistory.DataBind();
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
</script>


<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel HorizontalAlign="Center" Style="text-align: center" runat="server">

        <asp:Panel ID="pnlErrorMain" runat="server">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#EE1111"
                Text="ErrorMessage" Visible="False" ClientIDMode="Static"></asp:Label>
        </asp:Panel>
        <br />


        <div class="form-group col-md-4 col-md-offset-4">
            <form method="post" action="~/ConfirmationReport/ByUser">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">کد ملی کاربر</asp:Label>
                    <div class="col-md-5">
                        <input type="text" id="nationalCode" class="col-md-12" name="nationalCode" required="" />
                    </div>
                    <div class="col-md-4">
                        <input type="submit" value="جستجو" name="action:SearchByUser" class="btn btn-success col-md-12" />
                    </div>
                </div>
            </form>
        </div>
        
        <br/>
        <br/>
        <br/>
        
        <div class="form-group">
            <div class="col-md-10 col-md-offset-1">
                <asp:GridView runat="server" ID="grdHistory" CssClass="table table-responsive rtl ui-corner-all" HorizontalAlign="Center" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="نام تایید کننده">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).User.GetFullName() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تاریخ">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).RecordDateString %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="مرکز استعلام">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).Pasgah.FarsiName %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شماره سریال پته">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).PatteSerial %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شماره سریال پروانه">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).Patte.KotajNos.Replace("*","") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Right" />
                    <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
</asp:Content>




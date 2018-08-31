<%@ Page Title="گزارش تاییده های یک مرکز" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Import Namespace="PoliceServer.Models" %>
<%@ Import Namespace="PoliceServer.Utilities" %>
<%@ Import Namespace="PoliceServer.Repository" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>


<script runat="server">

    protected void Page_Load()
    {
        List<ConfirmedPatte> allConfirmedPatte = (List<ConfirmedPatte>)ViewData["allConfirmationForPasgah"];
        if (allConfirmedPatte != null)
        {
            grdHistory.DataSource = allConfirmedPatte;
            grdHistory.DataBind();
        }
        if (!IsPostBack)
        {
            Pasgah.DataSource = PasgahHelper.GetAllPasgahNamesAndID(PasgahRepository.GetInstance().GetAllPasgahs()).Keys;
            Pasgah.DataBind();

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


        <div class="form-group col-md-8 col-md-offset-2">
            <form method="post" action="~/ConfirmationReport/ByPasgah">
                <div class="form-group" dir="rtl">
                    <asp:Label runat="server" CssClass="col-md-3 control-label">مرکز استعلام مورد نظر را انتخاب کنید</asp:Label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="Pasgah" ClientIDMode="Static" CssClass="form-control col-md-4"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Pasgah"
                            CssClass="text-danger" ErrorMessage="محل خدمت را مشخص کنید." />
                    </div>
                    <div class="col-md-2">
                        <input type="submit" value="جستجو" name="action:SearchByPasgah" class="btn btn-success col-md-12" />
                    </div>
                </div>
            </form>
        </div>

        <br />
        <br />
        <br />

        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="grdHistory" CssClass="table table-responsive rtl ui-corner-all" HorizontalAlign="Center" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="ردیف">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تاریخ">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).RecordDateString %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="نام تایید کننده">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).User.GetFullName() %>
                            </ItemTemplate>
                        </asp:TemplateField>
<%--                        <asp:TemplateField HeaderText="آدرس تایید کننده">--%>
<%--                            <ItemTemplate>--%>
<%--                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).ConfirmationIp.IsNullOrWhiteSpace()? "نامشخص" : CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).ConfirmationIp  %>--%>
<%--                            </ItemTemplate>--%>
<%--                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="شرح کالا">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).Patte.GetAllCommoditiesInString() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="مقصد">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).Patte.GetOstanAndShahr() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شماره سریال پته">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).PatteSerial %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شماره سریال پروانه">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).Patte.KotajNos.IsNullOrWhiteSpace() ? "نامشخص": CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).Patte.KotajNos.Replace("*","") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="پلاک حامل">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<ConfirmedPatte>(Container.DataItem).Patte.getPlaque()%>
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




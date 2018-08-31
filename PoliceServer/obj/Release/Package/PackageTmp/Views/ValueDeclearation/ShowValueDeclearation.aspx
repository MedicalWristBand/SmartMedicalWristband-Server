<%@ Page Title="اطلاعات اظهارنامه ارزش" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>
<%@ Import Namespace="PoliceServer.Utilities" %>
<%@ Import Namespace="PoliceServer.CustomsValueDeclarationInformation" %>
<%@ Import Namespace="PoliceServer.Models" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        List<customsValueDeclaration> valueDeclarations = (List<customsValueDeclaration>)ViewData["ValueDeclaration"];
        if (valueDeclarations != null && valueDeclarations.Count > 0)
        {
            valueDeclarationGridVeiw.DataSource = valueDeclarations;
            valueDeclarationGridVeiw.DataBind();
        }
    }
</script>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12 col-lg-12 col-sm-12">
        <asp:GridView runat="server" ID="valueDeclarationGridVeiw" CssClass="table table-responsive rtl text-center" HorizontalAlign="Center" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="شناسه کالا">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).commodityHSCode %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="شرح کالا">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).commodityDescription %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="تعداد">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).commodityItemQuantity %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="نام تجاری لاتین">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).brand%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ارزش ارزی">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).currencyValue%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="کد نوع ارز">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).currencyTypeCode%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="کدخاص تعرفه">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).tsc%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="مدل کالا">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).commodityModel%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="وزن هر قلم">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).weightInKG%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="کد کشور">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).countryCode%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
        </asp:GridView>
        <div class="col-md-12" style="text-align: right">
            <asp:Label ID="commodityTableHeader" runat="server" Text="مسئولیت هرگونه مغایرت در اطلاعات اظهاری فوق بر عهده‌ی صاحب کالا می‌باشد." Font-Size="16" />
        </div>
    </div>
</asp:Content>
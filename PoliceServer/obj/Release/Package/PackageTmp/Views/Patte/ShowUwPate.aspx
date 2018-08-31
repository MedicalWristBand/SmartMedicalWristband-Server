<%@ Page Title="اطلاعات پته‌ی انبار شهری" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Import Namespace="PoliceServer.GetUrbanWarehousePermit" %>
<%@ Import Namespace="PoliceServer.Utilities" %>
<%@ Import Namespace="PoliceServer.Models" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Patte pate = (Patte)ViewData["uwPate"];
        List<ConfirmedPatte> history = (List<ConfirmedPatte>)ViewData["History"];
        if (pate == null)
        {
            Session["Information"] = "اطلاعات دریافتی برای این پته نامعتبر است.";
            Session["Type"] = "Error";
            Response.Redirect(ResolveUrl("~/Default/MessagePage"));
        }
        else
        {
            FillTemplate(pate);

            if (history != null)
            {
                FillHistory(history);
            }
            else
            {
                lblHistory.Text = "مشاهده سوابق تایید پته: این پته تا کنون در هیچ مرکزی تایید نشده است.";
            }
        }
    }

    private void FillHistory(List<ConfirmedPatte> history)
    {
        grdHistory.DataSource = history;
        grdHistory.DataBind();
    }

    private void FillTemplate(Patte pate)
    {
        patteDbId.Text = pate.Id.ToString();
        lblPateSerial.Text = pate.PatteSerial;
        lblMabda.Text = pate.CityOfOrigin;
        lblMaghsad.Text = pate.DestinationCity;
        lblOperatorNationalId.Text = pate.OperatorNationalId;
        lblConsignee.Text = pate.ConsigneeNationalId;
        IssuanceDate.Text = CommonUtilities.DateConverterMiladiToHijri(pate.issuanceDate);
        IssuanceTime.Text = pate.issuanceDate.ToLongTimeString();
        DriverName.Text = pate.Driver.GetFullName();
        DriverNatinalCode.Text = pate.Driver.NationalCode;
        DriverPhone.Text = pate.Driver.PhoneNo.IsNullOrWhiteSpace() ? "ناموجود" : pate.Driver.PhoneNo;
        txtCityCode.Text = pate.PlaqueCityCode;
        txtAlphabet.Text = pate.PlaqueMiddleCharacter;
        txtTwoLeft.Text = pate.PlaqueLeftTwoDigits;
        txtThree.Text = pate.PalqueMiddleThreeDigits;

        CommodityDescription.DataSource = pate.Containers.ToList()[0].Commoditys;
        CommodityDescription.DataBind();

        //        uwPate.Containers.ForEach(con => ContainerNo.Text += con.ContainerNumber);
        //        uwPate.Containers.ForEach(con => ContainerType.Text += con.ContainerType);
        //        patteDbId.Text = uwPate.Id.ToString();
        //        lblPureWeight.Text = uwPate.Weight.ToString();
        //        if (ContainerType.Text.Contains("20"))
        //            lblWeight.Text = (uwPate.Weight + 2000).ToString();
        //        else if (ContainerType.Text.Contains("40"))
        //        {
        //            lblWeight.Text = (uwPate.Weight + 4000).ToString();
        //        }
        //        else
        //        {
        //            lblWeight.Text = uwPate.Weight.ToString();
        //        }

    }


    private object getPateDBID()
    {
        return patteDbId.Text;
    }

    private object getPateSerial()
    {
        return lblPateSerial.Text;
    }

</script>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ScriptCotent">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#pateSerialFromBarcodeReader').focus();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--    <div class="form-horizontal col-md-12" style="text-align: center; margin-bottom: 20px">--%>
<%--        <div class="form-group">--%>
<%--            <span class="ui-state-error ui-corner-all" style="padding: 10px; width: 100%; font-size: 20px">این پته برای تردد در داخل شهر صادر شده است و اجازه تردد بین شهری ندارد</span>--%>
<%--        </div>--%>
<%--    </div>--%>

    <div class="form-horizontal col-md-6">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblPateSerial" CssClass="col-md-4 control-label">سریال پته</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblPateSerial" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblMabda" CssClass="col-md-4 control-label">مبدا</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblMabda" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblMaghsad" CssClass="col-md-4 control-label">مقصد</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblMaghsad" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblOperatorNationalId" CssClass="col-md-4 control-label">کدملی کارمند انبار</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblOperatorNationalId" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblConsignee" CssClass="col-md-4 control-label">کد ملی صاحب کالا</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblConsignee" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="IssuanceDate" CssClass="col-md-4 control-label">تاریخ صدور پته</asp:Label>
            <div class="col-md-4">
                <asp:Label runat="server" ID="IssuanceDate" CssClass="form-control" />
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" ID="IssuanceTime" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DriverName" CssClass="col-md-4 control-label">نام و نام خانوادگی راننده</asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="DriverName" CssClass=" form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DriverNatinalCode" CssClass="col-md-4 control-label">کدملی راننده</asp:Label>
            <div class="col-md-3">
                <asp:Label type="text" runat="server" ID="DriverNatinalCode" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="DriverPhone" CssClass="col-md-2 control-label">شماره موبایل</asp:Label>
            <div class="col-md-3">
                <asp:Label type="text" runat="server" ID="DriverPhone" CssClass=" form-control" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DriverPhone" CssClass="col-md-4 control-label">شماره پلاک</asp:Label>
            <div class="col-md-8" align="right">
                <table style="background-image: url('/Content/Images/pelak.gif'); width: 250px; height: 65px; font-size: 9pt; line-height: 15px" border="0" dir="rtl">
                    <tbody>
                        <tr>
                            <td valign="top">
                                <table style="text-align: center" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td style="line-height: 14px; font-size: 14px;" align="center" width="60">
                                                <span></span>
                                            </td>
                                            <td style="line-height: 14px; font-size: 12px;">&nbsp;
                                                    </td>
                                            <td style="line-height: 14px; font-size: 12px;" align="center">&nbsp;
                                                    </td>
                                            <td style="line-height: 14px; font-size: 12px;" align="center">&nbsp;
                                                    </td>
                                        </tr>
                                        <tr style="height: 40px">
                                            <td style="font-size: 13px;" width="50" valign="middle">
                                                <asp:Label ID="txtCityCode" Style="font-size: 18px; width: 30px; text-align: center; vertical-align: bottom" runat="server"></asp:Label>
                                            </td>
                                            <td style="line-height: 14px; font-size: 13px; width: 55px;" valign="middle">
                                                <asp:Label ID="txtThree" MaxLength="3" name="txtP2" Style="font-size: 18px; height: 25px; width: 40px; text-align: center; padding-top: 5px" runat="server"></asp:Label>
                                            </td>
                                            <td style="line-height: 14px; font-size: 13px; width: 45px" valign="middle">
                                                <asp:Label ID="txtAlphabet" runat="server" Width="40px" />
                                            </td>
                                            <td style="line-height: 14px; font-size: 13px;" valign="middle">
                                                <asp:Label ID="txtTwoLeft" name="txtP1" MaxLength="2" Style="font-size: 18px; width: 30px; text-align: center;" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>





        <%--        <div class="form-group">--%>
        <%--            <asp:Label runat="server" AssociatedControlID="ContainerNo" CssClass="col-md-4 control-label">شماره و نوع کانتینر</asp:Label>--%>
        <%--            <div class="col-md-4">--%>
        <%--                <asp:Label type="text" runat="server" ID="ContainerType" CssClass="form-control" />--%>
        <%--            </div>--%>
        <%--            <div class="col-md-4">--%>
        <%--                <asp:Label runat="server" ID="ContainerNo" CssClass="form-control" />--%>
        <%--            </div>--%>
        <%--        </div>--%>
        <%--        <div class="form-group">--%>
        <%--            <asp:Label runat="server" AssociatedControlID="lblWeight" CssClass="col-md-4 control-label">وزن کالا و کانتینر</asp:Label>--%>
        <%--            <div class="col-md-3">--%>
        <%--                <asp:Label type="text" runat="server" ID="lblWeight" CssClass="form-control" />--%>
        <%--            </div>--%>
        <%--            <asp:Label runat="server" AssociatedControlID="lblPureWeight" CssClass="col-md-2 control-label">وزن بار </asp:Label>--%>
        <%--            <div class="col-md-3">--%>
        <%--                <asp:Label type="text" runat="server" ID="lblPureWeight" CssClass="form-control" />--%>
        <%--            </div>--%>
        <%--        </div>--%>


        <div class="form-group">
            <asp:Label runat="server" Visible="False" ID="patteDbId"></asp:Label>
        </div>



    </div>


    <div class="form-horizontal col-md-6">
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="CommodityDescription" CssClass="table table-responsive rtl ui-corner-all text-center" HorizontalAlign="Center" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="شرح کالا">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).CommodityTariffDescription %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شناسه کالا">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).CommodityHsCode %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تعداد">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).CommodityItemQuantity %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وزن">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).NetWeight %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblHistory" CssClass="col-md-12 control-label" runat="server" Text="مشاهده سوابق تایید پته" />
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="grdHistory" CssClass="table table-responsive rtl ui-corner-all" HorizontalAlign="Center" AutoGenerateColumns="False">
                    <Columns>
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
                    </Columns>
                    <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Right" />
                    <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <form method="post">
                    <input type="hidden" id="pasgahID" name="pasgahID" value="<%=Session["PasgahID"].ToString() %>">
                    <input type="hidden" id="patteID" name="patteID" value="<%= getPateDBID() %>">
                    <input type="hidden" id="pateSerial" name="pateSerial" value="<%= getPateSerial() %>">
                    <input type="text" id="pateSerialFromBarcodeReader" name="pateSerialFromBarcodeReader" placeholder="برای تایید میتوانید با خواندن دوباره بارکد پته این پته را تایید کنید" value="" class="col-md-7 col-md-offset-5" />
                    <input type="submit" name="action:Confirm" value="تایید عبور پته از مرکز" class="btn btn-success btn-lg col-md-6 col-md-offset-6" />
                </form>
            </div>
        </div>
    </div>

</asp:Content>



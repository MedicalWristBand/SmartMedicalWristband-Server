<%@ Page Title="اطلاعات پته گمرکی" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Import Namespace="PoliceServer.Models" %>
<%@ Import Namespace="WebGrease.Css.Extensions" %>
<%@ Import Namespace="PoliceServer.Utilities" %>
<%@ Import Namespace="PoliceServer.CustomsValueDeclarationInformation" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        string error = (string)ViewData["infoException"];
        lblError.Visible = false;
        if (error != null)
        {
            lblError.Text = error;
            lblError.Visible = true;
        }
        Patte patte = (Patte)ViewData["patte"];
        List<customsValueDeclaration> valueDeclarations = (List<customsValueDeclaration>)ViewData["ValueDeclaration"];
        List<ConfirmedPatte> history = (List<ConfirmedPatte>)ViewData["History"];
        if (patte == null)
        {
            Session["Information"] = "اطلاعات دریافتی برای این پته نامعتبر است.";
            Session["Type"] = "Error";
            Response.Redirect(ResolveUrl("~/Default/MessagePage"));
        }
        else
        {
            FillTemplate(patte, valueDeclarations);

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

    private void FillTemplate(Patte pate, List<customsValueDeclaration> valueDeclarations)
    {
        string[] splited = pate.KotajNos.Split('*');
        for (int i = 0; i < splited.Length; i++)
        {
            if (splited[i].Contains("sa"))
                splited[i] = splited[i].Replace("sa", "") + "(پروانه صادراتی)";
        }
        splited.ForEach(serial => KootajNo.Text += serial + " ");
        lblMabda.Text = pate.CityOfOrigin;

        lblMaghsad.Text = pate.IsToUrbanWarehouse ? pate.DestinationName : pate.DestinationCity;
        if (!pate.IsToUrbanWarehouse)
        {
            lblDestinationCity.Visible = false;
            lblDestinationAddress.Visible = false;
            lblDestinationStockBossInfo.Visible = false;
            DestionationCityFarsi.Visible = false;
            DestionationAddressFarsi.Visible = false;
            DestionationStockBossInfoFarsi.Visible = false;
        }
        lblDestinationCity.Text = pate.DestinationState + "-" + pate.DestinationCity;
        lblDestinationAddress.Text = pate.DestinationAddress;
        lblDestinationStockBossInfo.Text = pate.DestinationBossName + "- تلفن: " + pate.DestinationBossPhoneNo;
        
        lblPatteSerial.Text = pate.PatteSerial;

        //CommodityDescription.DataSource = patte.Containers.ToList()[0].Commoditys;
        //CommodityDescription.DataBind();
        if (valueDeclarations != null && valueDeclarations.Count > 0)
        {
            valueDeclarationGridVeiw.DataSource = valueDeclarations;
            valueDeclarationGridVeiw.DataBind();
        }
        else
        {
            commodityTableHeader.Text = "";
            CommodityDescriptionGridView.DataSource = pate.Containers.ToList()[0].Commoditys;
            CommodityDescriptionGridView.DataBind();
        }

        pate.Containers.ForEach(con => ContainerNo.Text += con.ContainerNumber);
        pate.Containers.ForEach(con => ContainerType.Text += con.ContainerType);
        IssuanceDate.Text = CommonUtilities.DateConverterMiladiToHijri(pate.issuanceDate) + "  " + pate.issuanceDate.ToLongTimeString();
        DriverName.Text = pate.Driver.GetFullName();
        DriverNatinalCode.Text = pate.Driver.NationalCode;

        if (pate.ForeignPlaque.IsNullOrWhiteSpace())
        {
            txtCityCode.Text = pate.PlaqueCityCode;
            txtAlphabet.Text = pate.PlaqueMiddleCharacter;
            txtTwoLeft.Text = pate.PlaqueLeftTwoDigits;
            txtThree.Text = pate.PalqueMiddleThreeDigits;
        }
        else
        {
            iranianPlaque.Visible = false;
            lblForeignPlaque.Visible = true;
            lblForeignPlaque.Text = pate.ForeignPlaque;
        }
        patteDbId.Text = pate.Id.ToString();
        lblPureWeight.Text = pate.Weight.ToString();

        string containerType = pate.Containers.ToList()[0].ContainerType;
        if (containerType.Contains("20"))
            lblWeight.Text = (pate.Weight + 2000).ToString();
        else if (containerType.Contains("40"))
        {
            lblWeight.Text = (pate.Weight + 4000).ToString();
        }
        else
        {
            lblWeight.Text = pate.Weight.ToString();
        }

    }

    private string getPateDBID()
    {
        return patteDbId.Text;
    }

    private string getPateSerial()
    {
        return lblPatteSerial.Text;
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
    <asp:Panel ID="pnlErrorMain" HorizontalAlign="Center" runat="server">
        <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#EE1111"
            Text="ErrorMessage" Visible="False" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <br />

    <div class="row col-md-12">
        <form method="post">
            <input type="hidden" id="parvaneSerial" name="parvaneSerial" value="<%= KootajNo.Text %>">
            <input type="submit" name="action:showParvane" value="مشاهده پروانه"  class="btn btn-file col-md-2 col-md-offset-3" />
            <input type="submit" name="action:ShowValueDeclearation" value="مشاهده اظهارنامه ارزش" style="margin-right: 2px; margin-left: 2px" class="btn btn-file col-md-2" />
            <input type="hidden" id="driverNationalID" name="driverNationalID" value="<%= DriverNatinalCode.Text %>">
            <input type="submit" name="action:showBillOfLading" value="مشاهده بارنامه حامل" class="btn btn-file col-md-2" />
        </form>
    </div>
    <br />
    <div class="col-md-12 row">
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">سریال پته</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblPatteSerial" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">تاریخ صدور پته</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="IssuanceDate" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">سریال پروانه</asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="KootajNo" CssClass=" form-control" />
            </div>
        </div>
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">نوع کانتینر</asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="ContainerType" CssClass="form-control" />
            </div>
        </div>
         <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">شماره کانتینر</asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="ContainerNo" CssClass=" form-control" />
            </div>
        </div>
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">وزن کالا و کانتینر</asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="lblWeight" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">وزن بار </asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="lblPureWeight" CssClass="form-control" />
            </div>
        </div>
        

        
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">نام راننده</asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="DriverName" CssClass=" form-control" />
            </div>
        </div>
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">کدملی راننده</asp:Label>
            <div class="col-md-8">
                <asp:Label type="text" runat="server" ID="DriverNatinalCode" CssClass=" form-control" />
            </div>
        </div>
        
        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">گمرک مبدا</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblMabda" CssClass="form-control" />
            </div>
        </div>

        <div class="form-group col-md-4">
            <asp:Label runat="server" CssClass="col-md-4 control-label">مقصد</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblMaghsad" CssClass="form-control" />
            </div>
        </div>
        
        <div class="form-group col-md-4">
            <asp:Label runat="server" ID="DestionationCityFarsi" CssClass="col-md-4 control-label">شهرستان محل تخلیه</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblDestinationCity" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group col-md-8">
            <asp:Label runat="server" ID="DestionationAddressFarsi" CssClass="col-md-2 control-label">آدرس محل تخلیه</asp:Label>
            <div class="col-md-10">
                <asp:Label runat="server" ID="lblDestinationAddress" CssClass="form-control" />
            </div>
        </div>
        
        <div class="form-group col-md-4">
            <asp:Label runat="server" ID="DestionationStockBossInfoFarsi" CssClass="col-md-4 control-label">اطلاعات انباردار</asp:Label>
            <div class="col-md-8">
                <asp:Label runat="server" ID="lblDestinationStockBossInfo" CssClass="form-control" />
            </div>
        </div>
        

        
        
        
        <asp:Label runat="server" Visible="False" ID="patteDbId"></asp:Label>

    </div>

    <div class="col-md-12 col-lg-12 col-sm-12">
        <div class="col-md-12" style="text-align: right">
            <asp:Label ID="commodityTableHeader" runat="server" Text="اطلاعات کالا به انضمام اظهارنامه ارزش برای این حامل" Font-Size="20" />
        </div>
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
                <asp:TemplateField HeaderText="کد کشور">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<customsValueDeclaration>(Container.DataItem).countryCode%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
        </asp:GridView>
        <asp:GridView runat="server" ID="CommodityDescriptionGridView" CssClass="table table-responsive rtl ui-corner-all text-center" HorizontalAlign="Center" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="شناسه کالا">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).CommodityHsCode %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="شرح کالا">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).CommodityTariffDescription %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="تعداد">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).CommodityItemQuantity %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="نوع بسته بندی">
                    <ItemTemplate>
                        <%# CommonUtilities.GetItemObject<Commodity>(Container.DataItem).PackageType %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
        </asp:GridView>
    </div>

    <div class="col-md-12" style="text-align: left">
        <asp:Label runat="server" CssClass="col-md-1 control-label">شماره پلاک</asp:Label>
        <asp:Panel ID="iranianPlaque" CssClass="col-md-5" HorizontalAlign="right" runat="server">
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
        </asp:Panel>
        <div id="foreignPlaque" class="col-md-5" align="right">
            <asp:Label type="text" runat="server" ID="lblForeignPlaque" Visible="False" CssClass=" form-control" />
        </div>
        <div class="col-md-6" style="text-align: right">
            <asp:Label ID="lblHistory" CssClass="col-md-12 control-label" runat="server" Text="سوابق تایید پته" />
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
    </div>

    <br />
    <br />
    <br />


    <div class="form-group col-md-12">
        <div class="form-group col-md-12">
            <form method="post">
                <input type="hidden" id="pasgahID" name="pasgahID" value="<%=Session["PasgahID"].ToString() %>">
                <input type="hidden" id="patteID" name="patteID" value="<%= getPateDBID() %>">
                <input type="hidden" id="pateSerial" name="pateSerial" value="<%= getPateSerial() %>">
                <input type="text" id="pateSerialFromBarcodeReader" name="pateSerialFromBarcodeReader" placeholder="برای تایید میتوانید با خواندن دوباره بارکد پته این پته را تایید کنید" value="" class="col-md-4 col-md-offset-8" />
                <input type="submit" name="action:Confirm" value="تایید عبور پته از مرکز" class="btn btn-success btn-lg col-md-4 col-md-offset-8" />
            </form>
        </div>
    </div>
</asp:Content>

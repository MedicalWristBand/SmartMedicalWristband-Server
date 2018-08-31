<%@ Page Title="اطلاعات پروانه" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Import Namespace="PoliceServer.ParvaneBita" %>
<%@ Import Namespace="PoliceServer.Utilities" %>
<%@ Import Namespace="PoliceServer.Enums" %>
<%@ Import Namespace="Microsoft.Ajax.Utilities" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        $(function () {
            $("#Accordion").accordion({ active: false, collapsible: true });

        })
    </script>
</asp:Content>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        var declaration = (customsDeclaration)ViewData["CustomsDeclaration"];
        if (declaration == null)
        {
            Session["Information"] = "دریافت نامعتبر اطلاعات پروانه گمرکی";
            Session["Type"] = "Error";
            Response.Redirect(ResolveUrl("~/Default/MessagePage"));
        }
        else
        {
            FillTemplate(declaration);
        }
    }

    private void FillTemplate(customsDeclaration declaration)
    {
        lblDeclarationStatus.Text = getDeclarationStatus(declaration.declarationStatus);
        lblLastOperationOnDeclaration.Text = declaration.lastOperationOnDeclaration;
        lblDeclarationTypeCode.Text = getDeclarationTypeCode(declaration.declarationTypeCode);
        lblCustomsDeclarationSerialNumber.Text = declaration.customsDeclarationSerialNumber;
        lblCustomsDeclarationRegistrationDate.Text = getTimeFormat(declaration.customsDeclarationRegistrationDate);
        lblFinancialReceiptDate.Text = getTimeFormat(declaration.financialReceiptDate);


        lblDeclarantFullName.Text = declaration.declarantFullName;
        lblDeclarantNationalID.Text = declaration.declarantNationalID;
        lblDeclarantAddress.Text = declaration.declarantAddress;


        lblConsigneeNationalID.Text = declaration.consigneeNationalID;
        lblConsigneeFullName.Text = declaration.consigneeFullName;
        lblConsigneeAddress.Text = declaration.consigneeAddress;


        lblCommodityItemQuantity.Text = declaration.commodityItemQuantity.ToString();
        lblTotalPackagesCount.Text = declaration.totalPackagesCount.ToString();
        lblManifestRegistrationNumber.Text = declaration.manifestRegistrationNumber;
        lblLocationOfGoods.Text = declaration.locationOfGoods;
        if (!declaration.entranceCustomsName.IsNullOrWhiteSpace())
        {
            lblEntranceCustomsName.Text = EnumHelper.ToEnumString((CustomsCode)Int32.Parse(declaration.entranceCustomsName));
        }
        lblEntranceCustomsCode.Text = EnumHelper.ToEnumString((CustomsCode)Int32.Parse(declaration.entranceCustomsCode));

        lblCurrencyTypeCode.Text = declaration.currencyTypeCode;
        lblCurrencyRate.Text = declaration.currencyRate.ToString();
        lblTotalCurrencyValue.Text = declaration.totalCurrencyValue.ToString();
        lblTotalIRRValue.Text = declaration.totalIRRValue.ToString();
        lblInternationalShippingAgreementType.Text = declaration.internationalShippingAgreementType;
        lblTotalDutyValue.Text = declaration.totalDutyValue.ToString();

        //کالا
        Commodities.DataSource = declaration.declaredCommodityInformation;
        Commodities.DataBind();

        lblBankCode.Text = declaration.bankCode;
        lblBankBranchName.Text = declaration.bankBranchName;
        lblBankBranchCode.Text = declaration.bankBranchCode;
        lblBankName.Text = declaration.bankName;
        lblFinancialDocumentNumber.Text = declaration.financialDocumentNumber;
        lblNatureOfTransaction.Text = declaration.natureOfTransaction;
        lblBankTellerNationalID.Text = declaration.bankTellerNationalID;
        lblBankAddress.Text = declaration.bankAddress;

        lblOriginCountryCode.Text = declaration.originCountryCode;
        lblexportCountryCode.Text = declaration.exportCountryCode;
        lblDestinationCountryCode.Text = declaration.destinationCountryCode;
        lblCarrierIdentity.Text = declaration.carrierIdentity;
        lblBorderModeOfTransport.Text = declaration.borderModeOfTransport;
        lblInlandModeOfTransport.Text = declaration.inlandModeOfTransport;
    }

    private string getTimeFormat(DateTime time)
    {
        if (time.Year<1900)
        {
            return "تاریخ ثبت نشده است";
        }
        return PoliceServer.Utilities.CommonUtilities.DateConverterMiladiToHijri(time) + " " + time.Hour + ":" + ((time.Minute) > 10 ? time.Minute.ToString() : "0" + time.Minute);
    }

    private string getDeclarationStatus(string status)
    {
        if (status.IsNullOrWhiteSpace())
            return status;
        if (status.Equals("A"))
            return "پروانه شده";
        if (status.Equals("R"))
            return "اظهارنامه پروانه نشده";
        if (status.Equals("S"))
            return "اظهارنامه کوتاژ نشده";
        return status;
    }

    private string getDeclarationTypeCode(string status)
    {
        if (status.IsNullOrWhiteSpace())
            return "";
        if (status.Contains("وق"))
            return "واردات قطعی";
        if (status.Contains("مر"))
            return "مرجوعی";
        if (status.Equals("وم"))
            return "ورود موقت";
        if (status.Equals("پته"))
            return "پته مسافری";
        return status;
    }

</script>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal col-md-12">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblDeclarationStatus" CssClass="col-md-2 control-label">وضعیت اظهارنامه</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblDeclarationStatus" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="lblLastOperationOnDeclaration" CssClass="col-md-2 control-label">وضعیت جاری پروانه</asp:Label>
            <div class="col-md-4">
                <asp:Label type="text" runat="server" ID="lblLastOperationOnDeclaration" CssClass=" form-control" />
            </div>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblDeclarationTypeCode" CssClass=" form-control" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblCustomsDeclarationSerialNumber" CssClass="col-md-2 control-label">شماره‌ سریال پروانه گمرکی</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblCustomsDeclarationSerialNumber" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="lblCustomsDeclarationRegistrationDate" CssClass="col-md-2 control-label">تاریخ کوتاژ</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblCustomsDeclarationRegistrationDate" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="lblFinancialReceiptDate" CssClass="col-md-2 control-label">تاریخ صدور پروانه الکترونیک</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblFinancialReceiptDate" CssClass=" form-control" />
            </div>
        </div>

        <div class="well">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblDeclarantFullName" CssClass="col-md-2 control-label">نام و نام خانوادگی اظهارکننده</asp:Label>
                <div class="col-md-6">
                    <asp:Label type="text" runat="server" ID="lblDeclarantFullName" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblDeclarantNationalID" CssClass="col-md-2 control-label">کد ملی اظهارکننده</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblDeclarantNationalID" CssClass=" form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblDeclarantAddress" CssClass="col-md-2 control-label">آدرس اظهارکننده</asp:Label>
                <div class="col-md-10">
                    <asp:Label type="text" runat="server" ID="lblDeclarantAddress" CssClass=" form-control" />
                </div>
            </div>
        </div>

        <div class="well">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblConsigneeFullName" CssClass="col-md-2 control-label">نام و نام خانوادگی صاحب کالا</asp:Label>
                <div class="col-md-6">
                    <asp:Label type="text" runat="server" ID="lblConsigneeFullName" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblConsigneeNationalID" CssClass="col-md-2 control-label">کد ملی صاحب کالا</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblConsigneeNationalID" CssClass=" form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblConsigneeAddress" CssClass="col-md-2 control-label">آدرس صاحب کالا</asp:Label>
                <div class="col-md-10">
                    <asp:Label type="text" runat="server" ID="lblConsigneeAddress" CssClass=" form-control" />
                </div>
            </div>
        </div>

        <div class="well">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblCommodityItemQuantity" CssClass="col-md-2 control-label">تعداد قلم کالا</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblCommodityItemQuantity" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblTotalPackagesCount" CssClass="col-md-2 control-label">تعداد کل بسته</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblTotalPackagesCount" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblManifestRegistrationNumber" CssClass="col-md-2 control-label">شماره ثبت مانیفست</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblManifestRegistrationNumber" CssClass=" form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblLocationOfGoods" CssClass="col-md-2 control-label">گمرک خروج</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblLocationOfGoods" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblEntranceCustomsCode" CssClass="col-md-2 control-label">گمرک کارشناسی</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblEntranceCustomsCode" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblEntranceCustomsName" CssClass="col-md-2 control-label">نام گمرک ورودی</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblEntranceCustomsName" CssClass=" form-control" />
                </div>
            </div>
        </div>

        <div class="well">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblCurrencyTypeCode" CssClass="col-md-2 control-label">کد نوع ارز</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblCurrencyTypeCode" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblCurrencyRate" CssClass="col-md-2 control-label">نرخ ارز</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblCurrencyRate" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblTotalCurrencyValue" CssClass="col-md-2 control-label">ارزش ارزی کل</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblTotalCurrencyValue" CssClass=" form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblTotalIRRValue" CssClass="col-md-2 control-label">ارزش ریالی کل</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblTotalIRRValue" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblInternationalShippingAgreementType" CssClass="col-md-2 control-label">شرایط تحویل بین المللی</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblInternationalShippingAgreementType" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblTotalDutyValue" CssClass="col-md-2 control-label">جمع عوارض دریافتی</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblTotalDutyValue" CssClass=" form-control" />
                </div>
            </div>
        </div>


        <h3>اطلاعات کالا</h3>
        <div class="form-group">
            <div class=" col-md-12">
                <asp:GridView runat="server" ID="Commodities" CssClass="table table-responsive ui-corner-all text-center" HorizontalAlign="Center" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="شرح کالا">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).commodityDescription %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تعرفه">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).commodityHSCode %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ماخذ">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).taxesRate %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تعداد بسته">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).packageCount %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وزن ناخالص">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).grossWeightInKg %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وزن خالص">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).netWeightInKg %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ارزش قلمي">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).commodityItemCurrencyValue %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شماره قبض انبار">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<declaredCommodityInformation>(Container.DataItem).identificationOfWarehouse %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
        <br />

        <div id="Accordion" class="ui-accordion-header" clientidmode="Static">
            <caption>
                <h3 class="text-center">جزییات بیشتر...</h3>
                <div id="detailfield2">
                    <div class="well ui-state-default">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="lblBankCode" CssClass="col-md-2 control-label">کد بانک</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblBankCode" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblBankBranchName" CssClass="col-md-2 control-label">نام شعبه بانک</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblBankBranchName" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblBankBranchCode" CssClass="col-md-2 control-label">کد شعبه بانک</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblBankBranchCode" CssClass=" form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="lblBankName" CssClass="col-md-2 control-label">نام بانک</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblBankName" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblFinancialDocumentNumber" CssClass="col-md-2 control-label">شماره اسناد اعتباری</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblFinancialDocumentNumber" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblNatureOfTransaction" CssClass="col-md-2 control-label">نوع معامله</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblNatureOfTransaction" CssClass=" form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="lblBankTellerNationalID" CssClass="col-md-2 control-label">شماره ملی کارمند بانک</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblBankTellerNationalID" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblBankAddress" CssClass="col-md-2 control-label">آدرس بانک</asp:Label>
                            <div class="col-md-6">
                                <asp:Label type="text" runat="server" ID="lblBankAddress" CssClass=" form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="well ui-state-default">
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="lblOriginCountryCode" CssClass="col-md-2 control-label">کد کشور مبدا</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblOriginCountryCode" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblexportCountryCode" CssClass="col-md-2 control-label">کد کشور صادرکننده</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblexportCountryCode" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblDestinationCountryCode" CssClass="col-md-2 control-label">کد کشور مقصد</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblDestinationCountryCode" CssClass=" form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="lblCarrierIdentity" CssClass="col-md-2 control-label">هویت وسیله حمل</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblCarrierIdentity" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblBorderModeOfTransport" CssClass="col-md-2 control-label">نحوه حمل در مرز</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblBorderModeOfTransport" CssClass=" form-control" />
                            </div>
                            <asp:Label runat="server" AssociatedControlID="lblInlandModeOfTransport" CssClass="col-md-2 control-label">نحوه حمل درون مرزی</asp:Label>
                            <div class="col-md-2">
                                <asp:Label type="text" runat="server" ID="lblInlandModeOfTransport" CssClass=" form-control" />
                            </div>
                        </div>
                    </div>
                </div>
            </caption>
        </div> 
        <br />
    </div>
</asp:Content>
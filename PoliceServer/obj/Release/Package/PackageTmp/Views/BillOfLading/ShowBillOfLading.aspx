<%@ Page Title="اطلاعات بارنامه" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Import Namespace="PoliceServer.BillOfLadingInformation" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        BillOfLading bol = (BillOfLading)ViewData["BOL"];
        if (bol == null)
        {
            Session["Information"] = "دریافت نامعتبر اطلاعات بارنامه";
            Session["Type"] = "Error";
            Response.Redirect(ResolveUrl("~/Default/MessagePage"));
        }
        else
        {
            FillTemplate(bol);
        }
    }

    private void FillTemplate(BillOfLading bol)
    {
        lblBillOfLadingNumber.Text = bol.billOfLadingNumber;
        lblIssueDate.Text = bol.issueDate;
        lblOriginCityName.Text = bol.originCityName;
        
        lblDestinationCityName.Text = bol.destinationCityName;
        lblCommoditySenderName.Text = bol.commoditySenderName;
        lblCommodityReceiverName.Text = bol.commodityReceiverName;

        lblCustomsDeclarationSerialNumber.Text = bol.customsDeclarationSerialNumber;
        lblCustomsDeclarationRegistrationDate.Text = bol.customsDeclarationRegistrationDate;
        lblShippingCompanyName.Text = bol.shippingCompanyName;

        lblTraceCode.Text = bol.traceCode;
        lblCommodityDescription.Text = bol.commodityDescription;

        lblDriverNationalID.Text = bol.driverInformation.nationalID;
        lblDriverFirstName.Text = bol.driverInformation.firstName;
        lblDriverLastName.Text = bol.driverInformation.lastName;
        lblDriverHealthCardNumber.Text = bol.driverInformation.driverHealthCardNumber;
        lblDriverLicenseNumber.Text = bol.driverInformation.driverLicenseNumber;
        lblDriverCellPhoneNumber.Text = bol.driverInformation.cellPhoneNumber;


        txtCityCode.Text = bol.freighterInformation.plateSerialNumber;
        txtPlateNo.Text = bol.freighterInformation.plateNumber.Substring(2, 1);
        txtTwoLeft.Text = bol.freighterInformation.plateNumber.Substring(0, 2);
        txtThree.Text = bol.freighterInformation.plateNumber.Substring(3, 3);
        loaderLinkType.Text = bol.freighterInformation.loaderLinkType;
        vehicleCardNumber.Text = bol.freighterInformation.vehicleCardNumber;
        vin.Text = bol.freighterInformation.vin;

    }
    

</script>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal col-md-12">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblBillOfLadingNumber" CssClass="col-md-2 control-label">شماره بارنامه</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblBillOfLadingNumber" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="lblOriginCityName" CssClass="col-md-2 control-label">نام شهر مبدا</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblOriginCityName" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="lblDestinationCityName" CssClass="col-md-2 control-label">نام شهر مقصد</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblDestinationCityName" CssClass=" form-control" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="lblIssueDate" CssClass="col-md-2 control-label">تاریخ صدور بارنامه</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblIssueDate" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="lblCommoditySenderName" CssClass="col-md-2 control-label">نام فرستنده كالا</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblCommoditySenderName" CssClass=" form-control" />
            </div>
            <asp:Label runat="server" AssociatedControlID="lblCommodityReceiverName" CssClass="col-md-2 control-label">نام گيرنده كالا</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" runat="server" ID="lblCommodityReceiverName" CssClass=" form-control" />
            </div>
        </div>

        <div class="well">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblCustomsDeclarationSerialNumber" CssClass="col-md-2 control-label">شماره‌ سریال اظهارنامه گمرکی</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblCustomsDeclarationSerialNumber" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblCustomsDeclarationRegistrationDate" CssClass="col-md-2 control-label">تاریخ کوتاژ</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblCustomsDeclarationRegistrationDate" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblShippingCompanyName" CssClass="col-md-2 control-label">نام شركت حمل</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblShippingCompanyName" CssClass=" form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblTraceCode" CssClass="col-md-2 control-label">كد رهگيری</asp:Label>
                <div class="col-md-4">
                    <asp:Label type="text" runat="server" ID="lblTraceCode" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblCommodityDescription" CssClass="col-md-2 control-label">شرح کالا</asp:Label>
                <div class="col-md-4">
                    <asp:Label type="text" runat="server" ID="lblCommodityDescription" CssClass=" form-control" />
                </div>
            </div>
        </div>

        <div class="well">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblDriverNationalID" CssClass="col-md-2 control-label">کدملی راننده</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblDriverNationalID" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblDriverFirstName" CssClass="col-md-2 control-label">نام راننده</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblDriverFirstName" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblDriverLastName" CssClass="col-md-2 control-label">نام خانوادگی راننده</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblDriverLastName" CssClass=" form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="lblDriverHealthCardNumber" CssClass="col-md-2 control-label">شماره کارت سلامت راننده</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblDriverHealthCardNumber" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblDriverLicenseNumber" CssClass="col-md-2 control-label">شماره گواهینامه رانندگی</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblDriverLicenseNumber" CssClass=" form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="lblDriverCellPhoneNumber" CssClass="col-md-2 control-label">شماره تلفن همراه</asp:Label>
                <div class="col-md-2">
                    <asp:Label type="text" runat="server" ID="lblDriverCellPhoneNumber" CssClass=" form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="vehicleCardNumber" CssClass="col-md-2 control-label">شماره کارت خودرو</asp:Label>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="vehicleCardNumber" CssClass="form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="loaderLinkType" CssClass="col-md-2 control-label">نوع کشنده خودرو</asp:Label>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="loaderLinkType" CssClass="form-control" />
                </div>
                <asp:Label runat="server" AssociatedControlID="vin" CssClass="col-md-2 control-label">شماره شناسایی خودرو</asp:Label>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="vin" CssClass="form-control" />
                </div>
            </div>
        </div>


        <asp:Label runat="server" CssClass="col-md-1 control-label">شماره پلاک</asp:Label>
        <asp:Panel ID="iranianPlaque" CssClass="col-md-11" HorizontalAlign="right" runat="server">
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
                                        <td style="font-size: 20px;" width="50" valign="middle">
                                            <asp:Label ID="txtCityCode" Style="font-size: 18px; width: 30px; text-align: center; vertical-align: bottom" runat="server"></asp:Label>
                                        </td>
                                        <td style="line-height: 14px; font-size: 13px; width: 55px;" valign="middle">
                                            <asp:Label ID="txtThree" MaxLength="3" name="txtP2" Style="font-size: 18px; height: 25px; width: 40px; text-align: center; padding-top: 5px" runat="server"></asp:Label>
                                        </td>
                                        <td style="line-height: 14px; font-size: 20px; width: 45px" valign="middle">
                                            <asp:Label ID="txtPlateNo" runat="server" Width="40px" />
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
    </div>
</asp:Content>



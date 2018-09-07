<%@ Page Title="تولید بارکد" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="PoliceServer.Models" %>
<%@ Import Namespace="PoliceServer.Repository" %>
<%@ Import Namespace="QRCoder" %>

<script runat="server">

    protected void Page_Load()
    {
        userDataPanel.Visible = false;

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
            if (Session["PatientId"] != null)
            {
                Patient patient = PatientRepository.GetInstance().FindById(int.Parse(Session["PatientId"].ToString()));
                lblPatientName.Text = patient.Name;
                lblPatientLastName.Text = patient.Family;
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(patient.Status, QRCodeGenerator.ECCLevel.M);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                long tick = DateTime.Now.Ticks;

//                string outputFileName = Server.MapPath(@"~/img/barcode/"+Session["PatientId"]+".bmp");
//                using (MemoryStream memory = new MemoryStream())
//                {
//                    using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
//                    {
//                        qrCodeImage.Save(memory, ImageFormat.Bmp);
//                        byte[] bytes = memory.ToArray();
//                        fs.Write(bytes, 0, bytes.Length);
//                    }
//                }

                MemoryStream ms = new MemoryStream();
                qrCodeImage.Save(ms, ImageFormat.Bmp);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                imgCtrl.Src = "data:image/bmp;base64," + base64Data;

//                imgqrCode.ImageUrl =outputFileName;
                pnlPatient.Visible = true;
            }
            Session["PatientId"] = null;

        }

        else
        {
            Session["PatientId"] = null;
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
            <form method="post" action="~/generatebarcode/generatebarcode">
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-3 control-label"> کد بیمار</asp:Label>
                    <div class="col-md-5">
                        <input type="text" id="patientCode" class="col-md-12" name="patientCode" required="" />
                    </div>
                    <div class="col-md-4">
                        <input type="submit" value="جستجو" name="action:GenerateBarcode" class="btn btn-success col-md-12" />
                    </div>
                </div>
            </form>
        </div>

        <asp:Panel ID="userDataPanel" CssClass="col-md-4 col-md-offset-4" runat="server" Visible="False">

            <br />
            <br />
        </asp:Panel>
        
        <asp:Panel class="form-group col-md-8 col-md-offset-3" runat="server" ID="pnlPatient" Visible="False">
            <asp:Label runat="server" CssClass="col-md-2 control-label">نام:</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" ID="lblPatientName" class="col-md-2"  runat="server"/>
            </div>
            <asp:Label runat="server" CssClass="col-md-3 control-label">نام خانوادگی</asp:Label>
            <div class="col-md-2">
                <asp:Label type="text" ID="lblPatientLastName" class="col-md-3"  runat="server"/>
            </div>
            <br/>
            <br/>
            <asp:Image runat="server" ID="imgqrCode"/>
            <img runat="server" id="imgCtrl" width="400" height="400" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>






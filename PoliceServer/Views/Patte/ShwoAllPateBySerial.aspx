<%@ Page Title="مشاهده همه حامل های یک پروانه" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/PoliceSite.Master" %>

<%@ Import Namespace="PoliceServer.Utilities" %>
<%@ Import Namespace="PoliceServer.Models" %>
<%@ Import Namespace="WebGrease.Css.Extensions" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
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
        List<Patte> pateList = (List<Patte>)ViewData["ListPate"];
        grdAllPate.DataSource = pateList;
        grdAllPate.DataBind();

        showPateCommodityDetail();
    }

    public string getContainersType(List<Container> containers)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < containers.Count; i++)
        {
            stringBuilder.Append(containers[i].ContainerType).Append('\n');
        }
        return stringBuilder.ToString();
    }

    public string getContainersNumber(List<Container> containers)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < containers.Count; i++)
        {
            stringBuilder.Append(containers[i].ContainerNumber).Append('\n');
        }
        return stringBuilder.ToString();
    }

    public string getGrossWeight(List<Container> containers, double netWeight)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < containers.Count; i++)
        {
            double grossWeight;
            if (containers[i].ContainerType.Contains("20"))
                grossWeight = netWeight + 2000;
            else if (containers[i].ContainerType.Contains("40"))
            {
                grossWeight = netWeight + 4000;
            }
            else
            {
                grossWeight = netWeight;
            }
            stringBuilder.Append(grossWeight).Append('\n');
        }
        return stringBuilder.ToString();
    }

    private void showPateCommodityDetail()
    {
        Patte pate = (Patte) ViewData["pateDetailSelected"];
        if (pate != null)
        {
            List<Commodity> commodities = new List<Commodity>();
            pate.Containers.ForEach(con=> con.Commoditys.ForEach(com=> commodities.Add(com)));
            CommodityDescription.DataSource = commodities;
            CommodityDescription.DataBind();
        }
    }
</script>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel HorizontalAlign="Center" Style="text-align: center" runat="server">
        <asp:Panel ID="pnlErrorMain" runat="server">
            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#EE1111"
                Text="ErrorMessage" Visible="False" ClientIDMode="Static"></asp:Label>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="pnlDescription" Visible="True" runat="server">
            <span class="ui-state-active ui-corner-all" style="padding: 10px">برای مشاهده اطلاعات همه حامل های یک پروانه شماره سریال پروانه را وارد کنید.</span>
        </asp:Panel>
        <br />
        <form method="post">
            <input type="text" id="parvaneSerial" name="parvaneSerial" value="" />
            <input type="submit" name="action:searchSerial" value="جستجو" />
        </form>

    </asp:Panel>
    <br/>
    <br/>
    <br/>
    <div class="form-horizontal col-md-12">
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView runat="server" ID="grdAllPate" CssClass="table table-responsive rtl ui-corner-all text-center" HorizontalAlign="Center" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="شماره پته">
                            <ItemTemplate>
                                <asp:Label ID="lblPatteSerial" runat="server">
                                    <%# CommonUtilities.GetItemObject<Patte>(Container.DataItem).PatteSerial %>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="مبدا">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Patte>(Container.DataItem).CityOfOrigin %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تاریخ صدور">
                            <ItemTemplate>
                                <%# CommonUtilities.DateConverterMiladiToHijri(CommonUtilities.GetItemObject<Patte>(Container.DataItem).issuanceDate) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="نوع کانتینر">
                            <ItemTemplate>
                                <%# getContainersType(CommonUtilities.GetItemObject<Patte>(Container.DataItem).Containers.ToList()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="شماره کانتینر">
                            <ItemTemplate>
                                <%# getContainersNumber(CommonUtilities.GetItemObject<Patte>(Container.DataItem).Containers.ToList()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وزن بار">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Patte>(Container.DataItem).Weight %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وزن بار و کانتینر">
                            <ItemTemplate>
                                <%# getGrossWeight(CommonUtilities.GetItemObject<Patte>(Container.DataItem).Containers.ToList(), CommonUtilities.GetItemObject<Patte>(Container.DataItem).Weight) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="نام راننده">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Patte>(Container.DataItem).Driver.Name %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="کدملی راننده">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Patte>(Container.DataItem).Driver.NationalCode %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="پلاک حامل">
                            <ItemTemplate>
                                <%# CommonUtilities.GetItemObject<Patte>(Container.DataItem).getPlaque() %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="جزئیات کالا">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <form method="post">
                                    <input type="hidden" id="pateID" name="pateID" value="<%# CommonUtilities.GetItemObject<Patte>(Container.DataItem).PatteSerial %>">
                                    <input type="submit" name="action:pateDatail" value="مشاهده" class="btn-success" />
                                </form>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
                </asp:GridView>
                <br/>
                <br/>   
                <div class="col-md-6 col-md-offset-3">
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
                        </Columns>
                        <HeaderStyle BackColor="#2E5771" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                        <RowStyle BackColor="#EFF3FB" VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

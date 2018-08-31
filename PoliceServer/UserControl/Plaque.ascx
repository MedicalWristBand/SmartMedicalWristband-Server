<%@ Control Language="C#" AutoEventWireup="true"  CodeBehind="Plaque.ascx.cs" Inherits="PoliceServer.UserControl.Plaque" %>
<style type="text/css">
    .style1
    {
        font-size: large;
        font-weight: bold;
    }

    #txtP1
    {
        width: 25px;
    }

    #Text1
    {
        width: 26px;
    }

    #txtP2
    {
        width: 32px;
    }

    #txtP3
    {
        width: 28px;
    }
</style>
<script type="text/javascript">
    $(document).ready(function() {
        $('#txtPatteNo').focus();
    });
</script>
<%--<script type="text/javascript" src="/Scripts/others/plaque.js"> </script>--%>
<script type="text/javascript" src="/Scripts/others/onlynumber.js"> </script>
<asp:Panel HorizontalAlign="Center" Style="text-align: center" DefaultButton="btnSubmit" runat="server">
    <asp:Panel ID="pnlErrorMain" runat="server">
        <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#EE1111"
            Text="ErrorMessage" Visible="False" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <br/>
    <br/>
    <asp:Panel ID="pnlDescription" Visible="True" runat="server">
        <span class="ui-state-active ui-corner-all" style="padding: 10px">برای مشاهده اطلاعات یک حامل شماره پلاک آن را وارد نمایید.</span>
    </asp:Panel>
    <br />
    <table align="center">
        <tr>
            <td>شماره پلاک ماشین:</td>
            <td>
                <div dir="rtl">
                    <div style="width: 250px; background-image: url('/Content/Images/pelak.gif')" align="right">
                        <table style="height: 65px; font-size: 9pt; line-height: 15px; font-family: Tahoma; background-repeat: no-repeat;"
                            border="0" dir="rtl">
                            <tbody>
                                <tr>
                                    <td valign="top">
                                        <table border="0" cellpadding="0" cellspacing="0">
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
                                                <tr>
                                                    <td style="font-size: 13px;" align="center" width="50" valign="top">
                                                        <asp:TextBox ID="txtCityCode" MaxLength="2" onkeydown="chec" ClientIDMode="Static" name="txtP3" Style="font-family: Roya; font-size: 18px; height: 25px; width: 30px; text-align: center;" runat="server"></asp:TextBox>
                                                        <br />
                                                        <span style="color: Red; display: none;">*</span>
                                                    </td>
                                                    <td style="line-height: 14px; font-size: 13px; width: 55px; height: 40px" valign="top">
                                                        <asp:TextBox ID="txtThree" ClientIDMode="Static" MaxLength="3" name="txtP2" onkeydown="checkP2(event);" Style="font-family: Roya; font-size: 18px; height: 25px; width: 40px; text-align: center;" runat="server"></asp:TextBox>
                                                        <br />
                                                        <span style="color: Red; display: none;">*</span>
                                                    </td>
                                                    <td style="line-height: 14px; font-size: 13px; width: 45px" valign="top">
                                                        <asp:DropDownList ID="txtAlphabet" ClientIDMode="Static" runat="server" Width="40px" Height="35px">
                                                            <asp:ListItem>ع</asp:ListItem>
                                                            <asp:ListItem>الف</asp:ListItem>
                                                            <asp:ListItem>ب</asp:ListItem>
                                                            <asp:ListItem>ج</asp:ListItem>
                                                            <asp:ListItem>د</asp:ListItem>
                                                            <asp:ListItem>ر</asp:ListItem>
                                                            <asp:ListItem>س</asp:ListItem>
                                                            <asp:ListItem>ص</asp:ListItem>
                                                            <asp:ListItem>ط</asp:ListItem>
                                                            <asp:ListItem>ف</asp:ListItem>
                                                            <asp:ListItem>ق</asp:ListItem>
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>ل</asp:ListItem>
                                                            <asp:ListItem>م</asp:ListItem>
                                                            <asp:ListItem>ن</asp:ListItem>
                                                            <asp:ListItem>و</asp:ListItem>
                                                            <asp:ListItem>ه</asp:ListItem>
                                                            <asp:ListItem>ی</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <br />
                                                        <span style="color: Red; display: none;">*</span>
                                                    </td>
                                                    <td style="line-height: 14px; font-size: 13px;" valign="top">
                                                        <asp:TextBox ID="txtTwoLeft" ClientIDMode="Static" name="txtP1" MaxLength="2" Style="font-family: Roya; font-size: 18px; height: 25px; width: 30px; text-align: center;" onkeydown="checkP1(event);" runat="server"></asp:TextBox>
                                                        <br />
                                                        <span style="color: Red; display: none;">*</span>
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
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>شماره سریال پته:</td>
            <td>
                <asp:TextBox ID="txtPatteNo" ClientIDMode="Static"  Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSubmit" ClientIDMode="Static" runat="server" Font-Names="B Nazanin"
                    CssClass="btn-facebook btn btn-lg" Text="مشاهده اطلاعات"/>
            </td>
        </tr>
    </table>
</asp:Panel>



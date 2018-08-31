<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPermission.ascx.cs" Inherits="PoliceServer.UserControl.uPermission" %>

<style type="text/css">
    td {
        font-size: large;
    }
    
    label {
        padding: 5px;
    }
</style>

<script src="/Scripts/jQui/jquery-ui.min.js"></script>
<link rel="stylesheet" href="/Scripts/jQui/jquery-ui.css" />
<script>
    $(function () {
        $("#Accordion").accordion();
    })
</script>


<table style="text-align: right" class="col-md-12">
    <tr class="col-md-12">
        <td>
            <asp:Label CssClass="col-md-4" runat="server" ID="lblName"></asp:Label></td>
        <td>
            <asp:Label CssClass="col-md-4" runat="server" ID="lblFamily"></asp:Label></td>
        <td>
            <asp:Label CssClass="col-md-4" runat="server" ID="lblNationalCode"></asp:Label></td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr class="col-md-12">
        <td>
            <asp:CheckBox ID="chkSystemAdmin" Enabled="True" Checked="False" runat="server"  Text="مدیر سیستم" />
        </td>
        <td>
            <asp:CheckBox ID="chkDoctor" runat="server" Checked="False" Text="پزشک" />
        </td>
        <td>
            <asp:CheckBox ID="chkNurse" Enabled="True" Checked="False" runat="server" Text="پرستار" />
        </td>
    </tr>

    <tr>
        <td>
            <asp:CheckBox ID="chkStaff" Checked="False" runat="server" Text="سایر کارکنان" />
        </td>
    </tr>
</table>

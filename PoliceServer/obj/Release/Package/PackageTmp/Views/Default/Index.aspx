<%@ Page Title="صفحه نخست" Language="C#" MasterPageFile="~/PoliceSite.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center"><asp:Image runat="server" Width="300" Height="300"  ImageAlign="Middle" ImageUrl="~/Content/images/customs_Logo.png"/></div>
    <div align="center" dir="rtl">
        <h1>
            
            <asp:Label ID="lblHadis" runat="server" ForeColor="Blue" Font-Size="32">
                امام علی علیه السلام: عَرفْتُ اللهَّ سُبحانَهُ بفَسْخِ العَزائمِ و حَلَّ العُقودِ و نَقْض الهِمَمِ.
            </asp:Label>
            <br />
            <asp:Label ID="lblTarjome" runat="server" ForeColor="#339933" Font-Size="16">
                امام علی علیه‌السلام: من خداوند سبحان را در به درهم‌شکستن عزمها و فروریختن تصمیم‌ها و برهم‌خوردن اراده‌ها و خواست‌ها شناختم.
            </asp:Label>
        </h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LoginContent" runat="server">
</asp:Content>

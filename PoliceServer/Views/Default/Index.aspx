<%@ Page Title="صفحه نخست" Language="C#" MasterPageFile="~/PoliceSite.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center"><asp:Image runat="server" Width="300" Height="300"  ImageAlign="Middle" ImageUrl="~/Content/images/main.jpg"/></div>
    <div align="center" dir="rtl">
        <h1>
            
            <asp:Label ID="lblBlue" runat="server" ForeColor="Blue" Font-Size="32">
            </asp:Label>
            <br />
            <asp:Label ID="lblGreen" runat="server" ForeColor="#339933" Font-Size="16">
            </asp:Label>
        </h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LoginContent" runat="server">
</asp:Content>

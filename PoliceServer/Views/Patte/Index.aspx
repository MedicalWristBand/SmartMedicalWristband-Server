<%@ Page Title="دریافت اطلاعات پته" Language="C#" Inherits="System.Web.Mvc.ViewPage" EnableEventValidation="false" AutoEventWireup="true" MasterPageFile="~/PoliceSite.Master" %>
<%@ Register TagPrefix="PoliceServer" TagName="uPlaque" Src="~/UserControl/Plaque.ascx" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <PoliceServer:uPlaque ID="UUPlaque" runat="server"></PoliceServer:uPlaque>
</asp:Content>





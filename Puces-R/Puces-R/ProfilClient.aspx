<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilClient.aspx.cs" Inherits="Puces_R.ProfilClient" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="ProfilClient" Src="~/Controles/ProfilClient.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="ctrMenu" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau">
            <lp:ProfilClient runat="server"/>
        </div>
    </div>
</asp:Content>

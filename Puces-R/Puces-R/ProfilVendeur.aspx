<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilVendeur.aspx.cs" Inherits="Puces_R.ProfilVendeur" %>

<%@ Register TagPrefix="se" TagName="ProfilVendeur" Src="~/Controles/ProfilVendeur.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau">
            <se:ProfilVendeur ID="ProfilClient1" runat="server"/>
        </div>
    </div>
</asp:Content>
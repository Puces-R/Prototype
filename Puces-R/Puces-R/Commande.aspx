<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Commande.aspx.cs" Inherits="Puces_R.Commande" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="ProfilClient" Src="~/Controles/ProfilClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>
<%@ Register TagPrefix="lp" TagName="TablePanier" Src="~/Controles/TablePanier.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" ID="ctrMenu" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Commande.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <lp:ProfilClient ID="ProfilClient1" runat="server"/>
        </div>
        <div class="panneau pnlDroite pnlDetails"> 
            <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" />
        </div>
        <div class="panneau pnlDroite pnlDetails">
            <h2>Produits</h2>
            <lp:TablePanier runat="server" ID="ctrTablePanier" />
        </div>
    </div>
</asp:Content>

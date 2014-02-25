<%@ Page Title="Modification du produit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificationProduits.aspx.cs"
    Inherits="Puces_R.ModificationProduits" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="yc" TagName="FormulaireProduit" Src="~/Controles/FormulaireProduit.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Modification d'un produit
    </h2>
    <div class="rectangleComplet rectangleItem">
        <asp:Image ID="imgProduits" ImageUrl="logo.png" Height="250" runat="server" CssClass="imageProduitADroite" />
        <table class="formulaire"  style="width: 700px;">
            <yc:FormulaireProduit runat="server" ID="ctrProduit" />
        </table>
        <asp:Button ID="btnAjout" Text="Modifier le produit" runat="server" CausesValidation="false" OnClick="modifierProduit" />
        <asp:Button runat="server" ID="btnRetour" Text="Retour" CausesValidation="FALSE"
            PostBackUrl="GestionProduits.aspx" />
    </div>
</asp:Content>

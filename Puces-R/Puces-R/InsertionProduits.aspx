<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertionProduits.aspx.cs"
    Inherits="Puces_R.InsertionProduits" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="FormulaireProduit" Src="~/Controles/FormulaireProduit.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/InsertionProduits.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Insertion d'un nouveau produit</h2>
    <asp:Label ID="lblAvertissement" runat="server" CssClass="sRouge"></asp:Label>
    <div class="rectangleComplet rectangleItem">
        <table class="formulaire" style="width: 700px;">
            <yc:FormulaireProduit runat="server" ID="ctrProduit" />
        </table>
        <asp:Button ID="btnAjout" Text="Ajouter le produit" runat="server" OnClick="validationSaisie" />
    </div>
</asp:Content>

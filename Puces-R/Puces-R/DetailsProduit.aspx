<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetailsProduit.aspx.cs" Inherits="Puces_R.DetailsProduit" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/DetailsProduit.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="pnlGauche">
        <div class="boiteImageProduit">
            <div>
                <asp:Image runat="server" ID="imgProduit" />
            </div>
        </div>
    </div>
    <div class="pnlDroite pnlDetails">
        <table>
            <tr>
                <td>Produit: </td>
                <td><asp:Label ID="lblProduit" runat="server" /></td>
            </tr>
            <tr>
                <td>Catégorie: </td>
                <td><asp:Label ID="lblCategorie" runat="server" /></td>
            </tr>
            <tr>
                <td>Description: </td>
                <td><asp:Label ID="lblDescription" runat="server" CssClass="largeurMaxDetails" /></td>
            </tr>
            <tr>
                <td>Prix demandé: </td>
                <td><asp:Label ID="lblPrixDemande" runat="server" /></td>
            </tr>
            <tr>
                <td>Prix en vente: </td>
                <td><asp:Label ID="lblPrixEnVente" runat="server" /></td>
            </tr>
            <tr>
                <td>Quantité disponible: </td>
                <td><asp:Label ID="lblQuantiteDisponible" runat="server" /></td>
            </tr>
            <tr>
                <td>Date de création: </td>
                <td><asp:Label ID="lblDateCreation" runat="server" /></td>
            </tr>
            <tr>
                <td>Date de mise à jour: </td>
                <td><asp:Label ID="lblDateMiseAJour" runat="server" /></td>
            </tr>
        </table>
    </div>
</asp:Content>

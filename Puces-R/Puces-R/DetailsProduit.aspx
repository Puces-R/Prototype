<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetailsProduit.aspx.cs" Inherits="Puces_R.DetailsProduit" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" ID="ctrMenu" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/DetailsProduit.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <div class="boiteImageProduit">
                <div>
                    <asp:Image runat="server" ID="imgProduit" />
                </div>
            </div>
        </div>
        <div class="panneau pnlDroite pnlDetails">
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
                    <td><asp:Label ID="lblDescription" runat="server" CssClass="description" /></td>
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
                    <td>Disponible: </td>
                    <td><asp:Label ID="lblQuantiteDisponible" runat="server" /></td>
                </tr>
                <tr>
                    <td>Création: </td>
                    <td><asp:Label ID="lblDateCreation" runat="server" /></td>
                </tr>
                <tr>
                    <td>Mise à jour: </td>
                    <td><asp:Label ID="lblDateMiseAJour" runat="server" /></td>
                </tr>
                <tr>
                    <td>Quantité: </td>
                    <td><asp:TextBox runat="server" ID="txtQuantite" CssClass="boiteQuantite" Text="1" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button runat="server" ID="btnAjouterPanier" Text="Ajouter au panier" OnClick="btnAjouterPanier_Click" /></td>
                </tr>
            </table>       
        </div>
    </div>
</asp:Content>

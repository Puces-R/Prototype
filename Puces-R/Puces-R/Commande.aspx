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
        <div class="panneau pnlGauche pnlDetails">
            <h2>Produits</h2>
            <lp:TablePanier runat="server" ID="ctrTablePanier" />
            <div class="lignePointilleHorizontale pleineLargeur"></div>
            <h2>Facture</h2>
            <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" />
        </div>
        <div class="panneau pnlDroite pnlDetails">
            <lp:ProfilClient ID="ctrProfilClient" runat="server" AfficherCourrielEtMotDePasse="false" />
            <div class="lignePointilleHorizontale pleineLargeur"></div>
            <h2>Carte de crédit</h2>
            <table>
                <tr>
                    <td>Numéro: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNumero" />
                    </td>
                </tr>
                <tr>
                    <td>Date d'expiration: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDateExpiration" />
                    </td>
                </tr>
                <tr>
                    <td>Nom sur la carte: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNomCarte" />
                    </td>
                </tr>
                <tr>
                    <td>CCV: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCCV" />
                    </td>
                </tr>
            </table>
            <div class="boutonsAction">
                <asp:Button runat="server" Text="Facturer" ID="btnFacturer" OnClick="btnFacturer_OnClick" />                     
            </div>
            <div>
                <asp:CustomValidator runat="server" Display="Dynamic" ID="valQuantite" OnServerValidate="valQuantite_OnServerValidate" CssClass="erreur" /> 
            </div>
        </div>
    </div>
</asp:Content>

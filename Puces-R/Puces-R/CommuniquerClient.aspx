<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommuniquerClient.aspx.cs" Title="Informations du client"
    Inherits="Puces_R.CommuniquerClient" %>

<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="lp" TagName="ChangementMDP" Src="~/Controles/ChangementMDP.ascx" %>
<%@ Register TagPrefix="se" TagName="Adresse" Src="~/Controles/Adresse.ascx" %>
<%@ Register TagPrefix="lp" TagName="BoitePanier" Src="~/Controles/BoitePanier.ascx" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/InsertionProduits.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="rectangleComplet rectangleItem">
        <table class="formulaire" style="width: 700px;">
            <tr>
                <td>
                    Prénom
                </td>
                <td>
                    <asp:Label ID="lblPrenom" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Nom
                </td>
                <td>
                    <asp:Label ID="lblNom" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Rue
                </td>
                <td>
                    <asp:Label ID="lblRue" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Ville
                </td>
                <td>
                    <asp:Label ID="lblVille" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    Province
                </td>
                <td>
                    <asp:Label ID="lblProvince" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Pays
                </td>
                <td>
                    <asp:Label ID="lblPays" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Code postal
                </td>
                <td>
                    <asp:Label ID="lblCodePostal" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Téléphone 1
                </td>
                <td>
                    <asp:Label ID="lblTelephone1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Téléphone 2
                </td>
                <td>
                    <asp:Label ID="lblTelephone2" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnCourrierInterne" runat="server" Text="Envoyer un courriel interne" OnCommand="changer_view" CommandArgument="1" />
        <asp:Button ID="btnCourrielExterne" runat="server" Text="Envoyer un courriel Externe" OnCommand="changer_view" CommandArgument="2" />
    </div>
</asp:Content>

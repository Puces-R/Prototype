<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="Puces_R.Connexion"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagName="MotDePasse" TagPrefix="yc" Src="~/Controles/MotDePasse.ascx" %>
<%@ Register TagPrefix="yc" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuInvite runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="rectangleComplet rectangleItem">
        <table>
            <tr>
                <td>
                    Adresse courriel
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <yc:MotDePasse runat="server" ID="tbMotPasse" Obligatoire="false" />
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center;">
                    <asp:Button runat="server" CausesValidation="false" Text="Se connecter" OnClick="seConnecter" />
                    <asp:Button runat="server" CausesValidation="false" Text="Client" OnClick="defautClient" />
                    <asp:Button runat="server" CausesValidation="false" Text="Vendeur" OnClick="defautVendeur" />
                    <asp:Button runat="server" CausesValidation="false" Text="Gestionnaire" OnClick="defautGestionnaire" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

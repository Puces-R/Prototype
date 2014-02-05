<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puces_R.Default" %>

<%@ Register TagPrefix="lp" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<%@ Register TagPrefix="lp" TagName="Categories" Src="~/Controles/Categories.ascx" %>
<%@ Register TagName="MotDePasse" TagPrefix="yc" Src="~/Controles/MotDePasse.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuInvite ID="MenuInvite1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <lp:Categories runat="server" Public="true" />
        </div>
        <div  class="panneau pnlDroite">
            <div class="rectangleComplet rectangleItem">
                <table class="formulaire" style="width: 350px;">
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
                        <td class="erreur" colspan="3">
                            <asp:CustomValidator runat="server" ErrorMessage="Les identifiants sont incorrects" OnServerValidate="existe" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center;">
                            <asp:Button ID="Button1" runat="server" CausesValidation="false" Text="Se connecter" OnClick="seConnecter" />
                            <asp:Button ID="Button2" runat="server" CausesValidation="false" Text="Client" OnClick="defautClient" />
                            <asp:Button ID="Button3" runat="server" CausesValidation="false" Text="Vendeur" OnClick="defautVendeur" />
                            <asp:Button ID="Button4" runat="server" CausesValidation="false" Text="Gestionnaire" OnClick="defautGestionnaire" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

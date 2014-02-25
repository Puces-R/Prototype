<%@ Page Title="Bienvenue sur LesPetitesPuces.com" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puces_R.Default" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="Categories" Src="~/Controles/Categories.ascx" %>
<%@ Register TagName="MotDePasse" TagPrefix="yc" Src="~/Controles/MotDePasse.ascx" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="pleineHauteur">
        <div class="panneau pnlGauche">
            <lp:Categories runat="server" Public="true" />
        </div>
        <div class="panneau pnlDroite">
            <h2>Connexion</h2>
            <div class="rectangleComplet rectangleItem">
                <table class="formulaire formulaireConnexion colonneTitre">
                    <tr>
                        <td>
                            Adresse courriel
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" Width="150" />
                        </td>
                    </tr>
                    <tr>
                        <yc:MotDePasse runat="server" ID="tbMotPasse" Obligatoire="false" Longueur="false" />
                    </tr>
                </table>
                <div>
                    <div class="centre erreur">
                        <asp:CustomValidator runat="server" Text="Les identifiants sont incorrects" OnServerValidate="existe" Display="Static" />
                    </div>
                    <div class="boutonsConnexion">
                        <asp:Button runat="server" CausesValidation="false" Text="Se connecter" OnClick="seConnecter" />
                    </div>
                </div>
            </div>
<%--            <h2>Comptes de test</h2>
            <div class="rectangleComplet rectangleItem">
                <div class="boutonsConnexion">
                    <asp:Button runat="server" CausesValidation="false" Text="Client" OnClick="defautClient" />
                    <asp:Button runat="server" CausesValidation="false" Text="Vendeur" OnClick="defautVendeur" />
                    <asp:Button runat="server" CausesValidation="false" Text="Gestionnaire" OnClick="defautGestionnaire" />
                </div>
            </div>--%>
        </div>
    </div>
</asp:Content>

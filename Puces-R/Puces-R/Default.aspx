﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puces_R.Default" %>

<%@ Register TagPrefix="lp" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<%@ Register TagPrefix="lp" TagName="Categories" Src="~/Controles/Categories.ascx" %>
<%@ Register TagName="MotDePasse" TagPrefix="yc" Src="~/Controles/MotDePasse.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuInvite ID="MenuInvite1" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <lp:Categories runat="server" Public="true" />
        </div>
        <div class="panneau pnlDroite">
            <h2>Connexion</h2>
            <div class="rectangleComplet rectangleItem">
                <table class="formulaire formulaireConnexion colonneTitre">
                    <tr>
                        <td>
                            Adresse courriel: 
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" Width="150" />
                        </td>
                    </tr>
                    <tr>
                        <yc:MotDePasse runat="server" ID="tbMotPasse" Obligatoire="false" />
                    </tr>
                </table>
                <div>
                    <div>
                        <asp:CustomValidator runat="server" Text="Les identifiants sont incorrects" OnServerValidate="existe" CssClass="erreur" Display="Static" />
                    </div>
                    <div class="boutonsConnexion">
                        <asp:Button runat="server" CausesValidation="false" Text="Se connecter" OnClick="seConnecter" />
                    </div>
                </div>
            </div>
            <h2>Comptes de test</h2>
            <div class="rectangleComplet rectangleItem">
                <div class="boutonsConnexion">
                    <asp:Button runat="server" CausesValidation="false" Text="Client" OnClick="defautClient" />
                    <asp:Button runat="server" CausesValidation="false" Text="Vendeur" OnClick="defautVendeur" />
                    <asp:Button runat="server" CausesValidation="false" Text="Gestionnaire" OnClick="defautGestionnaire" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
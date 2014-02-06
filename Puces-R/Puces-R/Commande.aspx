﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Commande.aspx.cs" Inherits="Puces_R.Commande" %>

<%@ Register TagPrefix="lp" TagName="ProfilClient" Src="~/Controles/ProfilClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>
<%@ Register TagPrefix="lp" TagName="TablePanier" Src="~/Controles/TablePanier.ascx" %>

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
                        <asp:TextBox runat="server" ID="txtNumero" MaxLength="16" TextMode="Number" Columns="16" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNumero" Text="Le numéro ne peut pas être vide!" CssClass="erreur" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNumero" ValidationExpression="^\d{16}$" Text="Le numéro doit être composé de 16 chiffres!" CssClass="erreur" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td>Mois d'expiration: </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlMoisExpiration">
                            <asp:ListItem Text="" Value="-1" />
                            <asp:ListItem Text="Janvier" Value="01" />
                            <asp:ListItem Text="Février" Value="02" />
                            <asp:ListItem Text="Mars" Value="03" />
                            <asp:ListItem Text="Avril" Value="04" />
                            <asp:ListItem Text="Mai" Value="05" />
                            <asp:ListItem Text="Juin" Value="06" />
                            <asp:ListItem Text="Juillet" Value="07" />
                            <asp:ListItem Text="Aout" Value="08" />
                            <asp:ListItem Text="Septembre" Value="09" />
                            <asp:ListItem Text="Octobre" Value="10" />
                            <asp:ListItem Text="Novembre" Value="11" />
                            <asp:ListItem Text="Décembre" Value="12" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMoisExpiration" Text="Le mois d'expiration ne peut pas être vide!" CssClass="erreur" InitialValue="-1" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td>Année d'expiration: </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlAnneeExpiration">
                            <asp:ListItem Text="" Value="-1" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlAnneeExpiration" Text="L'année d'expiration ne peut pas être vide!" CssClass="erreur" InitialValue="-1" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td>Nom sur la carte: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNomCarte" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNomCarte" Text="Le nom sur la carte ne peut pas être vide!" CssClass="erreur" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td>CCV: </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCCV" MaxLength="3" TextMode="Number" Columns="3" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCCV" Text="Le numéro de sécurité doit être spécifié!" CssClass="erreur" Display="Dynamic" />
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

﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilClient.ascx.cs" Inherits="Puces_R.Controles.ProfilClient" %>

<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="lp" TagName="ChangementMDP" Src="~/Controles/ChangementMDP.ascx" %>
<%@ Register TagPrefix="se" TagName="Adresse" Src="~/Controles/Adresse.ascx" %>

<h2>Profil</h2>
<table class="tableProfil">
    <asp:PlaceHolder runat="server" ID="phCourrielEtMotDePasse">
        <tr>
            <td>Courriel: </td>
            <td>
                <asp:Label ID="lblCourriel" runat="server" />
            </td>
        </tr>
        <lp:ChangementMDP runat="server" ID="ctrMotDePasse" />
    </asp:PlaceHolder>
    <tr>
        <td>Prénom: </td>
        <td>
            <asp:TextBox ID="txtPrenom" runat="server" />
            <asp:RequiredFieldValidator runat="server" CssClass="erreur" Text="Le prénom ne peut pas être vide!" ControlToValidate="txtPrenom" />
        </td>
    </tr>
    <tr>
        <td>Nom: </td>
        <td>
            <asp:TextBox ID="txtNom" runat="server" />
            <asp:RequiredFieldValidator runat="server" CssClass="erreur" Text="Le nom ne peut pas être vide!" ControlToValidate="txtNom" />
        </td>
    </tr>
    <se:Adresse ID="txtRue" runat="server" Label="Rue" />
    <tr>
        <td>Ville: </td>
        <td>
            <asp:TextBox ID="txtVille" runat="server" />
            <asp:RequiredFieldValidator runat="server" CssClass="erreur" Text="La ville ne peut pas être vide!" ControlToValidate="txtVille" />
        </td>
    </tr>
    <tr>
        <td>Province: </td>
        <td>
            <yc:Province ID="ctrProvince" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Pays: </td>
        <td>
            <asp:TextBox ID="txtPays" runat="server" ReadOnly="true" />
        </td>
    </tr>
    <yc:CodePostal ID="ctrCodePostal" runat="server" />
    <yc:Telephone ID="ctrTelephone" runat="server" />
    <yc:Telephone ID="ctrCellulaire" Label="Cellulaire: " Obligatoire="false" runat="server" />
</table>
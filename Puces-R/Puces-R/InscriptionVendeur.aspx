<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InscriptionVendeur.aspx.cs"
    Inherits="Puces_R.InscriptionVendeur" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="Identifiants" Src="~/Controles/Identifiants.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuInvite runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
<div class="rectangleComplet rectangleItem">
    <table class="formulaire">
        <yc:Identifiants runat="server" ID="tbIdentifiants" />
        <tr>
            <td>
                Nom d'affaire
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbNomAffaires" MaxLength="50" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Nom
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbNom" MaxLength="50" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Prénom
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPrenom" MaxLength="50" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Rue
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbRue" MaxLength="50" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Ville
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbVille" MaxLength="50" />
            </td>
            <td>
            </td>
        </tr>
        <yc:CodePostal runat="server" ID="tbCodePostal" Obligatoire="true" />
        <tr>
            <td>
                Pays
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPays" Text="Canada" MaxLength="10" Enabled="false" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Province
            </td>
            <td>
                <yc:Province runat="server" ID="tbProvince" />
            </td>
            <td>
            </td>
        </tr>
        <yc:Telephone runat="server" ID="tbTel1" Label="Téléphone 1" Obligatoire="true" />
        <yc:Telephone runat="server" ID="tbTel2" Label="Téléphone 2"/>
        <tr>
            <td>
                Poids maximal d'une commande
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPoids" MaxLength="10" />
            </td>
            <td class="erreur">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPoids" ErrorMessage="Ce champ est obligatoire"
                    Display="Dynamic" />
                <asp:RangeValidator runat="server" ControlToValidate="tbPoids" Type="Integer" MinimumValue="0"
                    MaximumValue="2147483647" ErrorMessage="Ce champ doit contenir un nombre entre 0 et 2147483647"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                Prix minimum pour une livraison gratuite
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPrixLivraison" MaxLength="50" />
            </td>
            <td class="erreur">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrixLivraison" ErrorMessage="Ce champ est obligatoire"
                    Display="Dynamic" />
                <asp:RangeValidator runat="server" ControlToValidate="tbPrixLivraison" Type="Currency"
                    MinimumValue="0" MaximumValue="214748,36" ErrorMessage="Ce champ doit contenir un nombre en 0 et 214748,36"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:CheckBox runat="server" ID="cbTaxes" Checked="true" Text="Taxes" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center;" >
                <asp:Button runat="server" ID="btnConfirmer" Text="Confirmer l'inscription" CausesValidation="false"
                    OnClick="inscription" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

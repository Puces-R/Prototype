<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilVendeur.ascx.cs"
    Inherits="Puces_R.Controles.ProfilVendeur" %>
<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="yc" TagName="Courriel" Src="~/Controles/Courriel.ascx" %>
<h2>
    Profil du vendeur</h2>
<table class="tableProfil">
    <tr>
        <td>
            Nom d'affaires
        </td>
        <td>
            <asp:TextBox ID="tbNomAffaires" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Prénom
        </td>
        <td>
            <asp:TextBox ID="txtPrenom" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Nom
        </td>
        <td>
            <asp:TextBox ID="txtNom" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Rue
        </td>
        <td>
            <asp:TextBox ID="txtRue" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Ville
        </td>
        <td>
            <asp:TextBox ID="txtVille" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Province
        </td>
        <td>
            <yc:Province ID="ctrProvince" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <yc:CodePostal ID="ctrCodePostal" runat="server" Obligatoire="true" />
    <tr>
        <td>
            Pays
        </td>
        <td>
            <asp:TextBox ID="txtPays" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <yc:Telephone ID="ctrTelephone1" runat="server" Obligatoire="true" Label="Telephone 1" />
    <yc:Telephone ID="ctrTelephone2" runat="server" Label="Telephone 2" />
    <tr>
        <td>
            Courriel :
        </td>
        <td>
            <asp:Label ID="lblCourriel" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Mot de passe :
        </td>
        <td>
            <asp:Button ID="btnPassword" runat="server" Text="Changer votre mot de passe!" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Montant maximum pour livraison :
        </td>
        <td>
            <asp:TextBox ID="tbMaxLivraison" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Montant pour livraison gratuite:
        </td>
        <td>
            <asp:TextBox ID="tbLivraisonGratuite" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Taxes:
        </td>
        <td>
            <asp:CheckBox ID="cbTaxes" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Pourcentage :
        </td>
        <td>
            <asp:TextBox ID="tbPourcentage" runat="server" Enabled="false" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Dernière mise à jour effectuée le :
        </td>
        <td>
            <asp:Label ID="lblMAJ" runat="server" Enabled="false" />
        </td>
        <td>
        </td>
    </tr>
</table>
<asp:Button runat="server" ID="btnSauvegarder" Text="Sauvegarder" CausesValidation="FALSE"
    OnClick="sauverProfil" />
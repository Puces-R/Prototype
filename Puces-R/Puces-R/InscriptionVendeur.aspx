<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InscriptionVendeur.aspx.cs"
    Inherits="Puces_R.InscriptionVendeur" %>

<%@ Register TagPrefix="yc" TagName="Identifiants" Src="~/Controles/Identifiants.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>S'inscrire en tant que vendeur</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <yc:Identifiants runat="server" ID="tbIdentifiants" />
        <tr>
            <td>
                Nom d'affaire
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbNomAffaires" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Nom
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbNom" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Prénom
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPrenom" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Rue
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbRue" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Ville
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbVille" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                Code Postal
            </td>
            <td>
                <yc:CodePostal runat="server" ID="tbCodePostal" />
            </td>
        </tr>
        <tr>
            <td>
                Pays
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPays" Text="Canada" MaxLength="10" Enabled="false"/>
            </td>
        </tr>
        <tr>
        <td>Province</td>
        <td><yc:Province runat="server" ID="tbProvince" /></td>
        <tr>
            <td>
                Téléphone 1
            </td>
            <td>
                <yc:Telephone runat="server" ID="tbTel1" Obligatoire="true" />
            </td>
        </tr>
        <tr>
            <td>
                Téléphone 2
            </td>
            <td>
                <yc:Telephone runat="server" ID="tbTel2" />
            </td>
        </tr>
        <tr>
            <td>
                Poids maximal d'une commande
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPoids" MaxLength="10" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPoids" ErrorMessage="Obligatoire" />
                <asp:RangeValidator runat="server" ControlToValidate="tbPoids" Type="Integer" MinimumValue="0"
                    MaximumValue="100" ErrorMessage="Format" />
            </td>
        </tr>
        <tr>
            <td>
                Prix minimum pour une livraison gratuite
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbPrixLivraison" MaxLength="50" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrixLivraison" ErrorMessage="Obligatoire" />
                <asp:RangeValidator runat="server" ControlToValidate="tbPrixLivraison" Type="Currency"
                    MinimumValue="0" MaximumValue="100" ErrorMessage="Format" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:CheckBox runat="server" ID="cbTaxes" Checked="true" Text="Taxes" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnConfirmer" Text="Confirmer l'inscription" CausesValidation="false"
                    OnClick="inscription" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilVendeur.ascx.cs"
    Inherits="Puces_R.Controles.ProfilVendeur" %>
<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="yc" TagName="Courriel" Src="~/Controles/Courriel.ascx" %>
<%@ Register TagPrefix="se" TagName="Adresse" Src="~/Controles/Adresse.ascx" %>

<tr>
    <td>
        Nom d'affaires
    </td>
    <td>
        <asp:TextBox ID="tbNomAffaires" runat="server" MaxLength="50" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="reqNomAffaires" runat="server" ControlToValidate="tbNomAffaires"
            ErrorMessage="Le nom d'affaires est obligatoire" Display="Dynamic" />
    </td>
</tr>
<tr>
    <td>
        Prénom
    </td>
    <td>
        <asp:TextBox ID="txtPrenom" runat="server" MaxLength="50" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="reqPrenom" runat="server" ControlToValidate="txtPrenom"
            ErrorMessage="Le prénom est obligatoire" Display="Dynamic" />
    </td>
</tr>
<tr>
    <td>
        Nom
    </td>
    <td>
        <asp:TextBox ID="txtNom" runat="server" MaxLength="50" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="reqNom" runat="server" ControlToValidate="txtNom"
            ErrorMessage="Le nom est obligatoire" Display="Dynamic" />
    </td>
</tr>
<se:Adresse ID="ctrAdresse" runat="server" Label="Rue" />
<tr>
    <td>
        Ville
    </td>
    <td>
        <asp:TextBox ID="txtVille" runat="server" MaxLength="50" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="reqVille" runat="server" ControlToValidate="txtVille"
            ErrorMessage="La ville est obligatoire" Display="Dynamic" />
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
        <asp:TextBox ID="txtPays" runat="server" Enabled="false" Text="Canada" MaxLength="10" />
    </td>
    <td class="erreur">
    <%-- Pas nécessaire, mais ajouté en sécurité de plus --%>
    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPays" ErrorMessage="Le pays est obligatoire"
            Display="Dynamic" />
    </td>
</tr>
<yc:Telephone ID="ctrTelephone1" runat="server" Obligatoire="true" Label="Téléphone 1" />
<yc:Telephone ID="ctrTelephone2" runat="server" Label="Téléphone 2" />
<tr>
    <td>
        Poids maximum d'une livraison (en lbs)
    </td>
    <td>
        <asp:TextBox ID="tbMaxLivraison" runat="server" MaxLength="9" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbMaxLivraison" ErrorMessage="Le poids est obligatoire"
            Display="Dynamic" />
        <asp:RangeValidator runat="server" ControlToValidate="tbMaxLivraison" Type="Integer"
            MinimumValue="1" MaximumValue="999999999" ErrorMessage="Le poids doit se situer entre 1 et 999999999 lbs"
            Display="Dynamic" />
    </td>
</tr>
<tr>
    <td>
        Montant minimum pour livraison gratuit
    </td>
    <td>
        <asp:TextBox ID="tbLivraisonGratuite" runat="server" MaxLength="8" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbLivraisonGratuite"
            ErrorMessage="Le montant est obligatoire" Display="Dynamic" />
        <asp:RangeValidator runat="server" ControlToValidate="tbLivraisonGratuite" Type="Currency"
            MinimumValue="0" MaximumValue="99999,99" ErrorMessage="Le montant doit se situer entre 0 et 99999,99 $"
            Display="Dynamic" />
    </td>
</tr>
<tr>
    <td>
        Taxes
    </td>
    <td>
        <asp:CheckBox ID="cbTaxes" runat="server" Checked="true" />
    </td>
    <td>
    </td>
</tr>

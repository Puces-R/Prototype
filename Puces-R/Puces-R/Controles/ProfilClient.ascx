<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilClient.ascx.cs"
    Inherits="Puces_R.Controles.ProfilClient" %>
<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>
<%@ Register TagPrefix="se" TagName="Adresse" Src="~/Controles/Adresse.ascx" %>
<asp:PlaceHolder runat="server" ID="phCourrielEtMotDePasse">
    <tr>
        <td>
            Courriel
        </td>
        <td>
            <asp:Label ID="lblCourriel" runat="server" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:HyperLink runat="server" NavigateUrl="~/ModifierMotPasse.aspx" Text="Modifier le mot de passe" />
        </td>
        <td>
        </td>
    </tr>
</asp:PlaceHolder>
<tr>
    <td>
        Prénom
    </td>
    <td>
        <asp:TextBox ID="txtPrenom" runat="server" MaxLength="50" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator runat="server" Text="Le prénom est obligatoire" ControlToValidate="txtPrenom"
            Display="Dynamic" />
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
        <asp:RequiredFieldValidator runat="server" Text="Le nom est obligatoire" ControlToValidate="txtNom"
            Display="Dynamic" />
    </td>
</tr>
<se:Adresse ID="txtRue" runat="server" Label="Rue" />
<tr>
    <td>
        Ville
    </td>
    <td>
        <asp:TextBox ID="txtVille" runat="server" MaxLength="50" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator runat="server" Text="La ville est obligatoire"
            ControlToValidate="txtVille" />
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
<tr>
    <td>
        Pays
    </td>
    <td>
        <asp:TextBox ID="txtPays" runat="server" ReadOnly="true" />
    </td>
    <td>
    </td>
</tr>
<yc:CodePostal ID="ctrCodePostal" runat="server" Obligatoire="true" />
<yc:Telephone ID="ctrTelephone" runat="server" Obligatoire="true" />
<yc:Telephone ID="ctrCellulaire" Label="Cellulaire" Obligatoire="false" runat="server" />

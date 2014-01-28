<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfilClient.ascx.cs" Inherits="Puces_R.Controles.ProfilClient" %>

<%@ Register TagPrefix="yc" TagName="CodePostal" Src="~/Controles/CodePostal.ascx" %>
<%@ Register TagPrefix="yc" TagName="Province" Src="~/Controles/Province.ascx" %>
<%@ Register TagPrefix="yc" TagName="Telephone" Src="~/Controles/Telephone.ascx" %>

<h2>Profil</h2>
<table class="tableProfil">
    <tr>
        <td>Prénom</td>
        <td>
            <asp:TextBox ID="txtPrenom" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Nom</td>
        <td>
            <asp:TextBox ID="txtNom" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Rue</td>
        <td>
            <asp:TextBox ID="txtRue" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Ville</td>
        <td>
            <asp:TextBox ID="txtVille" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Province</td>
        <td>
            <yc:Province ID="ctrProvince" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Pays</td>
        <td>
            <asp:TextBox ID="txtPays" runat="server" />
        </td>
    </tr>
    <yc:CodePostal ID="ctrCodePostal" runat="server" />
    <yc:Telephone ID="ctrTelephone" runat="server" />
    <yc:Telephone ID="ctrCellulaire" Label="Celluaire" runat="server" />
</table>
<asp:Button runat="server" ID="btnSauvegarder" Text="Sauvegarder" OnClick="btnSauvegarder_OnClick" />
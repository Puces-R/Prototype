<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DoubleMdp.ascx.cs" Inherits="Puces_R.DoubleMdp" %>
<%@ Register TagPrefix="yc" TagName="MotDePasse" Src="~/Controles/MotDePasse.ascx" %>
<tr>
    <td>
        Mot de passe
    </td>
    <td>
        <yc:MotDePasse runat="server" ID="tbMDP1" Obligatoire="true" />
    </td>
</tr>
<tr>
    <td>
        Confirmer le mot de passe
    </td>
    <td>
        <yc:MotDePasse runat="server" ID="tbMDP2" Obligatoire="false" />
        <asp:CustomValidator runat="server" OnServerValidate="validerMDPIdentique" ErrorMessage="Différents" />
    </td>
</tr>

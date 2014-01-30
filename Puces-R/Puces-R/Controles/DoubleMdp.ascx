<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DoubleMdp.ascx.cs" Inherits="Puces_R.DoubleMdp" %>
<%@ Register TagPrefix="yc" TagName="MotDePasse" Src="~/Controles/MotDePasse.ascx" %>
<tr>
    <yc:MotDePasse runat="server" ID="tbMDP1" Obligatoire="true" />
</tr>
<tr>
    <yc:MotDePasse runat="server" ID="tbMDP2" Obligatoire="false" Label="Confirmer le mot de passe"/>
    <td class="erreur">
        <asp:CustomValidator ID="valTbMDP2" runat="server" OnServerValidate="validerMDPIdentique" ErrorMessage="Les mots de passe ne correspondent pas" Display="Dynamic"/>
    </td>
</tr>

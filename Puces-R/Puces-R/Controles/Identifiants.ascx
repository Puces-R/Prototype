<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Identifiants.ascx.cs"
    Inherits="Puces_R.Identifiants" %>
<%@ Register TagPrefix="yc" TagName="Courriel" Src="~/Controles/Courriel.ascx" %>
<%@ Register TagPrefix="yc" TagName="DoubleMdp" Src="~/Controles/DoubleMdp.ascx" %>
<yc:Courriel runat="server" ID="tbCourriel" Existe="true" Obligatoire="true" />
<tr>
    <td>
        Confirmer l'adresse courriel
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbCourrielConfirmation" MaxLength="100" />
    </td>
    <td>
        <asp:CustomValidator runat="server" OnServerValidate="validerCourrielIdentique" ErrorMessage="Les adresses courriel ne correspondent pas"
            Display="Dynamic" />
    </td>
</tr>
<yc:DoubleMdp runat="server" ID="tbMDP" />

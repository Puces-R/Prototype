<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangementMDP.ascx.cs" Inherits="Puces_R.Controles.ChangementMDP" %>

<%@ Register TagPrefix="yc" TagName="DoubleMdp" Src="~/Controles/DoubleMdp.ascx" %>

<yc:DoubleMdp runat="server" ID="ctrMotDePasse" Visible="false" Changement="true" />
<tr runat="server" id="trMotDePasse">
    <td />
    <td>
        <asp:Button ID="btnMotDePasse" runat="server" Text="Changer de mot de passe" OnClick="btnMotDePasse_OnClick" CausesValidation="false" />
    </td>
</tr>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MontantsFactures.ascx.cs" Inherits="Puces_R.Controles.MontantsFactures" %>

<h2>Facture</h2>
<table class="tableFacture">
    <tr>
        <td>Poids Total: </td>
        <td><asp:Label ID="lblPoidsTotal" runat="server" /></td>
    </tr>
    <tr>
        <td>Sous-Total: </td>
        <td><asp:Label ID="lblSousTotal" runat="server" /></td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="ddlModesLivraison" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModesLivraison_OnSelectedIndexChanged" Font-Size="X-Large" />:
        </td>
        <td><asp:Label ID="lblLivraison" runat="server" /></td>
    </tr>
    <tr>
        <td>TPS <asp:Label ID="lblTauxTPS" CssClass="tauxTaxes" runat="server" />: </td>
        <td><asp:Label ID="lblTPS" runat="server" /></td>
    </tr>
    <tr>
        <td>TVQ <asp:Label ID="lblTauxTVQ" CssClass="tauxTaxes" runat="server" />: </td>
        <td><asp:Label ID="lblTVQ" runat="server" /></td>
    </tr>
    <tr>
        <td>Grand-Total: </td>
        <td><asp:Label ID="lblGrandTotal" runat="server" CssClass="grandTotal" /></td>
    </tr>
    <tr>
        <td />
        <td>
            <asp:Button runat="server" Text="Commander" Font-Size="X-Large" ID="btnCommander" OnClick="btnCommander_OnClick" />
        </td>
    </tr>
</table>
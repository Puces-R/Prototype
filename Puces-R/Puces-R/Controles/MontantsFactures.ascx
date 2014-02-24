<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MontantsFactures.ascx.cs" Inherits="Puces_R.Controles.MontantsFactures" %>

<table class="tableFacture">
    <tr>
        <td>Poids Total: </td>
        <td><asp:Label ID="lblPoidsTotal" runat="server" /></td>
    </tr>
    <tr>
        <td>Sous-Total: </td>
        <td><asp:Label ID="lblSousTotal" runat="server" /></td>
    </tr>
    <asp:MultiView runat="server" ID="mvPartieBas" ActiveViewIndex="0">
        <asp:View runat="server">
            <tr>
                <td>
                    <asp:DropDownList ID="ddlModesLivraison" runat="server" AutoPostBack="true" Font-Size="X-Large" OnSelectedIndexChanged="ddlModesLivraison_OnSelectedIndexChanged" />:
                </td>
                <td><asp:Label ID="lblLivraison" runat="server" /></td>
            </tr>
            <tr>
                <td>TPS <asp:Label ID="lblTauxTPS" CssClass="tauxTaxes" runat="server" />: </td>
                <td><asp:Label ID="lblTPS" runat="server" /></td>
            </tr>
            <tr>
                <td>TVQ <asp:Label ID="lblTauxTVQ" CssClass="tauxTaxes" runat="server" />: </td>
                <td>
                    <asp:Label ID="lblTVQ" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Grand-Total: </td>
                <td>
                    <asp:Label ID="lblGrandTotal" runat="server" CssClass="grandTotal" />
                    <asp:Label Visible="false" ID="lblPrixReviseEtoile" ForeColor="Red" />
                </td>
            </tr>
        </asp:View>
        <asp:View runat="server">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMessageErreur" runat="server" Font-Size="Large" ForeColor="Red" />
                </td>
            </tr>
        </asp:View>
    </asp:MultiView>
</table>
<asp:Label Visible="false" ID="lblPrixReviseMessage" runat="server" Text="*Certain produits sont en vente, le prix seras révisé à la baisse lors de la commande." CssClass="" />
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TableDetailsCommande.ascx.cs" Inherits="Puces_R.Controles.TableDetailsCommande" %>

<asp:Repeater ID="rptProduits" runat="server" OnItemDataBound="rptProduits_ItemDataBound">
    <HeaderTemplate>
        <table class="tableProduits">
            <tr>
                <th>Produit</th>
                <th>Quantité</th>
                <th>Prix unitaire</th>
                <th>Prix total</th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
            <tr>
                <td>
                    <asp:HyperLink runat="server" ID="hypProduit" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblQuantite" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblPrixUnitaire" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblPrixTotal" />
                </td>
            </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
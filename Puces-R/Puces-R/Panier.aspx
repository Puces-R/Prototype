<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Panier.aspx.cs" Inherits="Puces_R.Panier" MasterPageFile="Site.Master" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="CSS/Panier.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="pnlGauche">
        <asp:Repeater runat="server" ID="rptProduits" OnItemDataBound="rptProduits_ItemDataBound" OnItemCommand="rptProduits_ItemCommand">
            <ItemTemplate>
                <div class="rectangleStylise rectangleProduits">
                    <div class="boiteImageProduit">
                        <div>
                            <asp:Image runat="server" ID="imgProduit" />
                        </div>
                    </div>
                    <div class="boiteDetailsProduit">
                        <div>
                            <asp:Label runat="server" ID="lblNoProduit" />
                            <asp:Label runat="server" ID="lblDescriptionAbregee" />
                            <asp:Label runat="server" ID="lblCategorie" />
                            <asp:Label runat="server" ID="lblPrixDemande" />
                            <div>
                                Quantité: <asp:TextBox runat="server" ID="txtQuantite" CssClass="boiteQuantite" />
                                <asp:Button runat="server" ID="btnMAJQuantite" Text="Changer" CommandName="MAJQuantite" />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="pnlDroite pnlDetails">
        <table>
            <tr>
                <td>Poids Total: </td>
                <td><asp:Label ID="lblPoidsTotal" runat="server" /></td>
            </tr>
            <tr>
                <td>Sous-Total: </td>
                <td><asp:Label ID="lblSousTotal" runat="server" /></td>
            </tr>
            <tr>
                <td>Livraison: </td>
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
        </table>
    </div>
</asp:Content>
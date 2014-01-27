<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccueilClient.aspx.cs" Inherits="Puces_R.AccueilClient" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
 
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="panneau pnlGauche">
        <h2>Catégories</h2>
        <div class="categories">
            <ASP:Repeater id="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <asp:Label runat="server" ID="lblCategorie" CssClass="titreRectangle" />
                        <asp:Repeater ID="rptVendeurs" runat="server" OnItemDataBound="rptVendeurs_ItemDataBound">
                            <ItemTemplate>
                                <div class="vendeur">
                                    <asp:HyperLink runat="server" ID="hypVendeur" CssClass="lienProduitsVendeur" /> (<asp:Label runat="server" ID="lblNbProduits" />)
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Panel runat="server" ID="pnlAucunVendeur" CssClass="lienProduitsVendeur">
                                    Aucun vendeurs
                                </asp:Panel>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="panneau pnlDroite">
        <h2>Paniers</h2>
        <ASP:Repeater id="rptPaniers" runat="server" OnItemDataBound="rptPaniers_ItemDataBound">
            <ItemTemplate>
                <div class="rectangleStylise">
                    <asp:HyperLink runat="server" ID="hypVendeur" CssClass="titreRectangle" />
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
                    <div class="sousTotal">
                        Sous-Total: <asp:Label runat="server" ID="lblSousTotal" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
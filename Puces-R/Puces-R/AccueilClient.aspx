<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccueilClient.aspx.cs" Inherits="Puces_R.AccueilClient" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="TablePanier" Src="~/Controles/TablePanier.ascx" %>
 
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <h2>Catégories</h2>
            <div class="categories">
                <ASP:Repeater id="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                    <ItemTemplate>
                        <div>
                            <asp:Label runat="server" ID="lblCategorie" CssClass="categorie" />
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
            <div class="catalogueGlobal">
                <asp:HyperLink NavigateUrl="Produits.aspx" runat="server" Text="Tout les produits" />
            </div>
        </div>
        <div class="panneau pnlDroite">
            <h2>Paniers</h2>
            <ASP:Repeater id="rptPaniers" runat="server" OnItemDataBound="rptPaniers_ItemDataBound">
                <ItemTemplate>
                    <div class="rectangleItem hautRectangle">
                        <asp:HyperLink runat="server" ID="hypVendeur" />
                    </div>
                    <div class="rectangleItem basRectangle">
                        <lp:TablePanier runat="server" ID="ctrProduits" />
                        <div class="sousTotal">
                            Sous-Total: <asp:Label runat="server" ID="lblSousTotal" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
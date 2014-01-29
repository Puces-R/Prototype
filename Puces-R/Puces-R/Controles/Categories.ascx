<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="Puces_R.Controles.Categories" %>

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
    <asp:HyperLink ID="HyperLink1" NavigateUrl="../Produits.aspx" runat="server" Text="Tout les produits" />
</div>
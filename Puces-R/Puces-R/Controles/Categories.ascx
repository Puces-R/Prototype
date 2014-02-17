<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="Puces_R.Controles.Categories" %>

<h2>
    Catalogue 
    <asp:DropDownList runat="server" ID="ddlRegroupement" AutoPostBack="true" OnSelectedIndexChanged="ddlRegroupement_OnSelectedIndexChanged" CssClass="boutonApresTitre">
        <asp:ListItem Text="Par catégorie" Selected="True" />
        <asp:ListItem Text="Par vendeur" />
    </asp:DropDownList>
</h2>
<div class="categories">
    <asp:MultiView runat="server" ID="mvCatalogue" ActiveViewIndex="0">
        <asp:View runat="server">
            <ASP:Repeater id="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <asp:HyperLink runat="server" ID="hypCategorie" CssClass="categorie" ForeColor="Black" />
                        <asp:Repeater ID="rptVendeursDeCategorie" runat="server" OnItemDataBound="rptVendeursDeCategorie_ItemDataBound">
                            <ItemTemplate>
                                <div class="vendeur">
                                    <asp:HyperLink runat="server" ID="hypVendeur" CssClass="lienProduitsVendeur" /> (<asp:Label runat="server" ID="lblNbProduits" />)
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:View>
        <asp:View runat="server">
            <ASP:Repeater id="rptVendeurs" runat="server" OnItemDataBound="rptVendeurs_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <asp:HyperLink runat="server" ID="hypVendeur" CssClass="categorie" ForeColor="Black" />
                        <asp:Repeater ID="rptCategoriesDeVendeur" runat="server" OnItemDataBound="rptCategoriesDeVendeur_ItemDataBound">
                            <ItemTemplate>
                                <div class="vendeur">
                                    <asp:HyperLink runat="server" ID="hypCategorie" CssClass="lienProduitsVendeur" /> (<asp:Label runat="server" ID="lblNbProduits" />)
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:View>
    </asp:MultiView>
</div>
<div class="catalogueGlobal">
    <asp:HyperLink ID="hypTous" runat="server" Text="Tous les produits" />
</div>
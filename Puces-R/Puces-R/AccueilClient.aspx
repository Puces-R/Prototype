<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccueilClient.aspx.cs" Inherits="Puces_R.AccueilClient" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="TablePanier" Src="~/Controles/TablePanier.ascx" %>
<%@ Register TagPrefix="lp" TagName="Categories" Src="~/Controles/Categories.ascx" %>
 
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <lp:Categories runat="server" />
        </div>
        <div class="panneau pnlDroite" style="height: 400px;">
            <h2>Paniers</h2>
            <asp:MultiView runat="server" ID="mvPaniers">
                <asp:View runat="server">
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
                </asp:View>
                <asp:View runat="server">
                    <div class="aucunPanier rectangleItem rectangleComplet">
                        <img src="Images/Precedent.png" alt="Flèche" />
                        <p>Vous n'avez présentement aucun produit en panier. Vous pouvez explorer par catégorie les différents produits de nos vendeurs.</p>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
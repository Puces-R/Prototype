<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GererPanierVendeur.aspx.cs" Inherits="Puces_R.GererPanierVendeur"  %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="se" TagName="PanierNettoyer" Src="~/Controles/NettoyerPanier.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/GestionCommandesVendeur.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">

<h2>Panier(s) Inactif</h2>

               
        <ASP:Repeater id="rptPaniers" runat="server" OnItemDataBound="rptPaniers_ItemDataBound">
            <ItemTemplate>
                <div class="rectangleItem rectangleComplet">
                    <asp:HyperLink runat="server" ID="hypVendeur" CssClass="titreRectangle" />
                    <se:PanierNettoyer ID="ctrPanierN" runat="server" />
                    <%--<asp:Repeater ID="rptProduits" runat="server" OnItemDataBound="rptProduits_ItemDataBound">
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
                    </asp:Repeater>--%>
                    <div class="sousTotal">
                        Sous-Total: <asp:Label runat="server" ID="lblSousTotal" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

</asp:Content>
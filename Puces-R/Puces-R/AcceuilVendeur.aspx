<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcceuilVendeur.aspx.cs" Inherits="Puces_R.AcceuilVendeur" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuVendeur.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="MenuClient1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <div class="panneau ">
 <asp:Label ID="nbVisite" runat="server" Text="Nombre de visites sur votre catalogue  " ></asp:Label>
        <h2>Paniers en Cours</h2>
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

         <h2>Commandes non traitées </h2>
         <asp:Repeater runat="server" ID="rptProduits" OnItemDataBound="rptCommandes_ItemDataBound" OnItemCommand="rptCommandes_ItemCommand">
            <ItemTemplate>
                <div class="rectangleStylise rectangleProduits">
                   
                    <div class="boiteDetailsProduit">
                        <div>
                            <asp:Label runat="server" ID="lblNoCommande" />
                            <asp:Label runat="server" ID="lblNoClient" />
                            <asp:Label runat="server" ID="lblCategorie" />
                            <asp:Label runat="server" ID="lblnoVendeur" />
                            <asp:Label runat="server" ID="lblDateCommande" />
                            <asp:Label runat="server" ID="lblTypeLivraison" />
                            <asp:Label runat="server" ID="lblMontantTotal" />
                            <asp:Label runat="server" ID="lblTPS" />
                            <asp:Label runat="server" ID="lblTVQ" />
                            <asp:Label runat="server" ID="lblPoidsTotal" />
                            <asp:Label runat="server" ID="lblStatut" />
                            <asp:Label runat="server" ID="lblNoAutorisation" />

                            
                            
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
 </asp:Content>

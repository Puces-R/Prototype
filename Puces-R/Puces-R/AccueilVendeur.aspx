<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccueilVendeur.aspx.cs" Inherits="Puces_R.AccueilVendeur" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="BoitePanier" Src="~/Controles/BoitePanier.ascx" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/AccueilClient.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    Consultations de votre catalogue: <asp:Label ID="nbVisite" runat="server"></asp:Label>
    <div class="lignePointilleHorizontale pleineLargeur">
    </div>
    <div>
        <div class="panneau pnlGauche">
            <h2>Paniers en Cours</h2>
            <asp:Repeater ID="rptPaniers" runat="server" OnItemDataBound="rptPaniers_ItemDataBound">
                <ItemTemplate>
                    <lp:BoitePanier runat="server" ID="ctrBoitePanier" />
                </ItemTemplate>
            </asp:Repeater>
            <asp:HyperLink ID="hplPanier" runat="server" Text="Voir plus..." CssClass="catalogueGlobal"></asp:HyperLink>
        </div>
        <div class="panneau pnlDroite">
            <h2>
                Commandes non traitées
            </h2>
            <asp:Repeater runat="server" ID="rptCommandes" OnItemDataBound="rptCommandes_ItemDataBound"
                OnItemCommand="rptCommandes_ItemCommand">
                <ItemTemplate>
                    <div class="rectangleItem rectangleComplet">
                        <div class="boiteDetailsProduit">
                            <div>
                                <table class="tableProduits">
                                    <tr>
                                        <th>
                                            No Commande
                                        </th>
                                        <th>
                                            Numéro Client
                                        </th>
                                        <th>
                                            Date de commande
                                        </th>
                                        <th>
                                            Frais de livraison
                                        </th>
                                        <th>
                                            Montant total
                                        </th>
                                        <th>
                                            Frais TPS
                                        </th>
                                        <th>
                                            Frais TVQ
                                        </th>
                                        <th>
                                            Poids de la Commande
                                        </th>
                                        <th>
                                            Statut
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink runat="server" ID="hypCommande" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNoClient" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblDateCommande" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTypeLivraison" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblMontantTotal" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTPS" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTVQ" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPoidsTotal" />
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblStatut" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:HyperLink ID="hplToutesCommandes" runat="server" Text="Voir plus..." CssClass="catalogueGlobal"></asp:HyperLink>
        </div>
    </div>
</asp:Content>

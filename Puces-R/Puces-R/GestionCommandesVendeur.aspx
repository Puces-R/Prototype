<%@ Page Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="GestionCommandesVendeur.aspx.cs"
    Inherits="Puces_R.GestionCommandesVendeur" %>

<%@ Register TagPrefix="lp" TagName="BoiteCommande" Src="~/Controles/BoiteCommande.ascx" %>
<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuVendeur.ascx" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="MenuClient1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/GestionCommandesVendeur.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="BarreCriteres">
    <span class="boiteListeDeroulante">
        Par page:
        <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
            <asp:ListItem Value="3" />
            <asp:ListItem Value="6" />
            <asp:ListItem Value="9" />
            <asp:ListItem Value="15" Selected="True" />
            <asp:ListItem Value="30" />
        </asp:DropDownList>
    </span>
    <span class="boiteListeDeroulante">
        Vendeur:
        <asp:DropDownList ID="ddlVendeur" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" />
    </span>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Items" runat="server">
    <div>
        <asp:MultiView runat="server" ID="mvCommandes" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <asp:DataList RepeatColumns="3" RepeatDirection="Horizontal" runat="server" ID="dlCommandes" OnItemDataBound="dlCommandes_OnItemDataBound">
                    <ItemTemplate>
                        <lp:BoiteCommande runat="server" ID="ctrCommande" SetBoutton="true"/>
                    </ItemTemplate>
                </asp:DataList>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <div class="messageCentral">Aucune commande ne vous a été demandé!</div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

<%--<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
   
    <div>
        <%--<asp:Repeater runat="server" ID="rptProduits" OnItemDataBound="rptProduits_ItemDataBound" OnItemCommand="rptProduits_ItemCommand">
            <ItemTemplate>
                <div class="rectangleItem rectangleComplet">
                   
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

                             <asp:Button runat="server" ID="btnMAJQuantite" Text="Changer le statut de la commande" CommandName="MAJQuantite" />
                            
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>
        <%--<asp:Repeater runat="server" ID="rptProduits" OnItemDataBound="rptProduits_ItemDataBound">
            OnItemCommand="rptProduits_ItemCommand">
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
                                    <th>
                                        Changer le statut de la Commande
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
                                    <td>
                                        <asp:Button runat="server" ID="btnCommande" Text="Changer le statut de la commande"
                                            CommandName="MAJQuantite" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>--%>

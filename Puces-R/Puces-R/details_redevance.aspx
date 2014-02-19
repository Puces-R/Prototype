<%@ Page Title="Détails de la redevance" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="details_redevance.aspx.cs" Inherits="Puces_R.details_redevance" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4_2.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BarreCriteres" runat="server">
        <div class="barreListesDeroulantes">
            <span class="boiteListeDeroulante">
                Recherche:
                <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                    <asp:ListItem Text="Client/Vendeur" />
                </asp:DropDownList>
                <asp:TextBox ID="txtCritereRecherche" runat="server" />
                <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
            </span>
            <span class="boiteListeDeroulante">
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Client/vendeur" />
                    <asp:ListItem Text="Date de vente" />
                    <asp:ListItem Text="Montant" Selected="True" />
                </asp:DropDownList>
            </span>
            <span class="boiteListeDeroulante">
                Par page:
                <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                    <asp:ListItem Value="25" />
                    <asp:ListItem Value="50" />
                    <asp:ListItem Value="75" />
                    <asp:ListItem Value="100" />
                </asp:DropDownList>
            </span>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Items" runat="server">
    <!--<div class="titre_sec">Demandes de vendeurs</div>-->
    <div style="font-size: small; width: 70%; margin-left: 11%;">
        <div id="div_msg" runat="server"></div>
        <asp:MultiView runat="server" ID="mv_commandes">
            <asp:View runat="server"  ID="view_vendeur">
                <table border="0" width="100%" cellpadding="7" cellspacing="2" >
                    <tr class="rectangleItem hautRectangle" >
                        <th>#</th>
                        <th>Client</th>
                        <th>Redevance</th>
                        <th>Date de vente</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptDetailsRedevance" OnItemDataBound="rptDetailsRedevance_ItemDataBound">
                        <ItemTemplate>                        
                            <tr class="rectangleItem basRectangle">
                                <td style="padding-top: 1%; padding-bottom: 1%;"><asp:LinkButton runat="server" ID="lbl_num" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                                <td><asp:LinkButton runat="server" ID="lbl_nom_client" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                                <td class="montant" ><asp:LinkButton runat="server" ID="lbl_redevance" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                                <td><asp:LinkButton runat="server" ID="date_vente" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:View>
            <asp:View runat="server"  ID="view_client">
                <table border="0" width="100%" cellpadding="7" cellspacing="2" >
                    <tr class="rectangleItem hautRectangle" >
                        <th>#</th>
                        <th>Vendeur</th>
                        <th>Montant</th>
                        <th>Date de vente</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptDetailsCommande" OnItemDataBound="rptDetailsCommande_ItemDataBound">
                        <ItemTemplate>                        
                            <tr class="rectangleItem basRectangle">
                                <td style="padding-top: 1%; padding-bottom: 1%;"><asp:LinkButton runat="server" ID="lbl_num" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                                <td><asp:LinkButton runat="server" ID="lbl_nom_client" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                                <td class="montant" ><asp:LinkButton runat="server" ID="lbl_redevance" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                                <td><asp:LinkButton runat="server" ID="date_vente" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    <div style="float: right; text-align: left; width: 15%;"><h3><asp:Label ID="lbl_total" runat="server" /></h3></div>
</asp:Content>

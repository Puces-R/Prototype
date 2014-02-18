<%@ Page Title="Redevance de ce mois" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="vendeur_redevance.aspx.cs" Inherits="Puces_R.vendeur_redevance" EnableEventValidation="false" %>
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
                    <asp:ListItem Text="Nom d'affaire" />
                </asp:DropDownList>
                <asp:TextBox ID="txtCritereRecherche" runat="server" />
                <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
            </span>
            <span class="boiteListeDeroulante">
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Nom d'affaire" />
                    <asp:ListItem Text="Date de paiement" />
                    <asp:ListItem Text="Montant dû" Selected="True" />
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
    <div id="div_msg" runat="server"></div>
    <div style="font-size: small; width: 70%; margin: auto;">
        <table border="0"  width="100%" cellpadding="7" cellspacing="2" >
            <tr class="rectangleItem hautRectangle" >
                <th>#</th>
                <th>Nom d'affaires</th>
                <th>Nom du vendeur</th>
                <th>Montant dû</th>
                <th>Date de paiement</th>
                <th>Actions</th>
            </tr>
            <asp:Repeater runat="server" ID="rptRedevances" OnItemDataBound="rptRedevances_ItemDataBound" >
                <ItemTemplate>
                    <tr class="rectangleItem basRectangle"  runat="server" id="ligne_histo" >
                        <td class="td_liste"><asp:LinkButton runat="server" ID="lbl_num"  OnCommand="voir_details_redevance" ToolTip="Voir les détails des commandes de ce mois" /></td>
                        <td><asp:LinkButton runat="server" ID="lbl_nom_affaire"  OnCommand="voir_details_redevance" ToolTip="Voir les détails des commandes de ce mois" /></td>
                        <td><asp:LinkButton runat="server" ID="lbl_nom_vendeur" OnCommand="voir_details_redevance" ToolTip="Voir les détails des commandes de ce mois"  /></td>
                        <td class="montant"><asp:LinkButton runat="server" ID="lbl_montant_du"  OnCommand="voir_details_redevance" ToolTip="Voir les détails des commandes de ce mois" /></td>                        
                        <td><asp:LinkButton runat="server" ID="lbl_date_paiement"  OnCommand="voir_details_redevance" ToolTip="Voir les détails des commandes de ce mois" /></td>                        
                        <td><asp:Button runat="server" ID="btn_enregistrer_paiement" OnCommand="enregistrer_paiement" ToolTip="Enregister paiement" Text="Enregister la reception du paiement" /></td>
                    </tr>
                </ItemTemplate>                
            </asp:Repeater>
        </table>
    </div>
</asp:Content>

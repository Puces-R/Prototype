﻿<%@ Page Title="Détails de la redevence" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="details_redevence.aspx.cs" Inherits="Puces_R.details_redevence" EnableEventValidation="false" %>
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
                    <asp:ListItem Text="Client" />
                </asp:DropDownList>
                <asp:TextBox ID="txtCritereRecherche" runat="server" />
                <asp:Button runat="server" Text="Go" ID="btnRecherche" OnClick="AfficherPremierePage" />
            </span>
            <span class="boiteListeDeroulante">
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Client" />
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
    <div id="div_msg" runat="server"></div>
    <div id="div_chck">    
        <div style="font-size: small;">
            <table border="0" width="100%" cellpadding="5" cellspacing="2" >
                <tr class="rectangleItem hautRectangle" >
                    <th>#</th>
                    <th>Client</th>
                    <th>Redevence</th>
                    <th>Date de vente</th>
                    <th></th>
                </tr>
                <asp:Repeater runat="server" ID="rptDetailsRedevence" OnItemDataBound="rptDetailsRedevence_ItemDataBound" >
                    <ItemTemplate>                        
                        <tr class="rectangleItem basRectangle">
                            <td><asp:Label runat="server" ID="lbl_num" /></td>
                            <td><asp:label runat="server" ID="lbl_nom_client" /></td>
                            <td>$<asp:Label runat="server" ID="lbl_redevance" /></td>
                            <td><asp:Label runat="server" ID="date_vente" /></td>
                            <td><asp:Button runat="server" ID="btn_voir_details_commande_redevance" OnCommand="voir_details_commande_redevance" ToolTip="Voir les détails de cette commande" Text="Détails" /> </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
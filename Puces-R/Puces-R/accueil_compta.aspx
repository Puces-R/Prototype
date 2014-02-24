<%@ Page Title="Suivi des redevances mensuelles" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="accueil_compta.aspx.cs" Inherits="Puces_R.accueil_compta" EnableEventValidation="false" %>
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
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                    <asp:ListItem Text="Montant attendu" />
                    <asp:ListItem Text="Montant reçu" />
                    <asp:ListItem Text="Montant dû" />
                    <asp:ListItem Text="Mois" Selected="True" />
                </asp:DropDownList>
                <asp:DropDownList ID="ddlOrdre" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                    <asp:ListItem Text="Croissant" Value=" ASC " />
                    <asp:ListItem Text="Décroissant" Value=" DESC " />
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
    <div style="font-size: small; width: 70%; margin: auto;">
    <div id="div_msg" runat="server"></div>
        <table border="0"  width="100%" cellpadding="7" cellspacing="2" >
            <tr class="rectangleItem hautRectangle" >
                <th>#</th>
                <th>Mois</th>
                <th>Montant attendu</th>
                <th>Montant reçu</th>
                <th>Montant dû</th>
            </tr>
            <asp:Repeater runat="server" ID="rptMois" OnItemDataBound="rptMois_ItemDataBound" >
                <ItemTemplate>
                    <tr class="rectangleItem basRectangle" >
                        <td class="td_liste"><asp:LinkButton runat="server" ID="lbl_num"  OnCommand="voir_redevances_mois" ToolTip="Cliquez pour voir/modifier les redevances de ce mois" /></td>
                        <td><asp:LinkButton runat="server" ID="lbl_mois"  OnCommand="voir_redevances_mois" ToolTip="Cliquez pour voir/modifier les redevances de ce mois" /></td>
                        <td class="montant"><asp:LinkButton runat="server" ID="lbl_attendu"  OnCommand="voir_redevances_mois" ToolTip="Cliquez pour voir/modifier les redevances de ce mois" /></td>
                        <td class="montant"><asp:LinkButton runat="server" ID="lbl_recu"  OnCommand="voir_redevances_mois" ToolTip="Cliquez pour voir/modifier les redevances de ce mois" /></td>
                        <td class="montant"><asp:LinkButton runat="server" ID="lbl_du"  OnCommand="voir_redevances_mois" ToolTip="Cliquez pour voir/modifier les redevances de ce mois" /></td>
                    </tr>
                </ItemTemplate>                
            </asp:Repeater>
        </table>
    </div>
</asp:Content>

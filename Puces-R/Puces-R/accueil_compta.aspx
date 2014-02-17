<%@ Page Title="Gérer des redevences de vendeurs" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="accueil_compta.aspx.cs" Inherits="Puces_R.accueil_compta" EnableEventValidation="false" %>
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
                    <asp:ListItem Text="Date de demande" />
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
                <th>Montant total dû</th>
            </tr>
            <asp:Repeater runat="server" ID="rptRetard" OnItemDataBound="rptRetard_ItemDataBound" OnItemCommand="voir_histo" >
                <ItemTemplate>
                    <tr class="rectangleItem basRectangle" >
                        <td class="td_liste"><asp:LinkButton runat="server" ID="lbl_num"  OnCommand="voir_histo" ToolTip="Cliquez pour voir/modifier l'historique de paiement de ce vendeur" /></td>
                        <td><asp:LinkButton runat="server" ID="lbl_nom_affaire"  OnCommand="voir_histo" ToolTip="Cliquez pour voir/modifier l'historique de paiement de ce vendeur" /></td>
                        <td><asp:LinkButton runat="server" ID="lbl_nom_vendeur" OnCommand="voir_histo" ToolTip="Cliquez pour voir/modifier l'historique de paiement de ce vendeur"  /></td>
                        <td><asp:LinkButton runat="server" ID="lbl_montant_du"  OnCommand="voir_histo" ToolTip="Cliquez pour voir/modifier l'historique de paiement de ce vendeur" /></td>
                    </tr>
                </ItemTemplate>                
            </asp:Repeater>
        </table>
    </div>
</asp:Content>

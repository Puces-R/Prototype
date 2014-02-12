<%@ Page Title="Historique de payement des redevences du vendeur" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="histo_redevance_vendeur.aspx.cs" Inherits="Puces_R.histo_redevance_vendeur" EnableEventValidation="false" %>
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
                    <asp:ListItem Text="Mois" Selected="True" />
                    <asp:ListItem Text="Date de paiement" />
                    <asp:ListItem Text="Montant dû" />
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
                    <th>Mois</th>
                    <th>Montant</th>
                    <th>Date de paiement</th>
                    <th></th>
                </tr>
                <asp:Repeater runat="server" ID="rptRetard" OnItemDataBound="rptRetard_ItemDataBound" >
                    <ItemTemplate>                        
                        <tr class="rectangleItem basRectangle" runat="server" id="ligne_histo" >
                            <td><asp:Label runat="server" ID="lbl_num" /></td>
                            <td><asp:label runat="server" ID="lbl_mois" /></td>
                            <td>$<asp:Label runat="server" ID="lbl_montant" /></td>
                            <td><asp:Label runat="server" ID="date_paiement" /></td>
                             <td>
                                <asp:Button runat="server" ID="btn_voir_details_redevence" OnCommand="voir_details_redevence" ToolTip="Voir les détails des commandes de ce mois" Text="Détails" /> 
                                <asp:Button runat="server" ID="btn_enregistrer_paiement" OnCommand="enregistrer_paiement" ToolTip="Enregister paiement" Text="Enregister la reception du paiement" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

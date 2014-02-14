<%@ Page Title="Gérer les nouvelles demandes de vendeurs" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="gerer_demandes_vendeurs.aspx.cs" Inherits="Puces_R.gerer_demandes_vendeurs" EnableEventValidation="false" %>
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
                    <asp:ListItem Text="Nom d'affaires" />
                    <asp:ListItem Text="Date de demande" />
                </asp:DropDownList>
            </span>
            <span class="boiteListeDeroulante">
                Par page:
                <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
                    <asp:ListItem Value="6" />
                    <asp:ListItem Value="12" />
                    <asp:ListItem Value="18" />
                    <asp:ListItem Value="24" />
                    <asp:ListItem Value="30" />
                    <asp:ListItem Value="50" />
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
                    <th>Nom d'affaires</th>
                    <th>Nom du vendeur</th>
                    <th>Date de demande</th>
                    <th></th>
                </tr>
                <asp:Repeater runat="server" ID="rptDemandes" OnItemDataBound="rptDemandes_ItemDataBound" >
                    <ItemTemplate>                        
                        <tr class="rectangleItem basRectangle">
                            <td><asp:Label runat="server" ID="lbl_num" /></td>
                            <td><asp:Label runat="server" ID="lbl_nom_affaire" /></td>
                            <td><asp:Label runat="server" ID="lbl_nom_vendeur" /></td>
                            <td><asp:Label runat="server" ID="date_demande" /></td>
                            <td>
                                <asp:Button id="btn_accepter" runat="server" Text="Accepter" OnCommand="acceptation_demande" ToolTip="Accepter la demande de ce vendeur" />
                                <asp:Button id="btn_refuser" runat="server" Text="Refuser" OnCommand="refus_demande" ToolTip="Refuser la demande de ce vendeur" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

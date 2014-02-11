<%@ Page Title="Gérer les nouvelles demandes de vendeurs" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="gerer_demandes_vendeurs.aspx.cs" Inherits="Puces_R.gerer_demandes_vendeurs" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Produits.css" />
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
        <div style="font-size: small;" >
            <asp:DataList RepeatColumns="2" RepeatDirection="Horizontal" runat="server" ID="rptDemandes" OnItemDataBound="rptDemandes_ItemDataBound" OnItemCommand="rptDemandes_ItemCommand">
                <ItemTemplate>
                    <div>
                        <div class="rectangleItem hautRectangle" onclick="afficheOuMasqueInfoVendeur(this);">
                            <asp:Label runat="server" ID="titre_demande" />
                        </div>
                        <div class="rectangleItem basRectangle">
                            <table class="tableTitreValeur">
                                <colgroup>
                                    <col width="50%" />
                                    <col width="50%" />
                                </colgroup>
                                <tr >
                                    <th>Adresse:</th>
                                    <td><asp:Label runat="server" ID="addr_demande" /></td>
                                </tr>
                                <tr>
                                    <th>Téléphone:</th>
                                    <td><asp:Label runat="server" ID="tels_demande" /></td>
                                </tr>
                                <tr>
                                    <th>Courriel:</th>
                                    <td><asp:Label runat="server" ID="courriel_demande" /></td>
                                </tr>
                                <tr>
                                    <th>Poids maximal:</th>
                                    <td><asp:Label runat="server" ID="charge_max_demande" /></td>
                                </tr>
                                <tr>
                                    <th>Livraison gratuite:</th>
                                    <td><asp:Label runat="server" ID="livraison_gratuite" /></td>
                                </tr>
                                <tr>
                                    <th>Date de demande:</th>
                                    <td><asp:Label runat="server" ID="date_demande" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button id="btn_accepter" runat="server" Text="Accepter" OnCommand="acceptation_demande" />
                                    </td>
                                    <td>
                                        <asp:Button id="btn_refuser" runat="server" Text="Refuser" OnCommand="refus_demande" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
</asp:Content>

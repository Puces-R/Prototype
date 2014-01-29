<%@ Page Title="Gérer l'inactivité des vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gerer_inactivite_vendeurs.aspx.cs" Inherits="Puces_R.gerer_inactivite_vendeurs" %>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/site.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <div class="titre_sec">Gestion de l'inactivité des vendeurs</div>

    <div id="categorie_innactivité">
        <div id="titre_cate" class="titre_tab3">
            <table border="0" width="100%">
                <tr>
                    <td>Depuis au moins un an</td>
                    <td class="a_droite" ><asp:Button id="ge" runat="server" text="Tout supprimer" CssClass="boutton" /></td>
                </tr>
            </table>
        </div>

        <div id="liste_vendeurs_innactifs" class="cont_categorie">
            <asp:Repeater runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                <ItemTemplate>      
                    <div class="vendeur_innactif" >
                        <table border="0" width="100%">
                        <tr>
                            <td><asp:Label runat="server" ID="titre_inactif1" /></td>
                            <td class="a_droite" >
                                <input type="button" value="Désactiver" class="boutton" /> <input type="button" value="Voir détails" class="boutton" />                
                            </td>
                        </tr>
                        </table>
                        <table border="0" width="100%" class="table_conf_suppr">
                            <colgroup>
                                <col width="50%"/>
                                <col width="50%"/>
                            </colgroup>
                            <tr><td colspan="2" ><h2 class="center">Voulez vous vraiment désactiver ce vendeur</h2></td></tr>
                            <tr >
                                <td align="right">Adresse:</td>
                                <td><asp:Label runat="server" ID="addr_inactif1" /></td>
                            </tr>
                            <tr>
                                <td align="right">Téléphone:</td>
                                <td><asp:Label runat="server" ID="tels_inactif1" /></td>
                            </tr>
                            <tr>
                                <td align="right">Courriel:</td>
                                <td><asp:Label runat="server" ID="courriel_inactif1" /></td>
                            </tr>
                            <tr>
                                <td align="right">Taux de facturation:</td>
                                <td><asp:Label runat="server" ID="taux_facturation_inactif1" /></td>
                            </tr>
                            <tr>
                                <td align="right">Charge maximale de livraison:</td>
                                <td><asp:Label runat="server" ID="charge_max_inactif1" /></td>
                            </tr>
                            <tr>
                                <td align="right">Livraison gratuite:</td>
                                <td><asp:Label runat="server" ID="livraison_gratuite_inactif1" /></td>
                            </tr>
                            <tr>
                                <td align="right">Inactif depuis:</td>
                                <td><asp:Label runat="server" ID="date_inactif1" /></td>
                            </tr>
                            <tr>
                                <td align="right"><input type="button" value="Désactiver" onclick="afficher_accepter(this);" class="boutton" /></td>
                                <td><input type="button" value="Annuler" onclick="afficher_refuser(this);" class="boutton" /></td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
</asp:Content>
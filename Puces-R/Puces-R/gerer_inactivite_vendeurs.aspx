<%@ Page Title="Gérer l'inactivité des vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
CodeBehind="gerer_inactivite_vendeurs.aspx.cs" Inherits="Puces_R.gerer_inactivite_vendeurs" EnableEventValidation="false"%>

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
    <!-- <div class="titre_sec">Gestion de l'inactivité des vendeurs</div> -->
    <div class="barreListesDeroulantes">
            <span class="boiteListeDeroulante">
                Recherche:
                <asp:DropDownList ID="ddlTypeRecherche" runat="server">
                    <asp:ListItem Text="Nom d'affaire" />
                </asp:DropDownList>
                <asp:TextBox ID="txtCritereRecherche" runat="server" />
                <asp:Button runat="server" Text="Go" ID="btnRecherche" />
            </span>
            <span class="boiteListeDeroulante">
                Trier par:
                <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true">
                    <asp:ListItem Text="Numéro" />
                    <asp:ListItem Text="Nom d'affaire" />
                    <asp:ListItem Text="Date de demande" />
                </asp:DropDownList>
            </span>
            <span class="boiteListeDeroulante">
                Par page:
                <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="5" />
                    <asp:ListItem Value="10" />
                    <asp:ListItem Value="15" Selected="True" />
                    <asp:ListItem Value="20" />
                    <asp:ListItem Value="25" />
                    <asp:ListItem Value="50" />
                </asp:DropDownList>
            </span>
        </div>
        <div class="lignePointilleHorizontale"></div>
    <div id="categorie_innactivite">
        <div id="titre_cate" class="titre_tab3" onclick="afficheOuMasqueBaliseBlock('liste_vendeurs_innactifs' + 1)">
            <table border="0" width="100%">
                <tr>
                    <td>Depuis au moins un an</td>
                    <td class="a_droite" ><asp:Button id="ge" runat="server" text="Tout désactiver" CssClass="boutton" /></td>
                </tr>
            </table>
        </div>

        <div id="liste_vendeurs_innactifs1" class="cont_categorie">
            <asp:Repeater runat="server" ID="rptInnactifs1" OnItemDataBound="rptInnactifs1_ItemDataBound" >
                <ItemTemplate>      
                    <div class="vendeur_innactif" >
                        <table border="0" width="100%">
                        <tr class="titre_vendeur_inactif1" onclick="afficheOuMasqueInfoInactif(this)" id="test">
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
                                <td align="right"><asp:Button id="btn_desactiver" runat="server" text="Désactiver" CssClass="boutton" OnCommand="desactiver_vendeur"/></td>
                                <td><input type="button" value="Annuler" onclick="annuler_desactiver(this);" class="boutton" /></td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>     

        </div>
        <div id="Div2" class="titre_tab3" onclick="afficheOuMasqueBaliseBlock('liste_vendeurs_innactifs' + 1)">
            <table border="0" width="100%">
                <tr>
                    <td>Depuis au moins deux ans</td>
                    <td class="a_droite" ><asp:Button id="Button2" runat="server" text="Tout désactiver" CssClass="boutton" /></td>
                </tr>
            </table>
        </div>
        <div id="Div3" class="titre_tab3" onclick="afficheOuMasqueBaliseBlock('liste_vendeurs_innactifs' + 1)">
            <table border="0" width="100%">
                <tr>
                    <td>Depuis au moins trois ans</td>
                    <td class="a_droite" ><asp:Button id="Button3" runat="server" text="Tout désactiver" CssClass="boutton" /></td>
                </tr>
            </table>
        </div>
        <div id="Div4" class="titre_tab3" onclick="afficheOuMasqueBaliseBlock('liste_vendeurs_innactifs' + 1)">
            <table border="0" width="100%">
                <tr>
                    <td>Depuis le début</td>
                    <td class="a_droite" ><asp:Button id="Button4" runat="server" text="Tout désactiver" CssClass="boutton" /></td>
                </tr>
            </table>
        </div>
    </div>
</div>
</asp:Content>
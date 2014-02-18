<%@ Page Title="Gérer le client" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="client.aspx.cs" Inherits="Puces_R.client" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
    <link rel="stylesheet" type="text/css" href="CSS/jchart/jchartfx.css" />
    <script type="text/javascript" src="lib/js/jchart/jchartfx.system.js"></script>
    <script type="text/javascript" src="lib/js/jchart/jchartfx.coreVector.js"></script>
    <script type="text/javascript" src="lib/js/jchart/jchartfx.coreVector3d.js"></script>
    <script type="text/javascript" src="lib/js/jchart/jchartfx.animation.js"></script>
    <script type="text/javascript" src="lib/js/jchart/jchartfx.advanced.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_englobant">
        <div class="panneau pnlGauche">
            <h2>Options</h2>
            <ul>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="0" Text="Informations" ToolTip="Afficher les informations de ce client" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="1" Text="Statistiques" ToolTip="Afficher les statistiques de ce client" /></li>
            </ul>
            <h2>Actions</h2>
            <ul>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="2" Text="Envoyer un message interne à ce client" ToolTip="Envoyer d'un message à ce client via le systeme de messagerie interne du site" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="3" Text="Envoyer un courriel a ce client" ToolTip="Envoyer un courriel vers l'adresse email de ce client" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="4" Text="Désactiver ce client" ToolTip="Rendre ce client innactif" /></li>
            </ul>
        </div>
        <div class="panneau pnlDroite">
            <asp:MultiView runat="server" ID="mvVendeur">
                <asp:View ID="View1" runat="server">
                    <div style="width:600px"> &nbsp;</div>
                    <h2>Informations</h2>
                    <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="lbl_nom_complet" Text="40"/>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <table class="tableProduits" style="width:95%">
                            <colgroup>
                                <col width="50%" />
                                <col width="50%" />
                            </colgroup>
                            <tr>
                                <th>Adresse</th>
                                <td><asp:Label runat="server" ID="lbl_adresse" CssClass="info_cellule"/></td>
                            </tr>
                            <tr>
                                <th>Numéros de téléphone</th>
                                <td>
                                    <asp:Label runat="server" ID="lbl_tel1" CssClass="info_cellule"/><br />
                                    <asp:Label runat="server" ID="lbl_tel2" CssClass="info_cellule" />
                                </td>
                            </tr>
                            <tr>
                                <th>Adresse courriel</th>
                                <td><asp:Label runat="server" ID="lbl_courriel" CssClass="info_cellule"/></td>
                            </tr>
                            <tr>
                                <th>Date d'inscription</th>
                                <td><asp:Label runat="server" ID="lbl_date_insc" CssClass="info_cellule" /></td>
                            </tr>
                            <tr>
                                <th>Dernière mise à jour du profil</th>
                                <td><asp:Label runat="server" ID="lbl_date_maj" CssClass="info_cellule"/></td>
                            </tr>
                            <tr>
                                <th>Nombre de connexions</th>
                                <td><asp:Label runat="server" ID="lbl_nb_connexion" CssClass="info_cellule" /></td>
                            </tr>
                            <tr>
                                <th>Statut</th>
                                <td><asp:Label runat="server" ID="lbl_statut" CssClass="info_cellule"/></td>
                            </tr>
                        </table>
                    </div>
                </asp:View>  
                <asp:View ID="View2" runat="server">
                    <div style="width:600px"> &nbsp;</div>
                    <h2>Statistiques</h2>
                    <h3>Nombre total de vendeurs: <asp:Label runat="server" ID="lbl_nb_vendeurs" CssClass="info_cellule" /></h3>
                    <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="Label1" Text="Vendeurs favoris"/>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <table class="table_avec_ligne" style="width:95%">
                            <colgroup>
                                <col width="50%" />
                                <col width="50%" />
                            </colgroup>
                            <tr>
                                <th>Vendeur</th>
                                <th>Total des commandes</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptFavVendeurs" OnItemDataBound="rptFavVendeurs_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:Label runat="server" ID="nom_vendeur_favoris" CssClass="info_cellule" /></td>
                                        <td class="montant" ><asp:Label runat="server" ID="total_commande_vendeur_favoris" /></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>

                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Total des commandes des derniers mois</td>
                            <td align="right" >
                                Nombre de mois
                                <asp:DropDownList ID="ddlNbMois_c1" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="generer_stat">
                            </asp:DropDownList>
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_c1" style="width:100%;height:375px;display:inline-block;margin: 0 auto;"></div>                       
                    </div>

                     <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Nombre de commandes par mois</td>
                            <td align="right" >
                                Nombre de mois
                                <asp:DropDownList ID="ddlNbMois_c2" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="generer_stat">
                            </asp:DropDownList>
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_c2" style="width:100%;height:375px;display:inline-block;margin: 0 auto;"></div>               
                    </div>
                </asp:View>              
            </asp:MultiView>
        </div>
    </div>
</asp:Content>

<%@ Page Title="Gérer le vendeur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vendeur.aspx.cs" Inherits="Puces_R.vendeur" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="Etoiles" Src="~/Controles/Etoiles.ascx" %>

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
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="0" Text="Informations" ToolTip="Afficher les informations de ce vendeur" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="1" Text="Statistiques" ToolTip="Afficher les statistiques de ce vendeur" /></li>
            </ul>
            <h2>Actions</h2>
            <ul>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="3" Text="Envoyer un message interne à ce vendeur" ToolTip="Envoyer un message à ce vendeur via le système de messagerie interne du site" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="4" Text="Envoyer un courriel a ce vendeur" ToolTip="Envoyer un courriel vers l'adresse email de ce vendeur" /></li>
                <li><asp:LinkButton id="lb_desactiver" runat="server" OnCommand="changer_view" CommandArgument="5" Text="Désactiver ce vendeur" ToolTip="Rendre ce vendeur innactif" /></li>
                <li><asp:LinkButton id="lb_voir_histo" runat="server" OnCommand="changer_view" CommandArgument="6" Text="Gérer les paiements de ce vendeur" ToolTip="Voir l'historique de paiements de ce vendeur" /></li>
                <li id="li_modifier_taux" runat="server" ><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="7" Text="Modifier le taux de redevance de ce vendeur" ToolTip="La modification du taux de redevance de ce vendeur est possible car ce dernier n'a jamais eu de commande" /></li>
            </ul>
        </div>
        <div class="panneau pnlDroite">
            <div id="div_msg" runat="server"></div>
            <asp:MultiView runat="server" ID="mvVendeur">
                <asp:View ID="View1" runat="server">
                    <div style="width:600px"> &nbsp;</div>
                    <h2>Informations</h2>
                    <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="lbl_nom_complet" />
                    </div>
                    <div class="rectangleItem basRectangle">
                        <table class="tableProduits" style="width:95%">
                            <colgroup>
                                <col width="50%" />
                                <col width="50%" />
                            </colgroup>
                            <tr>
                                <th>Adresse</th>
                                <td><asp:Label runat="server" ID="lbl_adresse" /></td>
                            </tr>
                            <tr>
                                <th>Numéros de téléphone</th>
                                <td>
                                    <asp:Label runat="server" ID="lbl_tel1" /><br />
                                    <asp:Label runat="server" ID="lbl_tel2" />
                                </td>
                            </tr>
                            <tr>
                                <th>Adresse courriel</th>
                                <td><asp:Label runat="server" ID="lbl_courriel"  /></td>
                            </tr>
                            <tr>
                                <th>Charge maximale de livraison</th>
                                <td><asp:Label runat="server" ID="lbl_charge_max"/></td>
                            </tr>
                            <tr>
                                <th>Montant minimal de livraison gratuite</th>
                                <td><asp:Label runat="server" ID="lbl_livraison_gratuite" /></td>
                            </tr>
                            <tr>
                                <th>Vend avec taxes</th>
                                <td><asp:Label runat="server" ID="lbl_taxes" /></td>
                            </tr>
                            <tr>
                                <th>Taux de redevance</th>
                                <td><asp:Label runat="server" ID="lbl_taux_redevance"  /></td>
                            </tr>
                            <tr>
                                <th>Date d'inscription</th>
                                <td><asp:Label runat="server" ID="lbl_date_insc" /></td>
                            </tr>
                            <tr>
                                <th>Dernière mise à jour du profil</th>
                                <td><asp:Label runat="server" ID="lbl_date_maj"  /></td>
                            </tr>
                            <tr>
                                <th>Statut</th>
                                <td><asp:Label runat="server" ID="lbl_statut" /></td>
                            </tr>
                            <tr>
                                <th>Moyenne de la côte des produits</th>
                                <td><lp:Etoiles runat="server" ID="ctr_etoiles" LienActive="true" /> <asp:Label runat="server" ID="lbl_commentaire_etoiles" /></td>
                            </tr>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div style="width:600px"> &nbsp;</div>
                    <h2>Statistiques</h2>
                    <h3>Nombre total de clients: <asp:Label runat="server" ID="lbl_nb_clients" CssClass="info_cellule" /></h3>
                    <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="Label1" Text="Cinq meilleurs clients"/>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <table class="table_avec_ligne" style="width:95%">
                            <colgroup>
                                <col width="50%" />
                                <col width="50%" />
                            </colgroup>
                            <tr>
                                <th>Nom</th>
                                <th>Total de commandes</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptBestClients" OnItemDataBound="rptBestClients_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:Label runat="server" ID="nom_meilleur_client" CssClass="info_cellule" /></td>
                                        <td class="montant" ><asp:Label runat="server" ID="total_commande_meilleur_client" /></td>
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
                                <asp:DropDownList ID="ddlNbMois" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerDonneesGraphiques">
                                <asp:ListItem Value="5" Selected="True" style="color: Black;" />
                                <asp:ListItem Value="6" style="color: Black;" />
                                <asp:ListItem Value="7" style="color: Black;" />
                                <asp:ListItem Value="8" style="color: Black;" />
                                <asp:ListItem Value="9" style="color: Black;" />
                                <asp:ListItem Value="10" style="color: Black;" />
                            </asp:DropDownList>
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="ChartDiv1" style="width:100%;height:375px;display:inline-block;margin: 0 auto;"></div>                       
                    </div>

                     <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="Label3" Text="Total de clients"/>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="ChartDiv2" style="width:100%;height:375px;display:inline-block;margin: 0 auto;"></div>               
                    </div>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <div style="width:600px"> &nbsp;</div>
                    <h2>Modifiaction du taux de redevance</h2>
                    Taux de redevance actuel: <asp:Label ID="lbl_ancien_taux" runat="server" /> <br /><br />
                    Entrez le nouveau taux de redevance:
                    <asp:TextBox runat="server" id="tb_nouveau_taux_redevance" MaxLength="5"  Width="55" step="0.01" Min="0" Max="100" /> <br />
                    <asp:Button ID="btn_changer_taux" runat="server" OnClick="changer_taux" Text="Enregistrer"/> <br />                    
                    <asp:RequiredFieldValidator
                        runat="server"
                        ControlToValidate="tb_nouveau_taux_redevance"
                        ErrorMessage="Veuillez entrer le taux de redevance"
                        Display="Dynamic" />
                    <asp:RegularExpressionValidator
                        runat="server"
                        id="regex_taux"
                        ControlToValidate="tb_nouveau_taux_redevance"
                        Display="Dynamic"
                        ErrorMessage="Format: --.-- dans l'intervalle ]0,100["
                        EnableClientScript="true" 
                        ValidationExpression="^(?!(00\.00))(\d{1,2}\.\d{1,2})$" />  
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>

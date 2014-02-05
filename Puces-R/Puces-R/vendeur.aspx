<%@ Page Title="Gérer le vendeur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vendeur.aspx.cs" Inherits="Puces_R.vendeur" %>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_englobant">
        <div class="panneau pnlGauche">
            <h2>Options</h2>
            <ul>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="0" Text="Informations" ToolTip="Afficher les informations de ce vendeur" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="1" Text="Statistiques" ToolTip="Afficher les statistiques de ce vendeur" /></li>
                <li><asp:LinkButton id="lb_vendeur" runat="server" OnCommand="selectionner_vendeur" Text="Historique de payement" ToolTip="Voir l'historique de payements de ce vendeur" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="2" Text="Actions" ToolTip="Afficher les actions que vous pouvez appliquer à ce vendeur" /></li>
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
                                <td><asp:Label runat="server" ID="lbl_adresse" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Numéros de téléphone</th>
                                <td>
                                    <asp:Label runat="server" ID="lbl_tel1" CssClass="info_cellule" Text="40"/><br />
                                    <asp:Label runat="server" ID="lbl_tel2" CssClass="info_cellule" Text="40"/>
                                </td>
                            </tr>
                            <tr>
                                <th>Adresse courriel</th>
                                <td><asp:Label runat="server" ID="lbl_courriel" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Charge maximale de livraison</th>
                                <td><asp:Label runat="server" ID="lbl_charge_max" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Montant minimal de livraison gratuite</th>
                                <td><asp:Label runat="server" ID="lbl_livraison_gratuite" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Vend avec taxes</th>
                                <td><asp:Label runat="server" ID="lbl_taxes" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Taux de redevence</th>
                                <td><asp:Label runat="server" ID="lbl_taux_redevence" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Date d'inscription</th>
                                <td><asp:Label runat="server" ID="lbl_date_insc" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Dernière mise à jour du profil</th>
                                <td><asp:Label runat="server" ID="lbl_date_maj" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Statut</th>
                                <td><asp:Label runat="server" ID="lbl_statut" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div style="width:600px"> &nbsp;</div>
                    <h2>Statistiques</h2>
                    <h3>Nombre total de clients: <asp:Label runat="server" ID="lbl_nb_clients" CssClass="info_cellule" Text="40"/></h3>
                    <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="Label1" Text="Cinq meilleurs clients"/>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <table class="tableProduits" style="width:95%">
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
                                        <td><asp:Label runat="server" ID="nom_meilleur_client" CssClass="info_cellule" Text="40"/></td>
                                        <td><asp:Label runat="server" ID="total_commande_meilleur_client" CssClass="info_cellule" Text="40"/></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>

                    <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="Label2" Text="Total des commandes des cinq derniers mois"/>
                    </div>
                    <div class="rectangleItem basRectangle">
                        
                    </div>


                </asp:View>
                <asp:View ID="View3" runat="server">
                    <div style="width:600px"> &nbsp;</div>
                    <h2>Actions</h2>
                    <asp:Button runat="server" Text="Envoyer un message interne" ToolTip="Envoyer un message interne à ce vendeur" />
                    <asp:Button runat="server" Text="Envoyer un courriel" ToolTip="Envoyer un courriel à ce vendeur" />
                    <asp:Button runat="server" Text="Enregistrer le payement de la redevence mensuelle" ToolTip="Enregistrer le payement de la redevence mensuelle de ce vendeur" />
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>

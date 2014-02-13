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
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="0" Text="Envoyer un message interne à ce client" ToolTip="" /></li>
                <li><asp:LinkButton runat="server" OnCommand="changer_view" CommandArgument="1" Text="Envoyer un courriel a ce client" ToolTip="" /></li>
                <li><asp:LinkButton runat="server" Text="Désactiver ce client" ToolTip="" /></li>
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
                                <th>Date d'inscription</th>
                                <td><asp:Label runat="server" ID="lbl_date_insc" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Dernière mise à jour du profil</th>
                                <td><asp:Label runat="server" ID="lbl_date_maj" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Nombre de connexions</th>
                                <td><asp:Label runat="server" ID="lbl_nb_connexion" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                            <tr>
                                <th>Statut</th>
                                <td><asp:Label runat="server" ID="lbl_statut" CssClass="info_cellule" Text="40"/></td>
                            </tr>
                        </table>
                    </div>
                </asp:View>                
            </asp:MultiView>
        </div>
    </div>
</asp:Content>

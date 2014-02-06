<%@ Page Title="Statistiques & Rapports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualiser_stats_rapports.aspx.cs" Inherits="Puces_R.visualiser_stats_rapports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/Site.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="div_englobant">
        <div class="panneau pnlGauche">
            <h2>Affichage</h2>
            <div class="categories">
                <div>
                    <asp:Label runat="server" ID="lblCategorie" CssClass="categorie" Text="Vendeurs" />
                    <div class="vendeur">
                        <asp:Label runat="server" ID="hypVendeur" CssClass="lienProduitsVendeur" Text="Nombre total de vendeurs (40)"/> 
                    </div>
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label1" CssClass="lienProduitsVendeur" Text="Nouveaux vendeurs"/> 
                    </div>                    
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label3" CssClass="lienProduitsVendeur" Text="Nombre de clients par vendeur"/> 
                    </div>
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label2" CssClass="lienProduitsVendeur" Text="Total de commandes par client"/> 
                    </div>
                </div>
                <div>
                    <asp:Label runat="server" ID="Label4" CssClass="categorie" Text="Clients" />
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label5" CssClass="lienProduitsVendeur" Text="Nombre total de clients (40)"/> 
                    </div>
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label7" CssClass="lienProduitsVendeur" Text="Nombre de nouveaux clients"/> 
                    </div>
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label8" CssClass="lienProduitsVendeur" Text="Nombre de connexions des clients(40)"/> 
                    </div>
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label6" CssClass="lienProduitsVendeur" Text="Nombre de visites des vendeurs"/> 
                    </div>                    
                    <div class="vendeur">
                        <asp:Label runat="server" ID="Label9" CssClass="lienProduitsVendeur" Text="Total de commandes par vendeur"/> 
                    </div>
                </div>
            </div>
        </div>
        <div class="panneau pnlDroite">
            <div style="width:600px"> &nbsp;</div>
            <div class="rectangleItem hautRectangle">
                <asp:Label runat="server" ID="Label15" CssClass="categorie" Text="Nouveaux vendeurs" />
            </div>
            <div class="rectangleItem basRectangle">
                <table class="tableProduits">
                    <tr>
                        <th>Nouveaux vendeurs depuis 1 mois</th>
                        <td><asp:Label runat="server" ID="Label10" CssClass="lienProduitsVendeur" Text="40"/> </td>
                    </tr>
                    <tr>
                        <th>Nouveaux vendeurs depuis 3 mois</th>
                        <td><asp:Label runat="server" ID="Label11" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                    <tr>
                        <th>Nouveaux vendeurs depuis 6 mois</th>
                        <td><asp:Label runat="server" ID="Label12" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                    <tr>
                        <th>Nouveaux vendeurs depuis 12 mois</th>
                        <td><asp:Label runat="server" ID="Label13" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                    <tr>
                        <th>Nombre total de vendeurs</th>
                        <td><asp:Label runat="server" ID="Label14" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                </table>
            </div>
            <div class="rectangleItem hautRectangle">
                <asp:Label runat="server" ID="Label16" CssClass="categorie" Text="Nombre de clients par vendeur" />
            </div>
            <div class="rectangleItem basRectangle">
                <table class="tableProduits">
                    <tr>
                        <th>
                            Sélectionez un vendeur:
                            <asp:DropDownList runat="server">
                                <asp:ListItem>&nbsp;</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <td><asp:Label runat="server" ID="Label17" CssClass="lienProduitsVendeur" Text="40"/> </td>
                    </tr>
                </table>
            </div>
            <div class="rectangleItem hautRectangle">
                <asp:Label runat="server" ID="Label18" CssClass="categorie" Text="Total de commandes d'un vendeur par clients" />
                <span class="a_droite" style="float:right;">
                    Sélectionez un vendeur:
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem>&nbsp;</asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
            <div class="rectangleItem basRectangle">
                <table class="tableProduits">
                    <tr>
                        <th>Client1</th>
                        <td><asp:Label runat="server" ID="Label19" CssClass="lienProduitsVendeur" Text="$40"/> </td>
                    </tr>
                    <tr>
                        <th>Client2</th>
                        <td><asp:Label runat="server" ID="Label20" CssClass="lienProduitsVendeur" Text="$40"/> </td>
                    </tr>
                    <tr>
                        <th>Client3</th>
                        <td><asp:Label runat="server" ID="Label21" CssClass="lienProduitsVendeur" Text="$40"/> </td>
                    </tr>
                </table>
            </div>

            <!-- Clients -->
            <div class="rectangleItem hautRectangle">
                <asp:Label runat="server" ID="Label22" CssClass="categorie" Text="Nouveaux clients" />
            </div>
            <div class="rectangleItem basRectangle">
                <table class="tableProduits">
                    <tr>
                        <th>Nouveaux clients depuis 3 mois</th>
                        <td><asp:Label runat="server" ID="Label23" CssClass="lienProduitsVendeur" Text="40"/> </td>
                    </tr>
                    <tr>
                        <th>Nouveaux clients depuis 6 mois</th>
                        <td><asp:Label runat="server" ID="Label24" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                    <tr>
                        <th>Nouveaux clients depuis 9 mois</th>
                        <td><asp:Label runat="server" ID="Label25" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                    <tr>
                        <th>Nouveaux clients depuis 12 mois</th>
                        <td><asp:Label runat="server" ID="Label26" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                    <tr>
                        <th>Nombre total de clients</th>
                        <td><asp:Label runat="server" ID="Label27" CssClass="lienProduitsVendeur" Text="40"/></td>
                    </tr>
                </table>
            </div>

            <div class="rectangleItem hautRectangle">
                <asp:Label runat="server" ID="Label28" CssClass="categorie" Text="Nombre de connexions des clients" />
            </div>
            <div class="rectangleItem basRectangle">
                <table class="tableProduits">
                    <tr>
                        <th>
                            Sélectionez un client:
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem>&nbsp;</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <td><asp:Label runat="server" ID="Label29" CssClass="lienProduitsVendeur" Text="40"/> </td>
                    </tr>
                </table>
            </div>

            <div class="rectangleItem hautRectangle">
                <asp:Label runat="server" ID="Label30" CssClass="categorie" Text="Nombre de visite de vendeurs" />
            </div>
            <div class="rectangleItem basRectangle">
                <table class="tableProduits">
                    <tr>
                        <th>
                            Sélectionez un client:
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem>&nbsp;</asp:ListItem>
                            </asp:DropDownList>
                        </th>
                        <td><asp:Label runat="server" ID="Label31" CssClass="lienProduitsVendeur" Text="40"/> </td>
                    </tr>
                </table>
            </div>

            <div class="rectangleItem hautRectangle">
                <asp:Label runat="server" ID="Label32" CssClass="categorie" Text="Total de commandes d'un client par vendeurs" />
                <span class="a_droite" style="float:right;">
                    Sélectionez un client:
                    <asp:DropDownList ID="DropDownList4" runat="server">
                        <asp:ListItem>&nbsp;</asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
            <div class="rectangleItem basRectangle">
                <table class="tableProduits">
                    <tr>
                        <th>Client1</th>
                        <td><asp:Label runat="server" ID="Label33" CssClass="lienProduitsVendeur" Text="$40"/> </td>
                    </tr>
                    <tr>
                        <th>Client2</th>
                        <td><asp:Label runat="server" ID="Label34" CssClass="lienProduitsVendeur" Text="$40"/> </td>
                    </tr>
                    <tr>
                        <th>Client3</th>
                        <td><asp:Label runat="server" ID="Label35" CssClass="lienProduitsVendeur" Text="$40"/> </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

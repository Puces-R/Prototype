<%@ Page Title="Statistiques & Rapports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualiser_stats_rapports.aspx.cs" Inherits="Puces_R.visualiser_stats_rapports" %>
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
            <h2>Vendeurs</h2>
            <ul>
                <li><asp:LinkButton ID="LinkButton1" runat="server" OnCommand="chargerGraphiquev1" CommandArgument="v1" Text="Nouveaux vendeurs" ToolTip="Les nouveaux vendeurs depuis un période" /></li>
                <li><asp:LinkButton ID="LinkButton2" runat="server" OnCommand="chargerGraphiquev2" CommandArgument="v2" Text="Meilleurs vendeurs" ToolTip="Les vendeurs qui ont le plus de redevance" /></li>
                <li><asp:LinkButton ID="LinkButton7" runat="server" OnCommand="chargerGraphiquev3" CommandArgument="v2" Text="Vendeurs populaires" ToolTip="Les vendeurs qui ont le plus de visites de clients" /></li>
                <li><asp:LinkButton ID="LinkButton8" runat="server" OnCommand="chargerGraphiquev4" CommandArgument="v4" Text="Meilleurs vendeurs potentiels" ToolTip="Les vendeurs qui ont le plus de paniers non commandés" /></li>
                <li><asp:LinkButton ID="LinkButton9" runat="server" OnCommand="chargerGraphiquev0" CommandArgument="v0" Text="Divers" ToolTip="Statistiques diverses sur les vendeurs" /></li>
            </ul>
            <h2>Clients</h2>
            <ul>
                <li><asp:LinkButton ID="LinkButton3" runat="server" OnCommand="chargerGraphiquec1" CommandArgument="c1" Text="Nouveaux clients" ToolTip="Les nouveux clients depuis un période" /></li>
                <li><asp:LinkButton ID="LinkButton4" runat="server" OnCommand="chargerGraphiquec2" CommandArgument="c2" Text="Meilleurs clients" ToolTip="Les clients qui ont le plus de commande" /></li>
                <li><asp:LinkButton ID="LinkButton5" runat="server" OnCommand="chargerGraphiquec3" CommandArgument="c2" Text="Meilleurs visiteurs" ToolTip="Les clients qui visitent le plus les vendeurs" /></li>
                <li><asp:LinkButton ID="LinkButton6" runat="server" OnCommand="chargerGraphiquec4" CommandArgument="c4" Text="Meilleurs clients potentiels" ToolTip="Les clients qui ont le plus de paniers non commandés" /></li>
                <li><asp:LinkButton ID="LinkButton10" runat="server" OnCommand="chargerConnexionsc5" CommandArgument="c4" Text="Dernieres connexions" ToolTip="Les clients qui se sont récenmment connecté au site " /></li>
                <li><asp:LinkButton ID="LinkButton0" runat="server" OnCommand="chargerGraphiquec0" CommandArgument="c0" Text="Divers" ToolTip="Statistiques diverses sur les clients" /></li>
            </ul>
        </div>
        <div class="panneau pnlDroite">
            <div style="width:675px; height: 0px;"> &nbsp;</div>
            <asp:MultiView ID="mvStats" runat="server">
                <asp:View ID="v1" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Nouveaux vendeurs</td>
                            <td align="right" >
                                Nombre de mois
                                <asp:DropDownList ID="ddlNbMois_v1" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquev1" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_v1" style="width:100%;height:375px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="v2" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Vendeurs avec le plus de ventes</td>
                            <td align="right" >
                                Nombre de vendeurs
                                <asp:DropDownList ID="ddlNbVendeurs_v2" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquev2" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_v2" style="width:100%;height:650px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="v3" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Vendeurs les plus visités</td>
                            <td align="right" >
                                Nombre de vendeurs
                                <asp:DropDownList ID="ddlNbVendeurs_v3" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquev3" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_v3" style="width:100%;height:650px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="v4" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Meilleurs vendeurs potentiels</td>
                            <td align="right" >
                                Nombre de vendeurs
                                <asp:DropDownList ID="ddlNbVendeurs_v4" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquev4" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_v4" style="width:100%;height:650px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="v0" runat="server">
                    <div class="rectangleItem hautRectangle">Statistiques diverses sur les vendeurs</div>
                    <div class="rectangleItem basRectangle">
                        <table class="tableProduits" style="width:95%;">
                            
                            <tr><th colspan="2" align="center" >Nombre total de vendeurs :<asp:label runat="server" Text="" ID="lbl_total_vendeurs" /></th></tr>
                            <tr><th colspan="2" align="center" >Répartition des vendeurs par statut<br /><div id="chart_v0" style="width:100%;height:500px;display:inline-block;margin: 0 auto;"></div></th></tr>
                        </table>
                    </div>
                </asp:View>

                <asp:View ID="c1" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Nouveaux clients</td>
                            <td align="right" >
                                Nombre de mois
                                <asp:DropDownList ID="ddlNbMois_c1" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquec1" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_c1" style="width:100%;height:375px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="c2" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Client avec le plus de commandes</td>
                            <td align="right" >
                                Nombre de clients
                                <asp:DropDownList ID="ddlNbClients_c2" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquec2" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_c2" style="width:100%;height:650px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="c3" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Clients visitant le plus les vendeurs</td>
                            <td align="right" >
                                Nombre de clients
                                <asp:DropDownList ID="ddlNbClients_c3" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquec3" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_c3" style="width:100%;height:650px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="c4" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Meilleurs clients potentiels</td>
                            <td align="right" >
                                Nombre de clients
                                <asp:DropDownList ID="ddlNbClients_c4" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerGraphiquec4" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <div id="chart_c4" style="width:100%;height:650px;display:inline-block;margin: 0 auto;"></div>
                    </div>
                </asp:View>
                <asp:View ID="c5" runat="server">
                    <div class="rectangleItem hautRectangle">
                        <table border="0" width="100%" >
                            <tr><td>Dernieres connexions de clients</td>
                            <td align="right" >
                                Nombre de clients
                                <asp:DropDownList ID="ddlNbClients_c5" runat="server" AutoPostBack="true" ForeColor="Black" OnSelectedIndexChanged="chargerConnexionsc5" />
                            </td></tr>
                        </table>
                    </div>
                    <div class="rectangleItem basRectangle">
                         <table class="table_avec_ligne" style="width:95%">
                            <tr>
                                <th>Date</th>
                                <th>Nom</th>
                                <th>Adresse email</th>
                                <th>Nombre de connexions</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptConnexionsRecentes" OnItemDataBound="rptConnexionsRecentes_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:Label runat="server" ID="lbl_date" /></td>
                                        <td><asp:Label runat="server" ID="lbl_nom_client" /></td>
                                        <td><asp:Label runat="server" ID="lbl_adresse_email_client" /></td>
                                        <td><asp:Label runat="server" ID="lbl_nb_connexions" /></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="c0" runat="server">
                    <div class="rectangleItem hautRectangle">Statistiques diverses sur les clients</div>
                    <div class="rectangleItem basRectangle">
                        <table class="tableProduits" style="width:95%;">
                            
                            <tr><th colspan="2" align="center" >Nombre total de clients :<asp:label runat="server" Text="" ID="lbl_total_clients" /></th></tr>
                            <tr><th colspan="2" align="center" >Répartition des clients par catégorie <br /><div id="chart_c0" style="width:100%;height:500px;display:inline-block;margin: 0 auto;"></div></th></tr>
                        </table>
                    </div>
                </asp:View>
            </asp:MultiView>

        </div>
    </div>
</asp:Content>

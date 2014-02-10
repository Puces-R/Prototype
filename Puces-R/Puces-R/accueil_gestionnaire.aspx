<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="accueil_gestionnaire.aspx.cs" Inherits="Puces_R.accueil_gestionnaire" Title="Accueil gestionnaire" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/accueil_gestionnaire.css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Table runat="server" >
            <asp:TableRow>
                <asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        Meilleurs clients ce mois
                    </div>
                    <div class="rectangleItem basRectangle" style="font-size: small;">
                        <ul>
                            <asp:Repeater runat="server" ID="rptMeilleursClients" OnItemDataBound="rptMeilleursClients_ItemDataBound" >
                                <ItemTemplate> 
                                    <li><asp:Label runat="server" ID="lbl_meilleur_client" /></li>
                                </ItemTemplate>
                            </asp:Repeater>                  
                        </ul>
                    </div>
                </asp:TableCell><asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        Meilleurs vendeurs ce mois
                    </div>
                    <div class="rectangleItem basRectangle" style="font-size: small;">
                        <ul>
                            <asp:Repeater runat="server" ID="rptMeilleursVendeurs" OnItemDataBound="rptMeilleursVendeurs_ItemDataBound" >
                                <ItemTemplate> 
                                    <li><asp:Label runat="server" ID="lbl_meilleur_vendeur" /></li>
                                </ItemTemplate>
                            </asp:Repeater>                  
                        </ul>
                    </div>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        Gérer les demandes des vendeurs (<asp:Label runat="server" ID="lbl_nb_demandes" />)
                    </div>
                    <div class="rectangleItem basRectangle"  style="font-size: small;">
                        Les cinq dernières demandes
                        <ul>
                            <asp:Repeater runat="server" ID="rptDemandes" OnItemDataBound="rptDemandes_ItemDataBound" >
                                <ItemTemplate> 
                                    <li><asp:Label runat="server" ID="lbl_nom_demande" /></li>
                                </ItemTemplate>
                            </asp:Repeater>                  
                        </ul>
                        <div class="lienDetails">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="gerer_demandes_vendeurs.aspx" Text="Détails" />
                        </div>
                    </div>
                </asp:TableCell><asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        <span>Rapports & Statistiques</span>
                    </div>
                    <div class="rectangleItem basRectangle" style="font-size: small;">
                        <ul>
                            <li>
                                Du côté des vendeurs
                                <ul>
                                    <li><asp:Label runat="server" ID="lbl_nb_nouv_vendeurs" /> nouveaux vendeurs ce mois</li>
                                    <li><asp:Label runat="server" ID="lbl_nb_vendeurs" /> vendeurs en tout</li>
                                </ul>
                            </li>
                            <li>
                                Du côté des clients
                                <ul>
                                    <li><asp:Label runat="server" ID="lbl_nb_nouv_clients" /> nouveaux clients ce mois</li>
                                    <li><asp:Label runat="server" ID="lbl_nb_clients" /> clients en tout</li>
                                </ul>
                            </li>                     
                        </ul>
                        <div class="lienDetails">
                            <asp:HyperLink runat="server" NavigateUrl="visualiser_stats_rapports.aspx" Text="Détails" />
                        </div>
                    </div>
                </asp:TableCell></asp:TableRow></asp:Table></div></asp:Content>
<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="accueil_gestionnaire.aspx.cs" Inherits="Puces_R.accueil_gestionnaire" Title="Accueil gestionnaire" %>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link rel="stylesheet" type="text/css" href="CSS/accueil_gestionnaire.css" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        <span>Gérer les demandes des vendeurs</span>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <ul>
                            <li>2 nouvelles demandes dépuis votre dernière visite</li>
                            <li>5 demandes à traiter</li>                     
                        </ul>
                        <div class="lienDetails">
                            <asp:HyperLink runat="server" NavigateUrl="gerer_demandes_vendeurs.aspx" Text="Détails" />
                        </div>
                    </div>
                </asp:TableCell><asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        <span>Gérer l'inactivité des vendeurs</span>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <ul>
                            <li>2 vendeurs devenus inactifs dépuis votre dernière visite</li>
                            <li>5 vendeurs inactifs</li>                     
                        </ul>
                        <div class="lienDetails">
                            <asp:HyperLink runat="server" NavigateUrl="gerer_inactivite_vendeurs.aspx" Text="Détails" />
                        </div>
                    </div>
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        <span>Gérer l'inactivité des clients</span>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <ul>
                            <li>2 clients devenus inactifs dépuis votre dernière visite</li>
                            <li>5 clients inactifs</li>                      
                        </ul>
                         <div class="lienDetails">
                            <asp:HyperLink runat="server" NavigateUrl="gerer_inactivite_clients.aspx" Text="Détails" />
                        </div>
                    </div>
                </asp:TableCell><asp:TableCell>
                    <div class="rectangleItem hautRectangle">
                        <span>Rapports & Statistiques</span>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <ul>
                            <li>
                                Du côté des vendeurs
                                <ul>
                                    <li>10 nouveaux vendeurs cette année</li>
                                    <li>50 vendeurs en tout</li>
                                </ul>
                            </li>
                            <li>
                                Du côté des clients
                                <ul>
                                    <li>50 nouveaux clients cette année</li>
                                    <li>300 clients en tout</li>
                                </ul>
                            </li>                     
                        </ul>
                        <div class="lienDetails">
                            <asp:HyperLink runat="server" NavigateUrl="visualiser_stats_rapports.aspx" Text="Détails" />
                        </div>
                    </div>
                </asp:TableCell></asp:TableRow></asp:Table></div></asp:Content>
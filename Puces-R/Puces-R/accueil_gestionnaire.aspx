<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="accueil_gestionnaire.aspx.cs" Inherits="Puces_R.accueil_gestionnaire" Title="Accueil gestionnaire" %>

<%@ Register TagPrefix="lp" TagName="MenuGestionnaire" Src="~/Controles/MenuGestionnaire.ascx" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuGestionnaire ID="MenuGestionnaire1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <link href="CSS/Site.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
    <asp:Table runat="server">
        <asp:TableRow  >
            <asp:TableCell ColumnSpan="3" CssClass="titre_sec">Quoi de neuf ...</asp:TableCell><asp:TableCell ></asp:TableCell>
        </asp:TableRow  >
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell></asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow  >
            <asp:TableCell CssClass="titre_tab">
                Gérer les demandes des vendeurs
            </asp:TableCell>
            <asp:TableCell CssClass="space_cell">&nbsp;</asp:TableCell>
            <asp:TableCell CssClass="titre_tab">
                Gérer l'inactivité des vendeurs
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="cont_tab">
                <ul>
                    <li>2 nouvelles demandes dépuis votre dernière visite</li>
                    <li>5 demandes à traiter</li>                     
                </ul>
                <div style="width:275px; text-align:center;"><a href="gerer_demandes_vendeurs.aspx" ><span class="lien_boutton">Voir plus</span> </a></div>
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="cont_tab">
                <ul>
                    <li>2 vendeurs devenus inactifs dépuis votre dernière visite</li>
                    <li>5 vendeurs inactifs</li>                     
                </ul>
                <div style="width:275px; text-align:center;"><a href="gerer_inactivite_vendeurs.aspx" ><span class="lien_boutton">Voir plus</span> </a></div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="titre_tab">
                Gérer l'inactivité des clients
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="titre_tab">
                Rapports & Statistiques
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="cont_tab">
                <ul>
                    <li>2 clients devenus inactifs dépuis votre dernière visite</li>
                    <li>5 clients inactifs</li>                      
                </ul>
                <div style="width:275px; text-align:center;"><a href="gerer_inactivite_clients.aspx" ><span class="lien_boutton">Voir plus</span> </a></div>
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell CssClass="cont_tab">
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
                <div style="width:275px; text-align:center;"><a href="visualiser_stats_rapports.aspx" ><span class="lien_boutton">Voir plus</span> </a></div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>
</asp:Content>

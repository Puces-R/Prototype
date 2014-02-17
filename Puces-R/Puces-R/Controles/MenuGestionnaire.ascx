<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuGestionnaire.ascx.cs" Inherits="Puces_R.Controles.MenuGestionnaire" %>

<asp:Menu ID="ctrMenu" runat="server" Orientation="Horizontal" SkipLinkText="">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <StaticHoverStyle ForeColor="#6AC331" />
    <DynamicSelectedStyle ForeColor="#6AC331" />
    <DynamicHoverStyle ForeColor="#6AC331" />
    <DynamicMenuStyle CssClass="popupMenu" />
    <Items>
        <asp:MenuItem Text="Accueil" NavigateUrl="../accueil_gestionnaire.aspx" />
        <asp:MenuItem Text="Gerer les vendeurs" NavigateUrl="../gerer_vendeurs.aspx">            
            <asp:MenuItem Text="Inactivité des vendeurs" NavigateUrl="../gerer_inactivite_vendeurs.aspx" />
            <asp:MenuItem Text="Nouvelles demandes de vendeurs" NavigateUrl="../gerer_demandes_vendeurs.aspx" />
            <asp:MenuItem Text="Suivi des redevances" NavigateUrl="../accueil_compta.aspx" />  
            <asp:MenuItem Text="Rechercher un vendeur" NavigateUrl="../gerer_vendeurs.aspx" />  
        </asp:MenuItem>
        <asp:MenuItem Text="Gerer les clients" NavigateUrl="../gerer_vendeurs.aspx">            
            <asp:MenuItem Text="Inactivité des client" NavigateUrl="../gerer_inactivite_clients.aspx" />
            <asp:MenuItem Text="Rechercher un client" NavigateUrl="../gerer_clients.aspx" />  
        </asp:MenuItem>
        <asp:MenuItem Text="Statistiques et rapports" NavigateUrl="../visualiser_stats_rapports.aspx" />
    </Items>
</asp:Menu>
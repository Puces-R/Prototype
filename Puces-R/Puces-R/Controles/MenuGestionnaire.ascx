﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuGestionnaire.ascx.cs" Inherits="Puces_R.Controles.MenuGestionnaire" %>

<asp:Menu ID="ctrMenu" runat="server" Orientation="Horizontal">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <LevelSubMenuStyles>
        <asp:SubMenuStyle />
        <asp:SubMenuStyle CssClass="popupMenu" />
    </LevelSubMenuStyles>
    <Items>
        <asp:MenuItem Text="Accueil" NavigateUrl="../accueil_gestionnaire.aspx" />
        <asp:MenuItem Text="Demandes des vendeurs" NavigateUrl="../gerer_demandes_vendeurs.aspx" />
        <asp:MenuItem Text="Inactivité" Selectable="false">
            <asp:MenuItem Text="Inactivité des vendeurs" NavigateUrl="../gerer_inactivite_vendeurs.aspx" />
            <asp:MenuItem Text="Inactivité des Clients" NavigateUrl="../gerer_inactivite_clients.aspx" />
        </asp:MenuItem>
        <asp:MenuItem Text="Statistiques et rapports" NavigateUrl="../visualiser_stats_rapports.aspx" />
        <asp:MenuItem Text="Déconnecter" NavigateUrl="../Connexion.aspx" />
    </Items>
</asp:Menu>
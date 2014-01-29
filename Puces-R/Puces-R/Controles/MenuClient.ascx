﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuClient.ascx.cs" Inherits="Puces_R.Controles.MenuClient" %>

<asp:Menu runat="server" Orientation="Horizontal" ID="ctrMenu">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <LevelSubMenuStyles>
        <asp:SubMenuStyle />
        <asp:SubMenuStyle CssClass="popupMenu" />
    </LevelSubMenuStyles>
    <Items>
        <asp:MenuItem Text="Accueil" NavigateUrl="../AccueilClient.aspx" />
        <asp:MenuItem Text="Modifier le profil" NavigateUrl="../ProfilClient.aspx" />
        <asp:MenuItem Text="Déconnecter" NavigateUrl="../Connexion.aspx" />
    </Items>
</asp:Menu>
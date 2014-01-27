<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuClient.ascx.cs" Inherits="Puces_R.Controles.MenuClient" %>

<asp:Menu runat="server" Orientation="Horizontal">
    <StaticMenuItemStyle HorizontalPadding="10" Font-Bold="True" />
    <Items>
        <asp:MenuItem Text="Accueil" NavigateUrl="../AccueilClient.aspx?noclient=10000" />
        <asp:MenuItem Text="Panier" NavigateUrl="../Panier.aspx?noclient=10000" />
        <asp:MenuItem Text="Modifier le profil" />
        <asp:MenuItem Text="Déconnecter" NavigateUrl="../Connexion.aspx" />
    </Items>
</asp:Menu>
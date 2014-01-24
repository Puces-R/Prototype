<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuClient.ascx.cs" Inherits="Puces_R.Controles.MenuClient" %>

<asp:Menu runat="server" Orientation="Horizontal">
    <StaticMenuItemStyle HorizontalPadding="10" Font-Bold="True" />
    <Items>
        <asp:MenuItem Text="Accueil" />
        <asp:MenuItem Text="Panier" />
        <asp:MenuItem Text="Modifier le profil" />
        <asp:MenuItem Text="Déconnecter" NavigateUrl="../Connexion.aspx" />
    </Items>
</asp:Menu>
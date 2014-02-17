<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuClient.ascx.cs" Inherits="Puces_R.Controles.MenuClient" %>

<asp:Menu runat="server" Orientation="Horizontal" ID="ctrMenu" SkipLinkText="">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <StaticHoverStyle ForeColor="#6AC331" />
    <DynamicSelectedStyle ForeColor="#6AC331" />
    <DynamicHoverStyle ForeColor="#6AC331" />
    <DynamicMenuStyle CssClass="popupMenu" />
    <Items>
        <asp:MenuItem Text="Accueil" Value="Accueil" NavigateUrl="../AccueilClient.aspx" />
        <asp:MenuItem Text="Produits" Value="Produits" NavigateUrl="../Produits.aspx" />
        <asp:MenuItem Text="Historique" Value="Historique" NavigateUrl="../CommandesClient.aspx" />
        <asp:MenuItem Text="Profil" Value="Profil" NavigateUrl="../ProfilClient.aspx" />
    </Items>
</asp:Menu>
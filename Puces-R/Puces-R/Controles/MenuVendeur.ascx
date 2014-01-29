<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuVendeur.ascx.cs" Inherits="Puces_R.Controles.MenuVendeur" %>

<asp:Menu runat="server" Orientation="Horizontal" ID="ctrMenu">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <Items>
        <asp:MenuItem Text="Accueil" />
        <asp:MenuItem Text="Gérer les commandes" />
        <asp:MenuItem Text="Gérer les produits" />
        <asp:MenuItem Text="Modifier le profil" NavigateUrl="../ProfilVendeur.aspx"/>
        <asp:MenuItem Text="Déconnecter" NavigateUrl="../Connexion.aspx" />
    </Items>
</asp:Menu>
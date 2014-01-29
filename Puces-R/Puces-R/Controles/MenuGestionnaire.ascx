<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuGestionnaire.ascx.cs" Inherits="Puces_R.Controles.MenuGestionnaire" %>

<asp:Menu ID="ctrMenu" runat="server" Orientation="Horizontal">
    <StaticMenuItemStyle HorizontalPadding="10" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <Items>
        <asp:MenuItem Text="Accueil" />
        <asp:MenuItem Text="Demandes des vendeurs" />
        <asp:MenuItem Text="Inactivité" />
        <asp:MenuItem Text="Statistiques et rapports" />
        <asp:MenuItem Text="Déconnecter" NavigateUrl="../Connexion.aspx" />
    </Items>
</asp:Menu>
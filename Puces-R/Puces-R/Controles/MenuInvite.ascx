<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuInvite.ascx.cs" Inherits="Puces_R.Controles.MenuInvite" %>

<asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" >
    <StaticMenuItemStyle HorizontalPadding="10" Font-Bold="True" />
    <Items>
        <asp:MenuItem Text="Accueil" />
        <asp:MenuItem Text="S'inscrire comme client" NavigateUrl="~/InscriptionClient.aspx" />
        <asp:MenuItem Text="S'inscrire comme vendeur" NavigateUrl="~/InscriptionVendeur.aspx" />
        <asp:MenuItem Text="Connexion" NavigateUrl="~/Connexion.aspx" />
        <asp:MenuItem Text="Mot de passe oublié ?" NavigateUrl="~/RecupererMotDePasse.aspx" />
    </Items>
</asp:Menu>
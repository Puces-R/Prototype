<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuACoteVendeur.ascx.cs" Inherits="Puces_R.Controles.MenuACoteVendeur" %>

<asp:Menu runat="server" Orientation="Vertical" ID="ctrMenu" SkipLinkText="">
    <StaticMenuItemStyle VerticalPadding="3" />
    <StaticSelectedStyle ForeColor="#6AC331" />
    <StaticHoverStyle ForeColor="#6AC331" />
    <Items>
        <asp:MenuItem Text="Produits" Value="Produits" />
        <asp:MenuItem Text="Panier" Value="Panier" />
        <asp:MenuItem Text="Historique" Value="Historique" />
    </Items>
</asp:Menu>
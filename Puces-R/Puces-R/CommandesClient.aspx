<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="CommandesClient.aspx.cs" Inherits="Puces_R.CommandesClient" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="BoiteCommande" Src="~/Controles/BoiteCommande.ascx" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/CommandesClient.css" />
</asp:Content>
 
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" ID="ctrMenu" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BarreCriteres">
    <span class="boiteListeDeroulante">
        Par page:
        <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" >
            <asp:ListItem Value="3" />
            <asp:ListItem Value="6" />
            <asp:ListItem Value="9" />
            <asp:ListItem Value="15" Selected="True" />
            <asp:ListItem Value="30" />
        </asp:DropDownList>
    </span>
    <span class="boiteListeDeroulante">
        Vendeur:
        <asp:DropDownList ID="ddlVendeur" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" />
    </span>
</asp:Content>

<asp:Content ContentPlaceHolderID="Items" runat="server">
    <div>
        <asp:MultiView runat="server" ID="mvCommandes" ActiveViewIndex="0">
            <asp:View runat="server">
                <asp:DataList RepeatColumns="3" RepeatDirection="Horizontal" runat="server" ID="dlCommandes" OnItemDataBound="dlCommandes_OnItemDataBound">
                    <ItemTemplate>
                        <lp:BoiteCommande runat="server" ID="ctrCommande" />
                    </ItemTemplate>
                </asp:DataList>
            </asp:View>
            <asp:View runat="server">
                <div class="messageCentral">Vous n'avez encore jamais effectué de commande!</div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
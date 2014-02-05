<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationItems.Master" AutoEventWireup="true" CodeBehind="CommandesClient.aspx.cs" Inherits="Puces_R.CommandesClient" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

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
                        <div class="rectangleCommande">
                            <div class="rectangleItem hautRectangle">
                                <asp:Label runat="server" ID="lblVendeur" Font-Size="Medium"/>
                                <asp:Label runat="server" ID="lblDate" Font-Size="x-Small" />
                            </div>
                            <div class="rectangleItem basRectangle">
                                <div class="pnlGauche">
                                    <table class="colonneTitre">
                                        <tr>
                                            <td>No. Commande: </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoCommande" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>No. Autorisation: </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoAutorisation" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="statutLivraison">
                                        Statut: <asp:Label runat="server" ID="lblStatut" Font-Bold="true" />
                                    </div>
                                </div>
                                <div class="pnlDroite montantsFactures">
                                    <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" Enabled="false" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </asp:View>
            <asp:View runat="server">
                <div class="messageCentral">Vous n'avez encore jamais effectué de commande!</div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
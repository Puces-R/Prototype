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

</asp:Content>

<asp:Content ContentPlaceHolderID="Items" runat="server">
    <div>
        <asp:DataList RepeatColumns="3" RepeatDirection="Horizontal" runat="server" ID="dlCommandes" OnItemDataBound="dlCommandes_OnItemDataBound">
            <ItemTemplate>
                <div class="rectangleCommande">
                    <div class="rectangleItem hautRectangle">
                        <div>
                            <asp:Literal runat="server" ID="litVendeur"/>
                        </div>
                        <div>
                            <asp:Label runat="server" ID="lblDate" Font-Size="Small" />
                        </div>
                    </div>
                    <div class="rectangleItem basRectangle">
                        <div class="pnlGauche">
                            <table class="tableCommande">
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
                                <tr>
                                    <td>Statut: </td>
                                    <td>
                                        <b>
                                            <asp:Label runat="server" ID="lblStatut" />
                                        </b>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="pnlDroite montantsFactures">
                            <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
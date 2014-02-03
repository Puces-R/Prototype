<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommandesClient.aspx.cs" Inherits="Puces_R.CommandesClient" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/CommandesClient.css" />
</asp:Content>
 
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Repeater runat="server" ID="rptCommandes" OnItemDataBound="rptCommandes_OnItemDataBound">
            <ItemTemplate>
                <div>
                    <div class="rectangleItem hautRectangle">
                        <asp:Label runat="server" ID="lblNoCommande" />
                    </div>
                    <div class="rectangleItem basRectangle">
                        <div class="pnlGauche">
                            <table>
                                <tr>
                                    <td>Vendeur: </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblVendeur" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date: </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblDate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Statut: </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblStatut" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. Autorisation: </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblNoAutorisation" />
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
        </asp:Repeater>
    </div>
</asp:Content>
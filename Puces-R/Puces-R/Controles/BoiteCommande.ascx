<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoiteCommande.ascx.cs" Inherits="Puces_R.Controles.BoiteCommande" %>

<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

<div class="rectangleCommande">
    <div class="rectangleItem hautRectangle">
        <asp:Label runat="server" ID="lblVendeur" Font-Size="Medium"/>
        <asp:Label runat="server" ID="lblDate" Font-Size="x-Small" />
        <asp:HyperLink runat="server" ID="hypNomClient" Visible="false"></asp:HyperLink>
    </div>
    <div class="rectangleItem basRectangle">
        <div>
            <div class="pnlDroite pnlDroiteCommande montantsFactures">
                <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" Enabled="false" />
            </div>
            <div class="pnlGauche">
                <table class="colonneTitre">
                    <tr>
                        <td>No. Commande: </td>
                        <td>
                            <asp:HyperLink runat="server" ID="hypNoCommande" Visible="False"></asp:HyperLink>
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
                        <td><asp:Label runat="server" ID="lblClient" Text="NoClient:" Visible="false"/></td>
                        <td>
                            <asp:HyperLink runat="server" ID="lblNoClient" Visible="false"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="statutLivraison">
            Statut: <asp:Label runat="server" ID="lblStatut" Font-Bold="true" />
        </div>
        <div class="boutonCommande">
            <asp:Button runat="server" ID="btnChanger" Font-Bold="true" Text="Changer le Statut" Visible="false" OnClick="ChangerStatut" />
        </div>
    </div>
</div>
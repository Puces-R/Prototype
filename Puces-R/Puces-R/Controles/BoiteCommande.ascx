<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoiteCommande.ascx.cs" Inherits="Puces_R.Controles.BoiteCommande" %>

<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

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

                <tr>
                    <td><asp:Label runat="server" ID="lblClient" Text="NoClient:" Visible="false"/></td>
                    <td>
                        <asp:Label runat="server" ID="lblNoClient" Visible="false"/>
                    </td>
                </tr>

            </table>
            <div class="statutLivraison">
                Statut: <asp:Label runat="server" ID="lblStatut" Font-Bold="true" />
                <asp:Button runat="server" ID="btnChanger" Font-Bold="true" Text="Changer le Statut de la Commande" Visible="false"/>
            </div>

            <div>
                 
            </div>
        </div>
        <div class="pnlDroite montantsFactures">
            <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" Enabled="false" />
        </div>
    </div>
</div>
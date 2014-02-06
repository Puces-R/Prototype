<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResultatPaiement.aspx.cs" Inherits="Puces_R.ResultatPaiement" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="MenuClient1" runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau pnlGauche">
            <div class="messageCentral">
                <asp:Literal runat="server" ID="litMessageResultat" />
                <br />
                <asp:HyperLink runat="server" Text="Réessayer de soumettre la commande" ID="hypReessayer" Font-Size="Smaller" />
            </div>
        </div>
        <asp:Panel runat="server" CssClass="panneau pnlDroite" ID="pnlMontantsFactures" Visible="false">
            <lp:MontantsFactures runat="server" ID="ctrMontantsFactures" Enabled="false" />
        </asp:Panel>
    </div>
</asp:Content>
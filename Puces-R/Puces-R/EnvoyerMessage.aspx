<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvoyerMessage.aspx.cs"
    Inherits="Puces_R.EnvoyerMessage" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuInvite runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <table>
            <tr>
                <td style="width: 100px;">
                    Destinataire
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbRecepteur" />
                </td>
            </tr>
            <tr>
                <td>
                    Sujet
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbSujet" MaxLength="50" Width="500px" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    Message
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbMessage" TextMode="MultiLine" Width="700px" Height="150px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button runat="server" OnClick="apercuMessage" Text="Aperçu" />
                    <asp:Button runat="server" OnClick="envoyerMessage" Text="Envoyer" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="divApercu" runat="server" Visible="false">
        <table style="border-collapse: collapse; table-layout:fixed; width:0;">
            <tr>
                <td style="width: 100px;">
                    Date
                </td>
                <td style="width: 700px;">
                    <asp:Label runat="server" ID="lblDate">Date</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    De
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDe">De</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Sujet
                </td>
                <td style="overflow: hidden; width: 700px;">
                    <asp:Label runat="server" ID="lblSujet">Sujet</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    Message
                </td>
                <td style="background-color: White; border: solid gray 1px; padding: 10px; overflow: hidden; width: 700px;">
                    <asp:Label runat="server" ID="lblMessage">Message</asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

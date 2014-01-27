<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvoyerMessage.aspx.cs"
    Inherits="Puces_R.EnvoyerMessage" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuInvite runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="rectangleStylise">
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
                    <asp:TextBox runat="server" ID="tbMessage" TextMode="MultiLine" Width="500px" Height="150px" />
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
    <p runat="server" ID="pApercu" Visible="false" style="width: 500px; border: 1px solid gray ; padding: 10px; margin: auto;"></p>
</asp:Content>

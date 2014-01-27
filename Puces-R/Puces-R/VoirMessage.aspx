<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoirMessage.aspx.cs" Inherits="Puces_R.VoirMessage"
    MasterPageFile="~/Site.Master" %>

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
                <td>
                    Date :
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDate">Date</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    De :
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDe">De</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Sujet :
                </td>
                <td>
                    <asp:Label runat="server" ID="lblSujet">Sujet</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Message :
                </td>
                <td style="background-color: White;">
                    <asp:Label runat="server" ID="lblMessage">Message</asp:Label>
                </td>
            </tr>
        </table>
        <div>
</asp:Content>

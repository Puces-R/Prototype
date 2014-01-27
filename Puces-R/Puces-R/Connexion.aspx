<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="Puces_R.Connexion"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagName="MotDePasse" TagPrefix="yc" Src="~/Controles/MotDePasse.ascx" %>
<%@ Register TagPrefix="yc" TagName="MenuClient" Src="~/Controles/MenuClient.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuClient runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        td
        {
            border: solid black 1px;
        }
    </style>
    <table>
        <tr>
            <td>
                Adresse courriel
            </td>
            <td>
                <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <yc:MotDePasse runat="server" ID="tbMotPasse" Obligatoire="false" />
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align;">
                <asp:Button runat="server" CausesValidation="false" Text="Se connecter" OnClick="seConnecter" />
            </td>
        </tr>
    </table>
</asp:Content>

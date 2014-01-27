<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InscriptionClient.aspx.cs"
    Inherits="Puces_R.InscriptionClient" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="Identifiants" Src="~/Controles/Identifiants.ascx" %>
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
        <yc:Identifiants runat="server" ID="ctlIdentifiants" />
        <tr>
            <td colspan="3" style="text-align:center;" >
                <asp:Button runat="server" ID="btnConfirmer" Text="Confirmer l'inscription" OnClick="inscription"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>

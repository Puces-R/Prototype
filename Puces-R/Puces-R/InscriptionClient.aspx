<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InscriptionClient.aspx.cs"
    Inherits="Puces_R.InscriptionClient" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="Identifiants" Src="~/Controles/Identifiants.ascx" %>
<%@ Register TagPrefix="yc" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuInvite runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="rectangleComplet rectangleItem">
        <table class="formulaire">
            <yc:Identifiants runat="server" ID="ctlIdentifiants" />
            <tr>
                <td colspan="3" style="text-align: center;">
                    <asp:Button runat="server" ID="btnConfirmer" Text="Confirmer l'inscription" OnClick="inscription"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

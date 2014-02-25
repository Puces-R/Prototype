<%@ Page Title="Profil du client" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProfilClient.aspx.cs" Inherits="Puces_R.ProfilClient" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="ProfilClient" Src="~/Controles/ProfilClient.ascx" %>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau rectangleComplet rectangleItem">
            <table class="formulaire">
                <lp:ProfilClient runat="server" AfficherCourrielEtMotDePasse="true" ID="ctrProfil" />
                <tr>
                    <td colspan="3" class="centre">
                        <asp:Button runat="server" ID="btnSauvegarder" Text="Sauvegarder" OnClick="btnSauvegarder_OnClick"
                            CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

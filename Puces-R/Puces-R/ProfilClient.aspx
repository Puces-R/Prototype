<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilClient.aspx.cs" Inherits="Puces_R.ProfilClient" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="lp" TagName="ProfilClient" Src="~/Controles/ProfilClient.ascx" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau">
            <lp:ProfilClient runat="server" AfficherCourrielEtMotDePasse="true" ID="ctrProfil" />
            <div class="boutonsAction">
                <asp:Button runat="server" ID="btnSauvegarder" Text="Sauvegarder" OnClick="btnSauvegarder_OnClick" />
            </div>
        </div>
    </div>
</asp:Content>

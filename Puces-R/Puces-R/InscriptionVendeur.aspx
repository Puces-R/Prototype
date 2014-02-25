<%@ Page Title="Inscription d'un vendeur" Language="C#" AutoEventWireup="true" CodeBehind="InscriptionVendeur.aspx.cs"
    Inherits="Puces_R.InscriptionVendeur" MasterPageFile="~/Site.Master" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="yc" TagName="Identifiants" Src="~/Controles/Identifiants.ascx" %>
<%@ Register TagPrefix="yc" TagName="Profil" Src="~/Controles/ProfilVendeur.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="rectangleComplet rectangleItem">
        <table class="formulaire" >
            <yc:Identifiants runat="server" ID="tbIdentifiants" />
            <yc:Profil runat="server" ID="ctrProfil" />
            <tr>
                <td colspan="3" style="text-align: center;">
                    <asp:Button runat="server" ID="btnConfirmer" Text="Confirmer l'inscription" CausesValidation="false"
                        OnClick="inscription" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

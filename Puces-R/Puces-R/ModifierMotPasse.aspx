<%@ Page Title="Changement du mot de passe" Language="C#" AutoEventWireup="true" CodeBehind="ModifierMotPasse.aspx.cs" Inherits="Puces_R.ModifierMotPasse" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="yc" TagName="MotDePasse" Src="~/Controles/MotDePasse.ascx" %>
<%@ Register TagPrefix="yc" TagName="DoubleMdp" Src="~/Controles/DoubleMdp.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="rectangleComplet rectangleItem">
        <table class="formulaire">
            <tr>
                <yc:MotDePasse runat="server" ID="tbMotPasse" Obligatoire="true" Longueur="false" />
            </tr>
            <yc:DoubleMdp runat="server" ID="tbNouveauMotPasse" Changement="true" />
            <tr>
                <td colspan="3" class="erreur centre">
                    <asp:CustomValidator runat="server" OnServerValidate="mdpValide" ErrorMessage="Le mot de passe entré est incorrect"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td colspan="3" class="centre">
                    <asp:Button runat="server" CausesValidation="false" Text="Modifier" OnClick="modifierMdp" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

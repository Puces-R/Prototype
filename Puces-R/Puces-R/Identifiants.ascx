<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Identifiants.ascx.cs"
    Inherits="Puces_R.Identifiants" %>
<%@ Register TagPrefix="yc" TagName="Courriel" Src="~/Courriel.ascx" %>

<asp:TableRow runat="server" ID="trCourriel">
    <asp:TableCell runat="server">
            Adresse courriel
        </asp:TableCell>
    <asp:TableCell runat="server">
        <yc:Courriel runat="server" ID="tbCourriel" Existe="true" />
    </asp:TableCell>
</asp:TableRow>
<asp:TableRow ID="trCourrielConfirmation" runat="server">
    <asp:TableCell runat="server">
            Confirmer l'adresse courriel
        </asp:TableCell>
    <asp:TableCell runat="server">
        <yc:Courriel runat="server" ID="tbCourrielConfirmation" Existe="false" />
    </asp:TableCell>
</asp:TableRow>
<asp:TableRow runat="server">
    <asp:TableCell runat="server">
            Mot de passe
        </asp:TableCell>
    <asp:TableCell runat="server">
        <asp:TextBox runat="server" ID="tbMotPasse" TextMode="Password" MaxLength="50" />
        <asp:RequiredFieldValidator runat="server" ID="mdpPresent" ControlToValidate="tbMotPasse"
            ErrorMessage="Obligatoire" Enabled="false" />
    </asp:TableCell>
</asp:TableRow>
<asp:TableRow runat="server" ID="trMdpConfirmation" Visible="false">
    <asp:TableCell runat="server">
            Confirmer le mot de passe
        </asp:TableCell>
    <asp:TableCell runat="server">
        <asp:TextBox runat="server" ID="tbMotPasseConfirmation" TextMode="Password" MaxLength="50" />
        <asp:CustomValidator runat="server" ID="valMdp" ControlToValidate="tbMotPasseConfirmation"
            ValidateEmptyText="true" OnServerValidate="validerMotPasse" ErrorMessage="Différents" />
    </asp:TableCell>
</asp:TableRow>

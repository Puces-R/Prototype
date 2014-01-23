<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Identifiants.ascx.cs"
    Inherits="Puces_R.Identifiants" %>
<asp:Table runat="server">
    <asp:TableRow runat="server">
        <asp:TableCell runat="server">
            Adresse courriel
        </asp:TableCell>
        <asp:TableCell runat="server">
            <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
            <asp:RequiredFieldValidator ID="valCourriel" runat="server" ControlToValidate="tbCourriel"
                ErrorMessage="Obligatoire" />
            <asp:RegularExpressionValidator runat="server" ControlToValidate="tbCourriel"
                ValidationExpression="^[-_.a-zA-Z0-9]+@[-_.a-zA-Z0-9]+\.[a-zA-Z]{2,6}$" ErrorMessage="Format" />
            <asp:CustomValidator runat="server" ID="adresseExiste" ControlToValidate="tbCourriel"
                OnServerValidate="validerAdresseExistante" ErrorMessage="Existe" Visible="false" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell runat="server">
            Mot de passe
        </asp:TableCell>
        <asp:TableCell runat="server">
            <asp:TextBox runat="server" ID="tbMotPasse" TextMode="Password" MaxLength="50" />
            <asp:RequiredFieldValidator runat="server" ID="mdpPresent" ControlToValidate="tbMotPasse"
                ErrorMessage="Obligatoire" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" ID="trInscription" Visible="false">
        <asp:TableCell runat="server">
            Confirmer le mot de passe
        </asp:TableCell>
        <asp:TableCell runat="server">
            <asp:TextBox runat="server" ID="tbMotPasseConfirmation" TextMode="Password" MaxLength="50" />
            <asp:CustomValidator runat="server" ID="valMdp" ControlToValidate="tbMotPasseConfirmation"
                ValidateEmptyText="true" OnServerValidate="validerMotPasse" ErrorMessage="Différents" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>

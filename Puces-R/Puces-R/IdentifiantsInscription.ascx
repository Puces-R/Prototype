<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IdentifiantsInscription.ascx.cs"
    Inherits="Puces_R.IdentifiantsInscription" %>
<tr>
    <td>
        Adresse courriel
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbCourriel"
            ErrorMessage="Obligatoire" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbCourriel"
            ValidationExpression="^[-_.a-zA-Z0-9]+@[-_.a-zA-Z0-9]+\.[a-zA-Z]{2,6}$" ErrorMessage="Format" />
        <asp:CustomValidator runat="server" ID="adresseExiste" ControlToValidate="tbCourriel"
            OnServerValidate="validerAdresseExistante" ErrorMessage="Existe" />
    </td>
</tr>
<tr>
    <td>
        Mot de passe
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbMotPasse" TextMode="Password" MaxLength="50" />
        <asp:RequiredFieldValidator runat="server" ID="mdpPresent" ControlToValidate="tbMotPasse"
            ErrorMessage="Obligatoire" />
    </td>
</tr>
<tr>
    <td>
        Confirmer le mot de passe
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbMotPasseConfirmation" TextMode="Password" MaxLength="50" />
        <asp:CustomValidator runat="server" ID="valMdp" ControlToValidate="tbMotPasseConfirmation"
            ValidateEmptyText="true" OnServerValidate="validerMotPasse" ErrorMessage="Différents" />
    </td>
</tr>

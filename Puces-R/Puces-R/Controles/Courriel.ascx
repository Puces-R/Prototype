<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Courriel.ascx.cs" Inherits="Puces_R.CourrielControle" %>
<tr>
    <td>
        Adresse courriel
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
    </td>
    <td class="erreur">
        <asp:RequiredFieldValidator ID="reqCourriel" runat="server" ControlToValidate="tbCourriel"
            ErrorMessage="L'adresse courriel est obligatoire" Display="Dynamic"/>
        <asp:RegularExpressionValidator runat="server" ID="formatCourriel" ControlToValidate="tbCourriel"
            ValidationExpression="^[-_.a-zA-Z0-9]+@[-_.a-zA-Z0-9]+\.[a-zA-Z]{2,6}$" ErrorMessage="Le format de l'adresse courriel n'est pas valide" Display="Dynamic"/>
        <asp:CustomValidator runat="server" ID="adresseExiste" ControlToValidate="tbCourriel"
            OnServerValidate="validerAdresseExistante" ErrorMessage="Cette adresse est déjà utilisée" Visible="false" Display="Dynamic"/>
    </td>
</tr>

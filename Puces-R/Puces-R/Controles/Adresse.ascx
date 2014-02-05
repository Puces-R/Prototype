<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Adresse.ascx.cs" Inherits="Puces_R.Controles.Adresse" %>

<tr>
    <td>
        <asp:Label ID="lblAdresse" runat="server"></asp:Label>
    </td>
    <td>
        <asp:TextBox runat="server" ID="tbAdresse" MaxLength="100" />
        <asp:RequiredFieldValidator ID="reqAdresse" runat="server" ControlToValidate="tbAdresse"
            ErrorMessage="L'adresse est obligatoire" Display="Dynamic" CssClass="erreur" />
        <asp:RegularExpressionValidator runat="server" ID="formatAdresse" ControlToValidate="tbAdresse"
            ValidationExpression="^\d+[a-zA-Z]? ([a-zA-Z0-9À-ÿ]([- '][a-zA-Z0-9À-ÿ])?)+$" ErrorMessage="Le format de l'adresse  n'est pas valide" Display="Dynamic" CssClass="erreur"/>
       
    </td>
</tr>
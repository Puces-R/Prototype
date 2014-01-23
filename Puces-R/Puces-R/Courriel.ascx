<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Courriel.ascx.cs" Inherits="Puces_R.Courriel" %>
<asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
<asp:RequiredFieldValidator ID="valCourriel" runat="server" ControlToValidate="tbCourriel"
    ErrorMessage="Obligatoire" />
<asp:RegularExpressionValidator runat="server" ControlToValidate="tbCourriel" ValidationExpression="^[-_.a-zA-Z0-9]+@[-_.a-zA-Z0-9]+\.[a-zA-Z]{2,6}$"
    ErrorMessage="Format" />
<asp:CustomValidator runat="server" ID="adresseExiste" ControlToValidate="tbCourriel"
    OnServerValidate="validerAdresseExistante" ErrorMessage="Existe" Visible="false" />
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CodePostal.ascx.cs"
    Inherits="Puces_R.CodePostal" %>
<asp:TextBox runat="server" ID="tbCodePostal" MaxLength="7" />
<asp:RegularExpressionValidator runat="server" ControlToValidate="tbCodePostal" ValidationExpression="^[a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d$"
    ErrorMessage="Format" />
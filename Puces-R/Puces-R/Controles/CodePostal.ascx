<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CodePostal.ascx.cs"
    Inherits="Puces_R.CodePostal" %>

<asp:TextBox runat="server" ID="tbPart1" MaxLength="3" Width="39px" />
&nbsp;
<asp:RequiredFieldValidator runat="server" ID="reqPart1" ControlToValidate="tbPart1" />
<asp:RegularExpressionValidator runat="server" ID="formatPart1" ControlToValidate="tbPart1"
    ValidationExpression="^[a-zA-Z]\d[a-zA-Z]$" />

<asp:TextBox runat="server" ID="tbPart2" MaxLength="3" Width="39px" />
<asp:RequiredFieldValidator runat="server" ID="reqPart2" ControlToValidate="tbPart2" />
<asp:RegularExpressionValidator runat="server" ID="formatPart2" ControlToValidate="tbPart2"
    ValidationExpression="^\d[a-zA-Z]\d$" />

<asp:CustomValidator runat="server" ID="reqCodePostal" OnServerValidate="validerObligatoire" ErrorMessage="Obligatoire" Visible="false" />
<asp:CustomValidator runat="server" OnServerValidate="validerFormat" ErrorMessage="Format" />
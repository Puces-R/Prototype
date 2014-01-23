<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvoyerMessage.aspx.cs" Inherits="Puces_R.EnvoyerMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ul>
    <li><asp:TextBox runat="server" ID="tbRecepteur" /></li>
    <li><asp:TextBox runat="server" ID="tbSujet" MaxLength="50"/></li>
    <li><asp:TextBox runat="server" ID="tbMessage" TextMode="MultiLine" /></li>
    <li><asp:Button runat="server" OnClick="envoyerMessage" />
    </ul>
    </form>
</body>
</html>

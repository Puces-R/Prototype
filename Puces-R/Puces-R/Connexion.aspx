<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="Puces_R.Connexion" %>

<%@ Register TagName="Identifiants" TagPrefix="yc" Src="~/Identifiants.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Connexion</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <yc:Identifiants runat="server" ID="tbIdentifiants" Type="CONNEXION" />
        <tr>
            <td>
                <asp:Button runat="server" CausesValidation="false" Text="Se connecter" OnClick="seConnecter" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

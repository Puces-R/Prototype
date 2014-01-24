<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="Puces_R.Connexion" %>

<%@ Register TagName="MotDePasse" TagPrefix="yc" Src="~/Controles/MotDePasse.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Connexion</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                Adresse courriel
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td>
                Mot de passe
            </td>
        </tr>
        <tr>
            <td>
                <yc:MotDePasse runat="server" ID="tbMotPasse" Obligatoire="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button runat="server" CausesValidation="false" Text="Se connecter" OnClick="seConnecter" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

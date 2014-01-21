<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InscriptionClient.aspx.cs"
    Inherits="Puces_R.InscriptionClient" %>
    <%@ Register TagPrefix="yc" TagName="IdentifiantsInscription" Src="~/IdentifiantsInscription.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>S'inscrire en tant que client</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <yc:IdentifiantsInscription runat="server" ID="ctlIdentifiants"/>
        <tr>
        <td colspan="2"><asp:Button runat="server" ID="btnConfirmer" Text="Confirmer l'inscription" OnClick="inscription" CausesValidation="false" /></td>
        </tr>
    </table>
    </form>
</body>
</html>

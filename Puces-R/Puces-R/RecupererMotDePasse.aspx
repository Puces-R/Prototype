<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecupererMotDePasse.aspx.cs"
    Inherits="Puces_R.RecupererMotDePasse" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="rectangleComplet rectangleItem">
        <table>
            <tr>
                <td>
                    Adresse courriel
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbCourriel" MaxLength="100" />
                </td>
            </tr>
            <tr>
                <td class="erreur" colspan="2" style="text-align: center; height: 12px; width: 200px;">
                    <asp:CustomValidator runat="server" OnServerValidate="adresseExiste"
                        ErrorMessage="Aucun utilisateur ne correspond à cette adresse" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button runat="server" OnClick="envoyerMdp" Text="Récupérer le mot de passe"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

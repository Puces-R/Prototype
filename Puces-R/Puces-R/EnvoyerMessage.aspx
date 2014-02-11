<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvoyerMessage.aspx.cs" Inherits="Puces_R.EnvoyerMessage" MasterPageFile="~/Site.Master" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function popup() {
            var w = window.open('ChoixDestinataires.aspx?Destinataire=<% for(int i = 0 ; i < lbDestinataires.Items.Count ; i++) { Response.Write((i == 0 ? "" : ",") + lbDestinataires.Items[i].Value); } %>&Type=<% Response.Write("Z" + Session["Type"].ToString()); %>', "ChoisirVendeur", "height=700,width=900");
            w.focus();
        }

        function postback(parametres) {
            __doPostBack('ChoixDestinataires', parametres);
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div class="rectangleComplet rectangleItem">
        <table>
            <tr>
                <td style="width: 100px;">
                    Destinataire
                </td>
                <td>
                    <asp:ListBox runat="server" ID="lbDestinataires" Rows="1" Width="700px" /><br />
                    <asp:Button runat="server" ID="btnDestinataire" Text="Modifier les destinataires" OnClientClick="popup(); return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    Sujet
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbSujet" MaxLength="50" Width="700px" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    Message
                </td>
                <td>
                    <asp:TextBox runat="server" ID="tbMessage" TextMode="MultiLine" Width="700px" Height="150px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button runat="server" OnClick="apercuMessage" Text="Aperçu" />
                    <asp:Button runat="server" OnClick="envoyerMessage" Text="Envoyer" />
                    <asp:Button runat="server" OnClick="sauvegarderMessage" Text="Sauvegarder comme brouillon" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="divApercu" runat="server" Visible="false" CssClass="rectangleComplet rectangleItem">
        <table style="border-collapse: collapse; table-layout: fixed; width: 0;">
            <tr>
                <td style="width: 100px;">
                    Date
                </td>
                <td style="width: 700px;">
                    <asp:Label runat="server" ID="lblDate">Date</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    De
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDe">De</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Sujet
                </td>
                <td style="overflow: hidden; width: 700px;">
                    <asp:Label runat="server" ID="lblSujet">Sujet</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    Message
                </td>
                <td style="border-radius: 10px; background-color: White; border: solid gray 1px;
                    padding: 10px; overflow: hidden; width: 700px;">
                    <asp:Label runat="server" ID="lblMessage">Message</asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

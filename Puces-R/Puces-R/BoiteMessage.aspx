<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoiteMessage.aspx.cs" Inherits="Puces_R.BoiteMessage"  Title="Messagerie interne"
    MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .sBoite
        {
            border-collapse: collapse;
            table-layout: fixed;
            width: 0;
        }
        .sBoite td, .sBoite th
        {
            height: 30px;
            text-align: left;
        }
        
        .sBoite td
        {
            border-top: 1px solid gray;
        }
        
        .sBoite .sLigneMessage:hover
        {
            background-color: white;
        }
        
        .sBoite .sLigneMessage a:hover
        {
            text-decoration: underline;
        }
        
        .sBoite .sCheckbox
        {
            width: 30px;
        }
        
        .sBoite .sDe
        {
            width: 250px;
        }
        
        .sBoite .sSujet
        {
            width: 600px;
            overflow: hidden;
        }
        
        .sBoite .sDate
        {
            width: 200px;
        }
        
        .sMenuBoite
        {
            font-size: 0.75em;
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript">
        function checkAll(value) {
            $("input:checkbox").each(function () {
                $(this).attr("checked", value);
            });
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <div>
            <div style="float: left;">
                <asp:Menu ID="menuAction" runat="server" Orientation="Horizontal" CssClass="sMenuBoite"
                    OnMenuItemClick="clickOption">
                    <StaticMenuItemStyle HorizontalPadding="10" />
                </asp:Menu>
                <div>
                    <a href="javascript:checkAll(true);">Tout sélectionner</a>&nbsp; <a href="javascript:checkAll(false);">
                        Tout désélectionner</a>
                </div>
            </div>
            <div style="float: right;">
                Changer de boîte&nbsp;
                <asp:DropDownList runat="server" ID="ddlBoite" OnSelectedIndexChanged="changeBoite"
                    AutoPostBack="true">
                    <asp:ListItem Text="Boîte de réception" Value="1" />
                    <asp:ListItem Text="Messages archivés" Value="2" />
                    <asp:ListItem Text="Corbeille" Value="3" />
                    <asp:ListItem Text="Messages envoyés" Value="-1" />
                    <asp:ListItem Text="Brouillon" Value="-2" />
                </asp:DropDownList>
            </div>
        </div>
        <div style="max-height: 400px; overflow: scroll; float: left; width: 100%">
            <div runat="server" class="rectangleComplet rectangleItem">
                <table class="sBoite">
                    <thead>
                        <tr>
                            <th class="sCheckbox">
                            </th>
                            <th class="sDe">
                                <asp:LinkButton runat="server" ID="linkDe" OnClick="ordre" Text="De" />
                            </th>
                            <th class="sSujet">
                                <asp:LinkButton runat="server" ID="linkSujet" OnClick="ordre" Text="Sujet" />
                            </th>
                            <th class="sDate">
                                <asp:LinkButton runat="server" ID="linkDate" OnClick="ordre" Text="Date" />
                            </th>
                        </tr>
                    </thead>
                    <tbody runat="server" id="ListeMessage">
                    </tbody>
                </table>
            </div>
        </div>
        <div runat="server" id="divMessage" style="max-height: 400px; overflow:scroll; width:100%" visible="false">
            <div class="rectangleComplet rectangleItem" style="width: 1088px;">
                <table style="border-collapse: collapse; table-layout: fixed; width: 0;">
                    <tr>
                        <td style="width: 100px;">
                            Date
                        </td>
                        <td style="width: 900px;">
                            <asp:Label runat="server" ID="lblDate">Date</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            De
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblDe">De</asp:Label>&nbsp;<asp:LinkButton ID="lnkRepondre"
                                runat="server" Text="Répondre" OnClick="repondre" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            À
                        </td>
                        <td>
                            <asp:ListBox runat="server" ID="lbDestinataires" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sujet
                        </td>
                        <td style="overflow: hidden; width: 900px;">
                            <asp:Label runat="server" ID="lblSujet">Sujet</asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trPiece" visible="false">
                        <td>
                            Pièce jointe
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnDownload" OnClick="download" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            Message
                        </td>
                        <td style="border-radius: 10px; background-color: White; border: solid gray 1px;
                            padding: 10px; overflow: hidden; width: 900px;">
                            <asp:Label runat="server" ID="lblMessage">Message</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

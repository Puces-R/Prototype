<%@ Page Title="Gérer les nouvelles demandes de vendeurs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gerer_demandes_vendeurs.aspx.cs" Inherits="Puces_R.gerer_demandes_vendeurs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/style_sec4.css" />
    <script type="text/javascript" src="lib/js/librairie.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="titre_sec">Demandes de vendeurs</div>
    <asp:Repeater runat="server" ID="rptDemandes" OnItemDataBound="rptDemandes_ItemDataBound" OnItemCommand="rptDemandes_ItemCommand">
        <HeaderTemplate>
            <table border="0">  
        </HeaderTemplate>
        <ItemTemplate>      
            <tr>
                <td colspan="2" class="titre_tab2" onclick="afficheOuMasqueInfoVendeur(this);"><asp:Label runat="server" ID="titre_demande" /></td>
            </tr>
            <tr class="ligne_info_demande" runat="server">
                <td colspan="2" class="cont_tab2">
                    <table border="0">
                        <colgroup>
                            <col width="50%"/>
                            <col width="50%"/>
                        </colgroup>
                        <tr>
                            <td align="right">Adresse:</td>
                            <td><asp:Label runat="server" ID="addr_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Téléphone:</td>
                            <td><asp:Label runat="server" ID="tels_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Courriel:</td>
                            <td><asp:Label runat="server" ID="courriel_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Charge maximale de livraison:</td>
                            <td><asp:Label runat="server" ID="charge_max_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">Livraison gratuite:</td>
                            <td><asp:Label runat="server" ID="livraison_gratuite" /></td>
                        </tr>
                        <tr>
                            <td align="right">Date de demande:</td>
                            <td><asp:Label runat="server" ID="date_demande" /></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button id="btn_accepter" runat="server" text="Accetpter" CssClass="boutton" CommandName="acceptation_demande"/> 
                            </td>
                            <td><asp:Button ID="btn_refuser" runat="server" text="Refuser" CssClass="boutton" CommandName="refus_demande"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

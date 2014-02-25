<%@ Page Title="Résultat du paiement" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResultatPaiement.aspx.cs" Inherits="Puces_R.ResultatPaiement" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/ResultatPaiement.css" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="messageCentral">
        <asp:Label runat="server" ID="lblMessageResultat" />
        <asp:MultiView runat="server" ID="mvActionMessage" ActiveViewIndex="0">
            <asp:View runat="server">  
                <br /><br />
                <asp:HyperLink runat="server" Text="Réessayer de soumettre la commande" ID="hypReessayer" Font-Size="Smaller" />
            </asp:View>
            <asp:View runat="server"> 
                <asp:ImageButton BackColor="LightGray" ImageUrl="Images/print.png" ID="btnImprimer" OnClientClick="window.print(); return false;" runat="server" CssClass="boutonImprimer noPrint" />     
            </asp:View>
        </asp:MultiView>  
    </div>
    <asp:PlaceHolder runat="server" ID="phRapport" Visible="false">
        <div class="lignePointilleHorizontale pleineLargeur"></div>
        <div>
            <asp:ScriptManager runat="server" EnableHistory="true" />
            <rsweb:ReportViewer SizeToReportContent="true" ID="ctrRapport" runat="server" Font-Names="Verdana" Font-Size="8pt" ShowToolBar="false" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" CssClass="bonCommande" >
                <LocalReport ReportPath="BonCommande.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ArticleEnPanierDetaille" 
                            Name="ArticleEnPanierDetaille" />
                        <rsweb:ReportDataSource DataSourceId="ClientDetaille" 
                            Name="ClientDetaille" />
                        <rsweb:ReportDataSource DataSourceId="VendeurDetaille" 
                            Name="VendeurDetaille" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="VendeurDetaille" runat="server" SelectMethod="GetData" TypeName="Puces_R.BonCommandeTableAdapters.VendeurDetailleTableAdapter">
                <SelectParameters>
                    <asp:Parameter Name="NoVendeur" DefaultValue="20" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ClientDetaille" runat="server" SelectMethod="GetData" TypeName="Puces_R.BonCommandeTableAdapters.ClientDetailleTableAdapter">
                <SelectParameters>
                    <asp:Parameter Name="NoClient" DefaultValue="10400" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ArticleEnPanierDetaille" runat="server" SelectMethod="GetData" TypeName="Puces_R.BonCommandeTableAdapters.ArticleEnPanierDetailleTableAdapter">
                <SelectParameters>
                    <asp:Parameter Name="NoClient" DefaultValue="10400" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:PlaceHolder>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResultatPaiement.aspx.cs" Inherits="Puces_R.ResultatPaiement" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register TagPrefix="lp" TagName="MontantsFactures" Src="~/Controles/MontantsFactures.ascx" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="messageCentral">
        <asp:Literal runat="server" ID="litMessageResultat" />
        <br />
        <asp:HyperLink runat="server" Text="Réessayer de soumettre la commande" ID="hypReessayer" Font-Size="Smaller" />
    </div>
    <div class="lignePointilleHorizontale pleineLargeur" />
    <div>
        <rsweb:ReportViewer ID="ctrRapport" runat="server" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" CssClass="bonCommande" width="650" Height="650">
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
        <asp:ScriptManager ID="ctrScriptManager" runat="server" />
    </div>
</asp:Content>
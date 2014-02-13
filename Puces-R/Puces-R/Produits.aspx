<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="Puces_R.Produits" MasterPageFile="~/NavigationItems.Master" %>
<%@ MasterType VirtualPath="~/NavigationItems.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
    <link href="CSS/Produits.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () { setDatePicker("#<%=txtAPartirDe.ClientID%>"); setDatePicker("#<%=txtJusquA.ClientID%>") })

        function setDatePicker(id) 
        {
            $(id).datepicker(); $("#format").change(function () { $(id).datepicker("format", "yy-mm-dd", $(this).val()); });
        }
    </script>

    <script type="text/javascript">
        function RefreshUpdatePanel() {
            __doPostBack('<%= udpProduits.ClientID %>', '');
        };
    </script>

    <style type="text/css">
        .ui-datepicker
        {
            font-size: x-small;
        }
    </style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BarreCriteres">
    <div>
        <div style="display:inline-block;vertical-align:top;text-align:left;">
            <div>
                <span class="boiteListeDeroulante">
                    Recherche:
                    <asp:DropDownList ID="ddlTypeRecherche" runat="server" Font-Size="X-Small" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage">
                        <asp:ListItem Text="Description" />
                        <asp:ListItem Text="Numéro" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtCritereRecherche" runat="server" Width="60" Font-Size="X-Small" OnTextChanged="AfficherPremierePage" OnKeyUp="RefreshUpdatePanel();" />
                </span>
                <span class="boiteListeDeroulante">
                    Trier par:
                    <asp:DropDownList ID="ddlTrierPar" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" >
                        <asp:ListItem Text="Numéro" />
                        <asp:ListItem Text="Catégorie" />
                        <asp:ListItem Text="Date de parution" />
                        <asp:ListItem Text="Évaluations" />
                    </asp:DropDownList>
                </span>
                <span class="boiteListeDeroulante">
                    Par page:
                    <asp:DropDownList ID="ddlParPage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" >
                        <asp:ListItem Value="5" />
                        <asp:ListItem Value="10" />
                        <asp:ListItem Value="15" Selected="True" />
                        <asp:ListItem Value="20" />
                        <asp:ListItem Value="25" />
                        <asp:ListItem Value="50" />
                        <asp:ListItem Text="Tous" Value="-1" />
                    </asp:DropDownList>
                </span>
            </div>
            <div>
                <asp:Panel runat="server" ID="pnlDeJusquA" Visible="false">
                    <span class="boiteListeDeroulante">
                        Publié après:
                        <asp:TextBox ID="txtAPartirDe" runat="server" Width="60" Font-Size="X-Small" AutoPostBack="true" OnTextChanged="AfficherPremierePage" />
                    </span>
                    <span class="boiteListeDeroulante">
                        Publié avant:
                        <asp:TextBox ID="txtJusquA" runat="server" Width="60" Font-Size="X-Small" AutoPostBack="true" OnTextChanged="AfficherPremierePage" />
                    </span>
                </asp:Panel>
            </div>
        </div>
        <div style="display:inline-block;vertical-align:top;text-align:left;">
            <div class="boiteListeDeroulante">
                <asp:MultiView runat="server" ID="mvCategorie" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        Catégorie:
                        <asp:DropDownList ID="ddlCategorie" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" />
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        Catégories:
                        <div class="listeCochable">
                            <div>
                                <asp:CheckBoxList ID="cblCategorie" runat="server" RepeatLayout="Table" OnSelectedIndexChanged="AfficherPremierePage" AutoPostBack="True" />
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
            <div class="boiteListeDeroulante">
                <asp:MultiView runat="server" ID="mvVendeur" ActiveViewIndex="0">
                    <asp:View ID="View3" runat="server">
                        Vendeur:
                        <asp:DropDownList ID="ddlVendeur" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AfficherPremierePage" Font-Size="X-Small" />
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        Vendeurs:
                        <div class="listeCochable">
                            <div>
                                <asp:CheckBoxList ID="cblVendeur" runat="server" RepeatLayout="Table" OnSelectedIndexChanged="AfficherPremierePage" AutoPostBack="True" />
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
            <span class="boiteListeDeroulante">
                <asp:Button runat="server" Text="Avancé" ID="btnRechercheAvance" OnClick="btnRechercheAvance_OnClick" Font-Size="X-Small" />
            </span>
        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Items">
    <asp:ScriptManager runat="server" ID="ctrScriptManager" />
    <asp:UpdatePanel runat="server" ID="udpProduits">
        <ContentTemplate>
            <div>
                <asp:MultiView runat="server" ID="mvProduits">
                    <asp:View ID="View5" runat="server">
                        <ASP:DataList id="dtlProduits" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" OnItemDataBound="dtlProduits_ItemDataBound">
                            <ItemTemplate>
                                <div class="rectangleProduits rectangleComplet rectangleItem">
                                    <div class="titreRectangle">
                                        <div>
                                            <asp:HyperLink runat="server" ID="hypDescriptionAbregee" />
                                        </div>
                                    </div>
                                    <div class="boiteImageProduit">
                                        <div>
                                            <asp:Image runat="server" ID="imgProduit" />
                                        </div>
                                    </div>
                                    <div class="detailsProduit">
                                        <asp:Label runat="server" ID="lblNoProduit" />
                                        <asp:Label runat="server" ID="lblCategorie" />
                                        <asp:Label runat="server" ID="lblPrixDemande" />
                                        <asp:Label runat="server" ID="lblEvaluation" />
                                        <asp:Label runat="server" ID="lblQuantite" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </ASP:DataList>
                    </asp:View>
                    <asp:View ID="View6" runat="server">
                        <div class="messageCentral">Aucun produit ne correspond aux critères.</div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
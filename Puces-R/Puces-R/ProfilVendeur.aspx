﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilVendeur.aspx.cs" Inherits="Puces_R.ProfilVendeur" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="se" TagName="ProfilVendeur" Src="~/Controles/ProfilVendeur.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="//code.jquery.com/jquery-1.9.1.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
<link rel="stylesheet" href="./ScriptsColorPicker/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            // Create jqxColorPicker.
            $("#jqxColorPicker").jqxColorPicker({ width: 350, height: 350 });
            $("#jqxColorPicker").jqxColorPicker('setColor', '#0020A5');
        });
    </script>

    <script type="text/javascript">
        function recevoirCouleur() {
          var color = $("#jqxColorPicker").jqxColorPicker('getColor').hex;
          document.getElementById('<%=hidColor.ClientID%>').value=color;
          alert(color);
      }

      function DefinirCouleur(numero) {
        $("#jqxColorPicker").jqxColorPicker('setColor', '#'+numero);
          //document.getElementById('<%=hidColor.ClientID%>').value = color;
          //alert(color);
      }
    </script>
    <script type="text/javascript" src="./ScriptsColorPicker/jqxcore.js"></script>
    <script type="text/javascript" src="./ScriptsColorPicker/jqxcolorpicker.js"></script>
</asp:Content>

<%--//hidden field valeur de la couleur--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="panneau">
            <se:ProfilVendeur ID="ProfilClient1" runat="server"/>
        </div>

        <asp:HiddenField ID="hidColor" runat="server" />
    <div id='jqxColorPicker'>

    </div >
    <asp:FileUpload ID="fileUploaderLogo" runat="server" /><br />
    <asp:Button  ID="btnPerso" OnClientClick="recevoirCouleur();" OnClick="sauverFavori" Text="Sauver personalisation" runat="server" />
    </div>
</asp:Content>
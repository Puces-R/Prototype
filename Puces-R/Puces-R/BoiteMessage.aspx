<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoiteMessage.aspx.cs" Inherits="Puces_R.BoiteMessage"
    MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="yc" TagName="MenuInvite" Src="~/Controles/MenuInvite.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="MenuItems">
    <yc:MenuInvite runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .sBoite
        {
            border-collapse: collapse;
            table-layout:fixed;
            width:0;
        }
        .sBoite td, .sBoite th
        {
            height: 30px;
            text-align: left;
            border-bottom: 1px solid gray;
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
            overflow:hidden;
        }
        
        .sBoite .sDate
        {
            width: 200px;
        }
    </style>
    <div class="rectangleStylise">
    <table class="sBoite">
        <thead>
            <tr>
                <th class="sCheckbox">
                </th>
                <th class="sDe">
                    De
                </th>
                <th class="sSujet">
                    Sujet
                </th>
                <th class="sDate">
                    Date
                </th>
            </tr>
        </thead>
        <tbody runat="server" id="ListeMessage">
        </tbody>
    </table>
    </div>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Province.ascx.cs" Inherits="Puces_R.Province" %>
<asp:DropDownList runat="server" ID="ddlProvince" AutoPostBack="true">
    <asp:ListItem Selected="True" Text="Québec" Value="QC" />
    <asp:ListItem Text="Ontario" Value="ON" />
    <asp:ListItem Text="Nouveau-Brunswick" Value="NB" />
</asp:DropDownList>

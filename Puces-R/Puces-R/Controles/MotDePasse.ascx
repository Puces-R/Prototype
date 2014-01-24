<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MotDePasse.ascx.cs" Inherits="Puces_R.MotDePasse" %>

<asp:TextBox runat="server" ID="tbMDP" TextMode="Password" MaxLength="50" />
<asp:RequiredFieldValidator runat="server" ID="reqMDP" ControlToValidate="tbMDP" Visible="false" ErrorMessage="Obligatoire" />
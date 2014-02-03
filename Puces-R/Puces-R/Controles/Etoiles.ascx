<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Etoiles.ascx.cs" Inherits="Puces_R.Controles.Etoiles" %>

<div class="etoiles">
    <asp:Repeater runat="server" ID="rptEtoiles" OnItemDataBound="rptEtoiles_OnItemDataBound" OnItemCommand="rptEtoiles_OnItemCommand">
        <ItemTemplate>
            <asp:ImageButton runat="server" ID="imgEtoile" CommandName="Etoile" />
        </ItemTemplate>
    </asp:Repeater>
</div>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationParPage.ascx.cs" Inherits="Puces_R.Controles.NavigationParPage" %>

<asp:Panel runat="server" CssClass="barreNavigation" ID="pnlBarreNavigation">
    <asp:Panel runat="server" class="lignePointilleHorizontale pleineLargeur" ID="pnlLigneHaut" Visible="false" />
    <asp:Panel runat="server" ID="pnlLeftNavigation" CssClass="navigation leftNavigation" Visible="false">
        <div>
            <asp:ImageButton runat="server" ID="imgFirst" OnClick="btnFirst_OnClick" ImageUrl="../Images/Premier.png" />
            <asp:LinkButton runat="server" Text="Premier" ID="btnFirst" OnClick="btnFirst_OnClick" />
        </div>
        <div>
            <asp:ImageButton runat="server" ID="imgPrevious" OnClick="btnPrevious_OnClick" ImageUrl="../Images/Precedent.png" />
            <asp:LinkButton runat="server" Text="Précédent" ID="btnPrevious" OnClick="btnPrevious_OnClick" />
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlRightNavigation" CssClass="navigation rightNavigation">
        <div>
            <asp:LinkButton runat="server" Text="Suivant" ID="btnNext" OnClick="btnNext_OnClick" />
            <asp:ImageButton runat="server" ID="imgNext" OnClick="btnNext_OnClick" ImageUrl="../Images/Prochain.png" />
        </div>   
        <div>
            <asp:LinkButton runat="server" Text="Dernier" ID="btnLast" OnClick="btnLast_OnClick" />
            <asp:ImageButton runat="server" ID="imgLast" OnClick="btnLast_OnClick" ImageUrl="../Images/Dernier.png" />
        </div>
    </asp:Panel>
    <asp:Label runat="server" ID="lblInfoAuCentre" CssClass="infoAuCentre" />
    <asp:Panel runat="server" class="lignePointilleHorizontale pleineLargeur" ID="pnlLigneBas" Visible="false" />
</asp:Panel>

<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertionProduits.aspx.cs" Inherits="Puces_R.InsertionProduits" %>

<%@ Register TagPrefix="lp" TagName="MenuClient" Src="~/Controles/MenuVendeur.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MenuItems">
    <lp:MenuClient ID="MenuClient1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="CSS/InsertionProduits.css" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

       <h2>Insertion d'un nouveau produit</h2>

       <asp:Label ID="lblAvertissement" runat="server" CssClass="sRouge"></asp:Label>

     <div>   

     <table>

     <tr>
         <td><asp:Label ID="lblCategorieProduits"  runat="server">Catégorie :</asp:Label></td>
         <td><asp:DropDownList ID="ddlCategorieProduits"  runat="server"></asp:DropDownList></td>
     </tr>

     <tr>

        <td><asp:Label ID="lblDesc" runat="server">Descrption abrégée:</asp:Label> </td> 
         <td><asp:TextBox ID="tbDescAbregee" runat="server"></asp:TextBox></td> 
         <td><asp:RequiredFieldValidator id="RequiredAbregee"
                    ControlToValidate="tbDescAbregee"
                    EnableClientScript="false"
                    ErrorMessage="Description abrégée absente!"
                    runat="server"/></td>
         

     </tr>

     <tr>
        <td> <asp:Label ID="lblPrix" runat="server">Prix demandé:</asp:Label>
        </td>

        <td> <asp:TextBox ID="tbPrix" runat="server" /></td>

         <td><asp:RequiredFieldValidator id="RequiredFieldValidator1"
                    ControlToValidate="tbPrix"
                    EnableClientScript="false"
                    ErrorMessage="Prix absent!"
                    runat="server"/>
          
         <asp:RegularExpressionValidator ID="rePrixDemande" 
           ControlToValidate="tbPrix"   
           EnableClientScript="false" runat="server"
           ErrorMessage="Format invalide !" 
           ValidationExpression="^\d+([\.\,]\d{0,5})?$"> 
           </asp:RegularExpressionValidator> 

      </td>

     </tr>

     <tr>
          <td><asp:Label ID="lblDescComplete" runat="server">Descrption Complète:</asp:Label></td>
        <td> <asp:TextBox ID="tbDescComplete"  TextMode="multiline" runat="server" CssClass="DescComplete" ></asp:TextBox></td>
        
         <td><asp:RequiredFieldValidator id="RequiredComplete"
                    ControlToValidate="tbDescComplete"
                    EnableClientScript="false"
                    ErrorMessage="Description non présente."
                    runat="server"/>
                    
                   
                    
                    </td>
     </tr>

     <tr>
          <td><asp:Label ID="lblPhoto" runat="server">Photo du produit : </asp:Label> </td>
         <td> <asp:FileUpload ID="uplNomFichier" runat="server" cssClass="" /></td>
     </tr>

     <%--<tr>
         <td><asp:Label ID="lblDateCreation" runat="server">Date de création :</asp:Label></td>
         <td><asp:TextBox ID="tbDateCreation" runat="server"></asp:TextBox></td>
     </tr>--%>

     <tr>
         <td><asp:Label ID="lblNbItems" runat="server">Nombre d'items :</asp:Label></td>
         <td><asp:TextBox ID="tbNbItems" runat="server"></asp:TextBox></td>
         
         <td>
          <asp:RequiredFieldValidator id="RequiredNbItems"
                    ControlToValidate="tbNbItems"
                    EnableClientScript="false"
                    ErrorMessage="Le nombre d'items doit être présent!"
                    runat="server"/>

                    

                    
                 <asp:RangeValidator id="RangeNbItems"
                    MinimumValue="0"
                    MaximumValue="32767"
                    Type="Double"
                    ControlToValidate="tbNbItems"
                    EnableClientScript="false"
                    ErrorMessage="Le nombre d'items n'est pas dans un format valide !"
                    runat="server"/>
            </td>
     </tr>

      <tr>
         <td><asp:Label ID="lblprixVente" runat="server">Prix de vente:</asp:Label></td>
         <td><asp:TextBox ID="tbPrixVente" runat="server"></asp:TextBox></td>
         
          <td>
         <asp:RegularExpressionValidator ID="rePrixVente" 
           ControlToValidate="tbPrixVente"   
           EnableClientScript="false" runat="server"
           ErrorMessage="Format invalide !" 
           ValidationExpression="^\d+([\.\,]\d{0,5})?$"> 
          </asp:RegularExpressionValidator> 

          <asp:CustomValidator runat="server" ID="adresseExiste" ControlToValidate="tbPrixVente"
            OnServerValidate="validerPrixVente" ErrorMessage="Le prix de vente doit être plus petit que le prix demandé! " />

         </td>

      </tr>

      <tr>
         <td><asp:Label ID="lblPois" runat="server">Pois de l'article:</asp:Label></td>
         <td><asp:TextBox ID="tbPois" runat="server"></asp:TextBox></td>
          
          <td>
           <asp:RequiredFieldValidator id="RequiredPois"
                    ControlToValidate="tbPois"
                    EnableClientScript="false"
                    ErrorMessage="Le pois doit être présent!"
                    runat="server"/>

              
           <asp:RegularExpressionValidator ID="rePoids" 
           ControlToValidate="tbPois"   
           EnableClientScript="false" runat="server"
           ErrorMessage="Format invalide !" 
           ValidationExpression="^\d+([\.\,]\d{0,5})?$"> 
           </asp:RegularExpressionValidator>   

                    
              

             </td>
      </tr>

      <tr>
         <td><asp:Label ID="lblDisponibilité" runat="server">Disponibilité:</asp:Label></td>
         <td><asp:CheckBox ID="cbDisponibilite" runat="server" Text="Le produit est-il visible par les clients?"  AutoPostBack="false" /> </td>
      </tr>
  </Table>

         <asp:Button ID="btnAjout" Text="Ajouter le produit" runat="server" OnClick="validationSaisie"/>
    </div>

  </asp:Content>
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuppressionProduits.aspx.cs" Inherits="Puces_R.SuppressionProduits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divEnTete">
         <asp:Label ID="lblTitre"  Text="Suppression d'un produit " runat="server"/><br />
        
     </div> 

     <div>   

     <table>

     
     
     <tr>
         <td><asp:Label ID="lblCategorieProduits"  runat="server">Catégorie :</asp:Label></td>
         <td><asp:DropDownList ID="ddlCategorieProduits"  runat="server" enabled="false"></asp:DropDownList></td>
     </tr>

     <tr>

        <td><asp:Label ID="lblDesc" runat="server">Descrption abrégée:</asp:Label> </td> 
         <td><asp:TextBox ID="tbDescAbregee" runat="server" enabled="false"></asp:TextBox>
        </td> 
         

     </tr>

     <tr>
        <td> <asp:Label ID="lblPrix" runat="server">Prix demandé:</asp:Label>
        </td>

        <td> <asp:TextBox ID="tbPrix" runat="server" enabled="false"/><br />

      </td>

     </tr>

     <tr>
          <td><asp:Label ID="lblDescComplete" runat="server">Descrption Complète:</asp:Label></td>
        <td> <asp:TextBox id="tbDescComplete"  TextMode="multiline" runat="server"  placeholder="Entrez le message"  title="aide" Enabled="false"></asp:TextBox><br />
        

                   
                    
                    </td>
     </tr>

     <tr>
          <td><asp:Label ID="lblPhoto" runat="server">Photo du produit :</asp:Label> </td>
         
     </tr>

     <tr>
          <td colspan="2" > <asp:Image ID="imgProduits" ImageUrl="logo.png" Width="350" Height="250" runat="server" /> </td><td></td>
         
     </tr>
     <%--<tr>
         <td><asp:Label ID="lblDateCreation" runat="server">Date de création :</asp:Label></td>
         <td><asp:TextBox ID="tbDateCreation" runat="server"></asp:TextBox></td>
     </tr>--%>

     <tr>
         <td><asp:Label ID="lblNbItems" runat="server">Nombre d'items :</asp:Label></td>
         <td><asp:TextBox ID="tbNbItems" runat="server" enabled="false"></asp:TextBox>
         

                    </td>


     </tr>

<%--      <tr>
         <td><asp:Label ID="lblprixVente" runat="server">Prix de vente:</asp:Label></td>
         <td><asp:TextBox ID="tbPrixVente" runat="server"></asp:TextBox><br /></td>
      </tr>

      <tr>
         <td><asp:Label ID="lblDateVente" runat="server">Date de vente:</asp:Label></td>
         <td><asp:TextBox ID="tbDateVente" runat="server"></asp:TextBox><br /></td>
      </tr>--%>

      <tr>
         <td><asp:Label ID="lblPois" runat="server">Pois de l'article:</asp:Label></td>
         <td><asp:TextBox ID="tbPois" runat="server" enabled="false"></asp:TextBox><br />
          
          

             </td>
      </tr>

      <tr>
         <td><asp:Label ID="lblDisponibilité" runat="server">Disponibilité:</asp:Label></td>
         <td><asp:CheckBox ID="cbDisponibilite" runat="server" Text="Le produit est-il visible par les clients?" enabled="false" /> </td>
      </tr>
         </Table>

         <asp:Button ID="btnAjout" Text="Confirmer la suppression!" runat="server" />
    </div>
    </form>
</body>
</html>
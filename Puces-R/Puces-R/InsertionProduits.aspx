<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertionProduits.aspx.cs" Inherits="Puces_R.InsertionProduits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Insertion d'un nouveau produit.</title>
</head>
<body>
    <form id="frmInscription" runat="server">
    <div id="divEnTete">
         <asp:Label ID="lblTitre"  Text="Insertion d'un nouveau produit" runat="server"/><br />
        
     </div> 

     <div>   

     <table>

     
     
     <tr>
         <td><asp:Label ID="lblCategorieProduits"  runat="server">Catégorie :</asp:Label></td>
         <td><asp:DropDownList ID="ddlCategorieProduits"  runat="server"></asp:DropDownList></td>
     </tr>

     <tr>

        <td><asp:Label ID="lblDesc" runat="server">Descrption abrégée:</asp:Label> </td> 
         <td><asp:TextBox ID="tbDescAbregee" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator id="RequiredAbregee"
                    ControlToValidate="tbDescAbregee"
                    EnableClientScript="false"
                    ErrorMessage="Description abrégée absente!"
                    runat="server"/></td> 
         

     </tr>

     <tr>
        <td> <asp:Label ID="lblPrix" runat="server">Prix demandé:</asp:Label>
        </td>

        <td> <asp:TextBox ID="tbPrix" runat="server" /><br />

         <asp:RequiredFieldValidator id="RequiredFieldValidator1"
                    ControlToValidate="tbPrix"
                    EnableClientScript="false"
                    ErrorMessage="Prix absent!"
                    runat="server"/>
           <asp:RangeValidator id="RangePrixDemande"
                    MinimumValue="0,01"
                    MaximumValue="9999999999"
                    Type="Double"
                    ControlToValidate="tbPrix"
                    EnableClientScript="false"
                    ErrorMessage="Le prix n'est pas dans un format valide!"
                    runat="server"/>
      </td>

     </tr>

     <tr>
          <td><asp:Label ID="lblDescComplete" runat="server">Descrption Complète:</asp:Label></td>
        <td> <asp:TextBox ID="tbDescComplete" runat="server"></asp:TextBox><br />
        
         <asp:RequiredFieldValidator id="RequiredComplete"
                    ControlToValidate="tbDescComplete"
                    EnableClientScript="false"
                    ErrorMessage="Description non présente."
                    runat="server"/>
                    
                   
                    
                    </td>
     </tr>

     <tr>
          <td><asp:Label ID="lblPhoto" runat="server">Photo du produit :</asp:Label> </td>
         <td> <asp:Button ID="btnAjoutPhoto" Text="Téléverser une photo" runat="server"/> </td>
     </tr>

     <%--<tr>
         <td><asp:Label ID="lblDateCreation" runat="server">Date de création :</asp:Label></td>
         <td><asp:TextBox ID="tbDateCreation" runat="server"></asp:TextBox></td>
     </tr>--%>

     <tr>
         <td><asp:Label ID="lblNbItems" runat="server">Nombre d'items :</asp:Label></td>
         <td><asp:TextBox ID="tbNbItems" runat="server"></asp:TextBox>
         
          <asp:RequiredFieldValidator id="RequiredNbItems"
                    ControlToValidate="tbNbItems"
                    EnableClientScript="false"
                    ErrorMessage="Le nombre d'items doit être présent!"
                    runat="server"/>

                    </td>

                    
                 <asp:RangeValidator id="RangeNbItems"
                    MinimumValue="0"
                    MaximumValue="32767"
                    Type="Double"
                    ControlToValidate="tbNbItems"
                    EnableClientScript="false"
                    ErrorMessage="Le nombre d'items n'est pas dans un format valide !"
                    runat="server"/>
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
         <td><asp:TextBox ID="tbPois" runat="server"></asp:TextBox><br />
          
           <asp:RequiredFieldValidator id="RequiredPois"
                    ControlToValidate="tbPois"
                    EnableClientScript="false"
                    ErrorMessage="Le pois doit être présent!"
                    runat="server"/>

                  

                    
                 <asp:RangeValidator id="RangePois"
                    MinimumValue="0,000000000001"
                    MaximumValue="32767"
                    Type="Double"
                    ControlToValidate="tbPois"
                    EnableClientScript="false"
                    ErrorMessage="Le format n'est pas valide!"
                    runat="server"/>

             </td>
      </tr>

      <tr>
         <td><asp:Label ID="lblDisponibilité" runat="server">Disponibilité:</asp:Label></td>
         <td><asp:CheckBox ID="cbDisponibilite" runat="server" Text="Le produit est-il visible par les clients?"  AutoPostBack="true" /> </td>
      </tr>
         </Table>

         <asp:Button ID="btnAjout" Text="Ajouter le produit" runat="server" OnClick="validationSaisie"/>
    </div>
    </form>
</body>
</html>

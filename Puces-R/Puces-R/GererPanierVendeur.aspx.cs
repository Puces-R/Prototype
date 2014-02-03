using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Puces_R.Controles;

namespace Puces_R
{
    public partial class GererPanierVendeur : System.Web.UI.Page
    {

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT TOP 5 (C.Nom + C.Prenom) AS NomC, C.NoClient,V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit inner join PPClients AS C on A.NoClient = C.NoClient where A.NoVendeur=" + Session["ID"] + " GROUP BY V.NomAffaires, A.NoVendeur, C.Nom,C.Prenom,C.NoClient", myConnection);
            DataTable tablePaniers = new DataTable();
            adapteurPaniers.Fill(tablePaniers);

            rptPaniers.DataSource = new DataView(tablePaniers);
            rptPaniers.DataBind();
        }

        protected void rptPaniers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink hypVendeur = (HyperLink)item.FindControl("hypVendeur");
                Label lblSousTotal = (Label)item.FindControl("lblSousTotal");
                NettoyerPanier ctrPanier = (NettoyerPanier)item.FindControl("ctrPanierN");
                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                String vendeur = (String)drvPanier["NomAffaires"];
                decimal sousTotal = (decimal)drvPanier["SousTotal"];
                long noVendeur = (long)drvPanier["NoVendeur"];
                long noClient = (long)drvPanier["NoClient"];

                //long numero = (long)drvPanier["NoClient"];
                //ctrPanier.NoClient = (long)numero;

                hypVendeur.Text = vendeur;
                hypVendeur.NavigateUrl = "Panier.aspx?noclient=" + noClient + "&novendeur=" + noVendeur;

                ctrPanier.NoClient = noClient;
                ctrPanier.NoVendeur = (int)Session["ID"];

                lblSousTotal.Text = sousTotal.ToString("C");
            }
        }

    }
}
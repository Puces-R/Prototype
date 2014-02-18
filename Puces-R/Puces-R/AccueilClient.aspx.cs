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
    public partial class AccueilClient : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, true, false, false);

                SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT A.NoVendeur, V.NomAffaires FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs V ON A.NoVendeur = V.NoVendeur WHERE A.NoClient = " + Session["ID"] + " GROUP BY A.NoVendeur, V.NomAffaires", myConnection);
                DataTable tablePaniers = new DataTable();
                adapteurPaniers.Fill(tablePaniers);

                mvPaniers.ActiveViewIndex = tablePaniers.Rows.Count > 0 ? 0 : 1;

                rptPaniers.DataSource = new DataView(tablePaniers);
                rptPaniers.DataBind();
            }
        }

        protected void rptPaniers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                BoitePanier ctrBoitePanier = (BoitePanier)item.FindControl("ctrBoitePanier");
                
                DataRowView drvPanier = (DataRowView)e.Item.DataItem;

                long noVendeur = (long)drvPanier["NoVendeur"];
                string nomAffaires = (string)drvPanier["NomAffaires"];

                ctrBoitePanier.NoVendeur = noVendeur;
                ctrBoitePanier.NoClient = (int)Session["ID"];
                ctrBoitePanier.Titre = nomAffaires;
                ctrBoitePanier.ChargerArticlesEnPanier();
                ctrBoitePanier.NavigateUrl = Chemin.Ajouter("~/Panier.aspx?novendeur=" + noVendeur, "Retour à l'accueil");
            }
        }
    }
}
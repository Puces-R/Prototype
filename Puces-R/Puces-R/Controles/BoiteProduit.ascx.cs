using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;

namespace Puces_R.Controles
{
    public partial class BoiteProduit : System.Web.UI.UserControl
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        public long NoProduit
        {
            get
            {
                return (long)ViewState["NoProduit"];
            }
            set
            {
                ViewState["NoProduit"] = value;
            }
        }

        public int NoSequentiel
        {
            get
            {
                return (int)ViewState["NoSequentiel"];
            }
            set
            {
                ViewState["NoSequentiel"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            myConnection.Open();

            SqlCommand commandeProduit = new SqlCommand("SELECT TOP(1) P.NoProduit, P.Photo, C.Description, P.Nom, P.PrixDemande, P.NombreItems, P.DateCreation, AVG(E.Cote) AS Evaluation FROM PPProduits P INNER JOIN PPCategories C ON C.NoCategorie = P.NoCategorie LEFT JOIN PPEvaluations E ON E.NoProduit = P.NoProduit WHERE P.NoProduit = " + NoProduit + " GROUP BY P.NoProduit, P.Photo, C.Description, P.Nom, P.PrixDemande, P.NombreItems, P.DateCreation", myConnection);

            SqlDataReader sdrProduit = commandeProduit.ExecuteReader();
            sdrProduit.Read();

            Object photo = sdrProduit["Photo"];
            String urlImage;
            if (photo is DBNull)
            {
                urlImage = "~/Images/image_non_disponible.png";
            }
            else
            {
                urlImage = "~/Images/Televerse/" + (String)photo;
            }
            String strCategorie = (String)sdrProduit["Description"];
            String strDescriptionAbregee = (String)sdrProduit["Nom"];
            decimal decPrixDemande = (decimal)sdrProduit["PrixDemande"];
            short intQuantite = (short)sdrProduit["NombreItems"];

            if (sdrProduit["Evaluation"] is DBNull)
            {
                lblEvaluation.Text = "Aucune évaluation";
            }
            else
            {
                decimal evaluation = (decimal)sdrProduit["Evaluation"];
                lblEvaluation.Text = "Cote moyenne: " + evaluation.ToString("N1") + " / 5";
            }

            lblNoProduit.Text = "No. " + NoProduit.ToString();
            imgProduit.ImageUrl = urlImage;
            hypDescriptionAbregee.Text = NoSequentiel + ". " + strDescriptionAbregee;
            hypDescriptionAbregee.NavigateUrl = Chemin.Ajouter("DetailsProduit.aspx?noproduit=" + NoProduit, "Retour aux produits");
            lblCategorie.Text = strCategorie;
            lblPrixDemande.Text = "Prix demandé: " + decPrixDemande.ToString("C");
            if (intQuantite > 0)
            {
                lblQuantite.Text = "Quantité: " + intQuantite.ToString();
            }
            else
            {
                lblQuantite.Text = "En rupture de stock";
                lblQuantite.ForeColor = Color.Red;
            }

            myConnection.Close();
        }
    }
}
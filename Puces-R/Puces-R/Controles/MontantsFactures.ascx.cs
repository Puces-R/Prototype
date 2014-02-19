using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R.Controles
{
    public partial class MontantsFactures : System.Web.UI.UserControl
    {
        SqlConnection myConnection = Librairie.Connexion;

        private Facture facture;

        public decimal GrandTotal
        {
            get
            {
                return facture.GrandTotal;
            }
        }

        public bool Enabled
        {
            set
            {
                ddlModesLivraison.Enabled = value;
            }
        }

        public String MessageErreur
        {
            get
            {
                return lblMessageErreur.Text;
            }
            set
            {
                mvPartieBas.ActiveViewIndex = (value == null ? 0 : 1);
                lblMessageErreur.Text = value;
            }
        }

        public long NoVendeur
        {
            get
            {
                return (long)ViewState["NoVendeur"];
            }
            set
            {
                ViewState["NoVendeur"] = value;
            }
        }

        public long NoCommande
        {
            get
            {
                return (long)ViewState["NoCommande"];
            }
            set
            {
                ViewState["NoCommande"] = value;
            }
        }

        public short CodeLivraison
        {
            get
            {
                if (ViewState["CodeLivraison"] == null)
                {
                    return 1;
                }
                else
                {
                    return (short)ViewState["CodeLivraison"];
                }
            }
            set
            {
                ViewState["CodeLivraison"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            CalculerCout();
        }

        public void CalculerCout()
        {
            SqlDataAdapter adapteurCategories = new SqlDataAdapter("SELECT * FROM PPTypesLivraison", myConnection);
            DataTable tableCategories = new DataTable();
            adapteurCategories.Fill(tableCategories);

            ddlModesLivraison.DataSource = tableCategories;
            ddlModesLivraison.DataTextField = "Description";
            ddlModesLivraison.DataValueField = "CodeLivraison";
            ddlModesLivraison.DataBind();

            if (ViewState["NoCommande"] != null)
            {
                facture = new Facture(NoCommande, CodeLivraison);
                mvPartieBas.ActiveViewIndex = 0;
            }
            else if (ViewState["NoVendeur"] != null)
            {
                facture = new Facture((int)Session["ID"], NoVendeur, CodeLivraison);
                lblTauxTPS.Text = "(" + facture.TauxTPS.ToString("P3") + ")";
                lblTauxTVQ.Text = "(" + facture.TauxTVQ.ToString("P3") + ")";

                if (facture.PoidsTotal > facture.PoidsMaximal)
                {
                    MessageErreur = "Poids maximal de " + facture.PoidsMaximal + " lbs. excédé.";
                }
            }

            lblPoidsTotal.Text = facture.PoidsTotal.ToString() + " lbs.";
            lblSousTotal.Text = facture.SousTotal.ToString("C");
            ddlModesLivraison.SelectedValue = CodeLivraison.ToString();
            lblLivraison.Text = facture.PrixLivraison.ToString("C");
            lblTPS.Text = facture.PrixTPS.ToString("C");
            lblTVQ.Text = facture.PrixTVQ.ToString("C");
            lblGrandTotal.Text = facture.GrandTotal.ToString("C");

            myConnection.Close();
        }

        protected void ddlModesLivraison_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CodeLivraison = short.Parse(ddlModesLivraison.SelectedValue);
        }
    }
}
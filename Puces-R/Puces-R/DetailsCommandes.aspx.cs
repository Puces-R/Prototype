using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class DetailsCommandes : System.Web.UI.Page
    {
        int noCommande = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!int.TryParse(Request.Params["noCommande"], out noCommande))
            {
                Response.Redirect("Connexion.aspx");
            }
            else
            {
                chargerDonnees();
                //Response.Write(noProduit.ToString());
            }
        }

        protected void chargerDonnees()
        {
            SqlConnection dbConn = new SqlConnection();
            String maChaineDeConnexion = "Data Source=sqlinfo.cgodin.qc.ca;Initial Catalog=BD6B8_424R;Persist Security Info=True;User ID=6B8equipe424r;Password=Password2";
            SqlConnection maConnexion = new SqlConnection();
            maConnexion.ConnectionString = maChaineDeConnexion;
            maConnexion.Open();

            SqlCommand maCommande = new SqlCommand("select * from PPCommandes where NoCommande=" + noCommande, maConnexion);
            object rep = maCommande.ExecuteScalar();

            if (rep != null)
            {

                SqlDataReader repT = maCommande.ExecuteReader();
                while (repT.Read())
                {
                    tbNoCommande.Text = Convert.ToString((Int64)repT[0]);
                    tbNoClient.Text = Convert.ToString((Int64)repT[1]);
                    tbNoVendeur.Text = Convert.ToString((Int64)repT[2]);
                    tbDate.Text = Convert.ToString((DateTime)repT[3]);
                    tbLivraisonM.Text = Convert.ToString(repT[4]);
                    tbLivraisonType.Text = Convert.ToString(repT[5]);
                    tbMontant.Text = Convert.ToString(repT[6]);
                    TbTps.Text = Convert.ToString(repT[7]);
                    tbTvq.Text = Convert.ToString(repT[8]);
                    tbPoids.Text = Convert.ToString(repT[9]);
                    tbStatut.Text = Convert.ToString((String)repT[10]);
                    tbNoAutorisation.Text = (String)repT[11];
                }
            }

            else
            {
                Response.Redirect("Connexion.aspx");
            }
            maConnexion.Close();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Puces_R
{
    public partial class gerer_inactivite_vendeurs : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string req_inactif = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable tableInnactif1 = charge_inactifs1();

                rptInnactifs1.DataSource = new DataView(tableInnactif1);
                rptInnactifs1.DataBind();
            }

            if (Session["err_msg"] != null)
                if (Session["err_msg"] != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }
        }

        private DataTable charge_inactifs1()
        {
            req_inactif += "SELECT PPVendeurs.NoVendeur, NomAffaires, Nom, Prenom, Rue, Ville, Pays, Tel1, AdresseEmail, MaxLivraison, LivraisonGratuite, Taxes, Pourcentage, DateCreation ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, MAX(DATEADD(day, 365,PPProduits.DateVente)) maxdate ";
            req_inactif += "					FROM PPVendeurs, PPProduits   ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPProduits.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R2 ";
            req_inactif += "WHERE R2.maxdate < GETDATE() ";
            req_inactif += "INTERSECT ";
            req_inactif += "SELECT PPVendeurs.NoVendeur, NomAffaires, Nom, Prenom, Rue, Ville, Pays, Tel1, AdresseEmail, MaxLivraison, LivraisonGratuite, Taxes, Pourcentage, DateCreation ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, MAX(DATEADD(day,365,PPCommandes.NoCommande)) maxdate ";
            req_inactif += "					FROM PPVendeurs, PPCommandes   ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPCommandes.NoCommande ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R3 ";
            req_inactif += "WHERE R3.maxdate < GETDATE() ";
            req_inactif += "UNION ";
            req_inactif += "SELECT PPVendeurs.NoVendeur, NomAffaires, Nom, Prenom, Rue, Ville, Pays, Tel1, AdresseEmail, MaxLivraison, LivraisonGratuite, Taxes, Pourcentage, DateCreation ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, COUNT(*) nbCommandes ";
            req_inactif += "					FROM PPVendeurs, PPCommandes ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPCommandes.NoCommande ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R5 ";
            req_inactif += "WHERE R5.nbCommandes = 0 ";
            req_inactif += "INTERSECT ";
            req_inactif += "SELECT PPVendeurs.NoVendeur, NomAffaires, Nom, Prenom, Rue, Ville, Pays, Tel1, AdresseEmail, MaxLivraison, LivraisonGratuite, Taxes, Pourcentage, DateCreation ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, COUNT(*) nbProduits ";
            req_inactif += "					FROM PPVendeurs, PPProduits ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPProduits.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R4 ";
            req_inactif += "WHERE R4.nbProduits = 0; ";


            SqlDataAdapter adapteurInnactif1 = new SqlDataAdapter(req_inactif, myConnection);
            DataTable tableInnactif1 = new DataTable();
            adapteurInnactif1.Fill(tableInnactif1);
            myConnection.Close();

            return tableInnactif1;
        }

        protected void rptInnactifs1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label titre_inactif1 = (Label)item.FindControl("titre_inactif1");
                Label addr_inactif1 = (Label)item.FindControl("addr_inactif1");
                Label tels_inactif1 = (Label)item.FindControl("tels_inactif1");
                Label courriel_inactif1 = (Label)item.FindControl("courriel_inactif1");
                Label charge_max_inactif1 = (Label)item.FindControl("charge_max_inactif1");
                Label livraison_gratuite = (Label)item.FindControl("livraison_gratuite");
                Label date_inactif1 = (Label)item.FindControl("date_inactif1");
                Button btnRefuser = (Button)item.FindControl("btn_refuser");
                Button btn_accepter = (Button)item.FindControl("btn_accepter");
                TextBox cont_mail_acceptation = (TextBox)item.FindControl("cont_mail_acceptation");
                TextBox cont_mail_refus = (TextBox)item.FindControl("cont_mail_refus");

                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;

                titre_inactif1.Text = drvinactif1["NomAffaires"].ToString() + ", par " + drvinactif1["Prenom"].ToString() + " " + drvinactif1["Nom"].ToString();
                addr_inactif1.Text = drvinactif1["Rue"].ToString() + ", " + drvinactif1["Ville"].ToString() + ", " + drvinactif1["Pays"].ToString();
                tels_inactif1.Text = drvinactif1["Tel1"].ToString();
                courriel_inactif1.Text = drvinactif1["AdresseEmail"].ToString();
                charge_max_inactif1.Text = drvinactif1["MaxLivraison"].ToString() + "Kg";
                livraison_gratuite.Text = drvinactif1["LivraisonGratuite"].ToString();
                date_inactif1.Text = drvinactif1["DateCreation"].ToString();
                //btnRefuser.CommandArgument = drvinactif1["AdresseEmail"].ToString();
                //btn_accepter.CommandArgument = drvinactif1["AdresseEmail"].ToString();
            }
        }
    }
}
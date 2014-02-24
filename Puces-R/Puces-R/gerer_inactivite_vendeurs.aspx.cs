using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Puces_R
{
    public partial class gerer_inactivite_vendeurs : System.Web.UI.Page
    {
        SqlConnection myConnection = Librairie.Connexion;
        string req_inactif = ""; 
        string whereClause, orderByClause = " ORDER BY ";
        int anneesMaximal;
        PagedDataSource pdsDemandes = new PagedDataSource();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text.Trim() != string.Empty)
            {
                String colonne = "PPVendeurs.NomAffaires";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "PPVendeurs.NomAffaires";
                        break;
                }
                whereParts.Add(colonne + " LIKE @critere");
            }

            whereParts.Add("PPVendeurs.NoVendeur IN ");

            if (whereParts.Count > 0)
            {
                whereClause += " WHERE " + string.Join(" AND ", whereParts);
            }

            //String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += "PPVendeurs.NoVendeur ";
                    break;
                case 1:
                    orderByClause += "PPVendeurs.NomAffaires ";
                    break;
                case 2:
                    orderByClause += "PPVendeurs.DateCreation DESC ";
                    break;
            }
            
            anneesMaximal = int.Parse(ddlTempsInnactivite.SelectedValue);
                                   
            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }
            
            //((SiteMaster)(Master.Master)).Titre = "Gestion de l'inactivité des vendeurs";
            Master.ChargerItems += charge_inactifs1;

            if (!IsPostBack)
            {
                Master.AfficherPremierePage();
            }

            if (Session["msg"] != null)
                if (Session["msg"].ToString() != "")
                {
                    div_msg.InnerText = Session["msg"].ToString();
                    Session["msg"] = "";
                }

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

            Librairie.activer_cocher_tout(div_chck); 
        }

        private void charge_inactifs1(object sender, EventArgs e)
        {
            charge_inactifs1();
        }


        private DataTable charge_inactifs1()
        {
            req_inactif = "SELECT * FROM PPVendeurs " + whereClause;
            req_inactif += "( SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, MAX(DATEADD(yy, " + anneesMaximal + ",PPProduits.DateCreation)) maxdate ";
            req_inactif += "					FROM PPVendeurs, PPProduits   ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPProduits.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R2 ";
            req_inactif += "WHERE R2.maxdate < GETDATE() ";
            req_inactif += "AND PPVendeurs.NoVendeur = R2.NoVendeur ";
            req_inactif += "INTERSECT ";
            req_inactif += "SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, MAX(DATEADD(yy," + anneesMaximal + ",PPCommandes.DateCommande)) maxdate ";
            req_inactif += "					FROM PPVendeurs, PPCommandes   ";
            req_inactif += "					WHERE PPVendeurs.NoVendeur = PPCommandes.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R3 ";
            req_inactif += "WHERE R3.maxdate < GETDATE() ";
            req_inactif += "AND PPVendeurs.NoVendeur = R3.NoVendeur ";
            req_inactif += "UNION ";
            req_inactif += "SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, COUNT(NoCommande) nbCommandes ";
            req_inactif += "					FROM PPVendeurs LEFT OUTER JOIN PPCommandes ";
            req_inactif += "					ON PPVendeurs.NoVendeur = PPCommandes.NoCommande ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R5 ";
            req_inactif += "WHERE R5.nbCommandes = 0 ";
            req_inactif += "AND PPVendeurs.NoVendeur = R5.NoVendeur ";
            req_inactif += "AND DATEADD(yy," + anneesMaximal + ", DateCreation) <= GETDATE()";
            req_inactif += "INTERSECT ";
            req_inactif += "SELECT PPVendeurs.NoVendeur ";
            req_inactif += "FROM PPVendeurs, ( ";
            req_inactif += "					SELECT PPVendeurs.NoVendeur, COUNT(NoProduit) nbProduits ";
            req_inactif += "					FROM PPVendeurs LEFT OUTER JOIN PPProduits ";
            req_inactif += "					ON PPVendeurs.NoVendeur = PPProduits.NoVendeur ";
            req_inactif += "					GROUP BY PPVendeurs.NoVendeur ";
            req_inactif += "				  ) R4 ";
            req_inactif += "WHERE R4.nbProduits = 0 ";
            req_inactif += "AND PPVendeurs.NoVendeur = R4.NoVendeur ) AND ISNULL(Statut, 0) <> 1 " + orderByClause;
            //req_inactif += orderByClause;

            SqlDataAdapter adapteurInnactif1 = new SqlDataAdapter(req_inactif, myConnection);
            if (txtCritereRecherche.Text.Trim() != string.Empty)
            {
                adapteurInnactif1.SelectCommand.Parameters.AddWithValue("@critere", "%" + txtCritereRecherche.Text.Trim() + "%");
            }
            DataTable tableInnactif1 = new DataTable();
            adapteurInnactif1.Fill(tableInnactif1);

            pdsDemandes.DataSource = new DataView(tableInnactif1);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptInnactifs1.DataSource = pdsDemandes;
            rptInnactifs1.DataBind();
            
            myConnection.Close();

            return tableInnactif1;
        }

        protected void rptInnactifs1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_nom_affaire = (LinkButton)item.FindControl("lbl_nom_affaire");
                LinkButton lbl_nom_vendeur = (LinkButton)item.FindControl("lbl_nom_vendeur");
                Button btn_desactiver = (Button)item.FindControl("btn_desactiver");
                
                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (pdsDemandes.CurrentPageIndex * pdsDemandes.PageSize + e.Item.ItemIndex + 1).ToString();
                lbl_nom_affaire.Text = drvinactif1["NomAffaires"].ToString();
                lbl_nom_vendeur.Text = drvinactif1["Prenom"].ToString() + " " + drvinactif1["Nom"].ToString();
                //btnRefuser.CommandArgument = drvinactif1["AdresseEmail"].ToString();
                btn_desactiver.CommandArgument = drvinactif1["NoVendeur"].ToString();

                lbl_num.CommandArgument = drvinactif1["NoVendeur"].ToString();
                lbl_nom_affaire.CommandArgument = drvinactif1["NoVendeur"].ToString();
                lbl_nom_vendeur.CommandArgument = drvinactif1["NoVendeur"].ToString();
            }
        }

        protected void desactiver_vendeur(object sender, CommandEventArgs e)
        {
            Session["desactiver_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect(Chemin.Ajouter("verdict_desactiver.aspx", "Retour à la liste des vendeurs innactifs"));  
        }

        protected void desactiver_liste(object sender, EventArgs e)
        {
            string liste = "";
            foreach (RepeaterItem vendeur in rptInnactifs1.Items)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox cb_desactiver = (System.Web.UI.HtmlControls.HtmlInputCheckBox)vendeur.FindControl("cb_desactiver");
                if (cb_desactiver.Checked)
                {
                    Button btn_desactiver = (Button)vendeur.FindControl("btn_desactiver");
                    liste += btn_desactiver.CommandArgument + ", ";
                }
            }

            if (liste == "")
                Session["err_msg"] = "Aucun vendeur selectionné";
            else
            {
                Session["desactiver_liste"] = liste.Remove(liste.Length - 2);

                Response.Redirect(Chemin.Ajouter("verdict_desactiver.aspx", "Retour à la liste des vendeurs innactifs"));
            }
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }
    }
}
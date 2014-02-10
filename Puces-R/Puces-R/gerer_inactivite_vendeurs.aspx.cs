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
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string req_inactif = ""; 
        string whereClause, orderByClause = " ORDER BY ";
        int anneesMaximal;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "PPVendeurs.NomAffaires";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = "PPVendeurs.NomAffaires";
                        break;
                }
                whereParts.Add(colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
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
            
            ((SiteMaster)(Master.Master)).Titre = "Gestion de l'inactivité des vendeurs";
            ((NavigationItems)Master).ChargerItems += charge_inactifs1;

            if (!IsPostBack)
            {
                ((NavigationItems)Master).AfficherPremierePage();
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
            DataTable tableInnactif1 = new DataTable();
            adapteurInnactif1.Fill(tableInnactif1);

            PagedDataSource pdsDemandes = new PagedDataSource();
            pdsDemandes.DataSource = new DataView(tableInnactif1);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = ((NavigationItems)Master).PageActuelle;
            ((NavigationItems)Master).NbPages = pdsDemandes.PageCount;

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
                Label lbl_num = (Label)item.FindControl("lbl_num");
                Label lbl_nom_affaire = (Label)item.FindControl("lbl_nom_affaire");
                Label date_inactif1 = (Label)item.FindControl("date_inactif1");
                Button btn_desactiver = (Button)item.FindControl("btn_desactiver");
                
                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (e.Item.ItemIndex + 1).ToString();
                lbl_nom_affaire.Text = drvinactif1["NomAffaires"].ToString();
                date_inactif1.Text = drvinactif1["DateCreation"].ToString();
                //btnRefuser.CommandArgument = drvinactif1["AdresseEmail"].ToString();
                btn_desactiver.CommandArgument = drvinactif1["NoVendeur"].ToString();
            }
        }

        protected void desactiver_vendeur(object sender, CommandEventArgs e)
        {
            Session["desactiver_vendeur"] = e.CommandArgument.ToString();
            Response.Redirect("verdict_desactiver.aspx");  
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

            Session["desactiver_liste"] = liste.Remove(liste.Length - 2);

            Response.Redirect("verdict_desactiver.aspx");
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            ((NavigationItems)Master).AfficherPremierePage();
        }
    }
}
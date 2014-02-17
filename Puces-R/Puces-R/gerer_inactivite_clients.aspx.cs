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
    public partial class gerer_inactivite_clients : System.Web.UI.Page
    {
        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");
        string req_inactif = "";
        string whereClause, orderByClause = " ORDER BY ";
        int anneesMaximal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, false, true);
            }
            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = " PPClients.Prenom + ' ' + PPClients.Nom ";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = " PPClients.Prenom + ' ' + PPClients.Nom ";
                        break;
                }
                whereParts.Add(colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
            }

            whereParts.Add("PPClients.NoClient IN ");

            if (whereParts.Count > 0)
            {
                whereClause += " WHERE " + string.Join(" AND ", whereParts);
            }

            //String orderByClause = " ORDER BY ";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += "PPClients.NoClient ";
                    break;
                case 1:
                    orderByClause += "PPClients.Prenom, PPClients.Nom";
                    break;
                case 2:
                    orderByClause += "PPClients.DateCreation DESC ";
                    break;
            }

            anneesMaximal = int.Parse(ddlTempsInnactivite.SelectedValue);

            if (Session["err_msg"] != null)
                if (Session["err_msg"].ToString() != "")
                {
                    Response.Write(Session["err_msg"]);
                    Session["err_msg"] = "";
                }

            Master.ChargerItems += charge_inactifs1;

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
            req_inactif = "SELECT * FROM PPClients " + whereClause;
            req_inactif += " ( SELECT PPClients.NoClient ";
            req_inactif += " FROM PPClients, ( ";
            req_inactif += " 					SELECT PPClients.NoClient, MAX(DATEADD(yy, " + anneesMaximal + ",PPVendeursClients.DateVisite)) maxdate ";
            req_inactif += " 					FROM PPClients, PPVendeursClients   ";
            req_inactif += " 					WHERE PPClients.NoClient = PPVendeursClients.NoClient ";
            req_inactif += " 					GROUP BY PPClients.NoClient ";
            req_inactif += " 				  ) R2 ";
            req_inactif += " WHERE R2.maxdate < GETDATE()  ";
            req_inactif += " AND PPClients.NoClient = R2.NoClient ";
            req_inactif += " INTERSECT ";
            req_inactif += " SELECT PPClients.NoClient ";
            req_inactif += " FROM PPClients, ( ";
            req_inactif += " 					SELECT PPClients.NoClient, MAX(DATEADD(yy," + anneesMaximal + ",PPCommandes.DateCommande)) maxdate ";
            req_inactif += " 					FROM PPClients, PPCommandes   ";
            req_inactif += " 					WHERE PPClients.NoClient = PPCommandes.NoClient ";
            req_inactif += " 					GROUP BY PPClients.NoClient ";
            req_inactif += " 				  ) R3 ";
            req_inactif += " WHERE R3.maxdate < GETDATE()  ";
            req_inactif += " AND PPClients.NoClient = R3.NoClient ";
            req_inactif += " UNION ";
            req_inactif += " SELECT PPClients.NoClient ";
            req_inactif += " FROM PPClients, ( ";
            req_inactif += " 					SELECT PPClients.NoClient, COUNT(NoCommande) nbCommandes ";
            req_inactif += " 					FROM PPClients LEFT OUTER JOIN PPCommandes ";
            req_inactif += " 					ON PPClients.NoClient = PPCommandes.NoCommande ";
            req_inactif += " 					GROUP BY PPClients.NoClient ";
            req_inactif += " 				  ) R5 ";
            req_inactif += " WHERE R5.nbCommandes = 0 ";
            req_inactif += " AND PPClients.NoClient = R5.NoClient ";
            req_inactif += " INTERSECT ";
            req_inactif += " SELECT PPClients.NoClient ";
            req_inactif += " FROM PPClients, ( ";
            req_inactif += " 					SELECT PPClients.NoClient, COUNT(PPVendeursClients.NoClient) nbVisites ";
            req_inactif += " 					FROM PPClients LEFT OUTER JOIN PPVendeursClients ";
            req_inactif += " 					ON PPClients.NoClient = PPVendeursClients.NoClient ";
            req_inactif += " 					GROUP BY PPClients.NoClient ";
            req_inactif += " 				  ) R4 ";
            req_inactif += " WHERE R4.nbVisites = 0 ";
            req_inactif += " AND PPClients.NoClient = R4.NoClient ) AND ISNULL(Statut, 0) <> 1 " + orderByClause;
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
            //Response.Write(req_inactif);

            return tableInnactif1;
        }

        protected void rptInnactifs1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lbl_num = (LinkButton)item.FindControl("lbl_num");
                LinkButton lbl_nom_complet = (LinkButton)item.FindControl("lbl_nom_complet");
                LinkButton lbl_courriel = (LinkButton)item.FindControl("lbl_courriel");
                Button btn_desactiver = (Button)item.FindControl("btn_desactiver");

                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;

                lbl_num.Text = (e.Item.ItemIndex + 1).ToString();
                lbl_nom_complet.Text = drvinactif1["Prenom"].ToString() + " " + drvinactif1["Nom"].ToString();
                lbl_courriel.Text = drvinactif1["AdresseEmail"].ToString();
                btn_desactiver.CommandArgument = drvinactif1["NoClient"].ToString();

                lbl_num.CommandArgument = drvinactif1["NoClient"].ToString();
                lbl_nom_complet.CommandArgument = drvinactif1["NoClient"].ToString();
                lbl_courriel.CommandArgument = drvinactif1["NoClient"].ToString();
            }
        }

        protected void desactiver_client(object sender, CommandEventArgs e)
        {
            Session["desactiver_client"] = e.CommandArgument.ToString();
            Response.Redirect("verdict_desactiver_client.aspx");
        }

        protected void desactiver_liste(object sender, EventArgs e)
        {
            string liste = "";
            foreach (RepeaterItem client in rptInnactifs1.Items)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox cb_desactiver = (System.Web.UI.HtmlControls.HtmlInputCheckBox)client.FindControl("cb_desactiver");
                if (cb_desactiver.Checked)
                {
                    Button btn_desactiver = (Button)client.FindControl("btn_desactiver");
                    liste += btn_desactiver.CommandArgument + ", ";
                }
            }

            Session["desactiver_liste"] = liste.Remove(liste.Length - 2);
            Response.Redirect("verdict_desactiver_client.aspx");
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            ((NavigationItems)Master).AfficherPremierePage();
        }
    }
}
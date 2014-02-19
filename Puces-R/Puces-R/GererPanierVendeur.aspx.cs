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
        string req_inactif = "";
        string whereClause, orderByClause = "";
        int anneesMaximal;
        String havingClause = "";
        PagedDataSource pdsDemandes = new PagedDataSource();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Librairie.Autorisation(false, false, true, false);
            }
            SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT TOP 5 (C.Nom + C.Prenom) AS NomC, C.NoClient,V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit inner join PPClients AS C on A.NoClient = C.NoClient where A.NoVendeur=" + Session["ID"] + " GROUP BY V.NomAffaires, A.NoVendeur, C.Nom,C.Prenom,C.NoClient", myConnection);
            DataTable tablePaniers = new DataTable();
            adapteurPaniers.Fill(tablePaniers);


            List<String> whereParts = new List<String>();

            if (txtCritereRecherche.Text != string.Empty)
            {
                String colonne = "NomC";
                switch (ddlTypeRecherche.SelectedIndex)
                {
                    case 0:
                        colonne = " and (C.Nom + ' ' + C.Prenom) ";
                        break;
                }
                whereParts.Add(colonne + " LIKE '%" + txtCritereRecherche.Text + "%'");
            }


             whereClause = " WHERE A.NoVendeur = " + Session["ID"];

              havingClause = "";
             switch (ddlTempsInnactivite.SelectedIndex)
             {
                 case 0:
                     
                     break;
                 case 1:
                     havingClause+=(" HAVING MAX(A.DateCreation) <  DATEADD(month, -1, GetDate()) ");
                     break;
                 case 2:
                     havingClause += (" HAVING MAX(A.DateCreation) <  DATEADD(month, -2, GetDate()) ");
                     break;
                 case 3:
                     havingClause += (" HAVING MAX(A.DateCreation) <  DATEADD(month, -3, GetDate()) ");
                     break;
                 case 4:
                     havingClause += (" HAVING MAX(A.DateCreation) <  DATEADD(month, -6, GetDate()) ");
                     break;

             }

            
            //whereParts.Add("A.NoVendeur = "+Session["ID"]);

            if (whereParts.Count > 0)
            {
                whereClause += string.Join(" AND ", whereParts);
            }

             orderByClause = "";
            switch (ddlTrierPar.SelectedIndex)
            {
                case 0:
                    orderByClause += " ORDER BY DerniereMAJ DESC ";
                    break;
                case 1:
                    orderByClause += " ORDER BY SousTotal DESC";
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
            Librairie.activer_cocher_tout(div_chck, "cb_tout", "cbCocher"); 
        }

        private void charge_inactifs1(object sender, EventArgs e)
        {
            charge_inactifs1();
        }


        private DataTable charge_inactifs1()
        {
            req_inactif = "SELECT  (C.Nom + ' ' + C.Prenom) AS NomC, C.NoClient,V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal, MAX(A.DateCreation) AS DerniereMAJ FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit inner join PPClients AS C on A.NoClient = C.NoClient" + whereClause + " GROUP BY V.NomAffaires, A.NoVendeur, C.Nom,C.Prenom,C.NoClient " + havingClause + orderByClause;
           
            //req_inactif += orderByClause;

            SqlDataAdapter adapteurInnactif1 = new SqlDataAdapter(req_inactif, myConnection);
            DataTable tableInnactif1 = new DataTable();
            adapteurInnactif1.Fill(tableInnactif1);

            
            pdsDemandes.DataSource = new DataView(tableInnactif1);
            pdsDemandes.AllowPaging = true;
            pdsDemandes.PageSize = int.Parse(ddlParPage.SelectedValue);

            pdsDemandes.CurrentPageIndex = Master.PageActuelle;
            Master.NbPages = pdsDemandes.PageCount;

            rptInnactifs1.DataSource = pdsDemandes;
            rptInnactifs1.DataBind();

            mvCommandes.ActiveViewIndex = tableInnactif1.Rows.Count == 0 ? 1 : 0;
            myConnection.Close();

            return tableInnactif1;
        }

        protected void rptInnactifs1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lbl_num = (Label)item.FindControl("lbl_num");
                //Label lbl_nom_affaire = (Label)item.FindControl("lbl_nom_affaire");
                //Label lbl_no_vendeur = (Label)item.FindControl("lblNoVendeur");
                Label lbl_nom_vendeur = (Label)item.FindControl("lbl_nom_vendeur");
                Label lbl_date = (Label)item.FindControl("date_inactif1");
                Label lbl_NomClient = (Label)item.FindControl("lblNomClient");
                Label lblMontant = (Label)item.FindControl("lblMontant");
                Label lblPasActif = (Label)item.FindControl("lblInactif");
                Button btn_desactiver = (Button)item.FindControl("btn_desactiver");
                System.Web.UI.HtmlControls.HtmlInputCheckBox cbSupprimmer = (System.Web.UI.HtmlControls.HtmlInputCheckBox)item.FindControl("cb_desactiver");
                            
                           
                            
                DataRowView drvinactif1 = (DataRowView)e.Item.DataItem;




                lbl_num.Text = (pdsDemandes.PageSize * pdsDemandes.CurrentPageIndex + e.Item.ItemIndex + 1).ToString();

                //lbl_no_vendeur.Text = drvinactif1["NoVendeur"].ToString();
                //lbl_nom_affaire.Text = drvinactif1["NomAffaires"].ToString();
                //lbl_nom_vendeur.Text = drvinactif1["SousTotal"].ToString();
                lbl_NomClient.Text = drvinactif1["NomC"].ToString() == "" ? "Nom Inconnu" : drvinactif1["NomC"].ToString();
                lblMontant.Text = Convert.ToDecimal(drvinactif1["SousTotal"]).ToString("#0.00 $");
                lbl_date.Text = drvinactif1["DerniereMAJ"].ToString();

                DateTime myDate = DateTime.Now;
                DateTime newDate = myDate.AddMonths(-6);

                if ((DateTime)drvinactif1["DerniereMAJ"] < newDate)
                {
                    btn_desactiver.Visible = true;
                    cbSupprimmer.Attributes["class"] = "cbCocher";
                }
                else 
                {
                    lblPasActif.Visible = true;
                    cbSupprimmer.Attributes["Disabled"] = "false";

                    //cbSupprimmer.Style
                }
                //btnRefuser.CommandArgument = drvinactif1["AdresseEmail"].ToString();
                btn_desactiver.CommandArgument = drvinactif1["NoClient"].ToString() + "-" + drvinactif1["NomC"].ToString();
            }
        }

        protected void desactiver_vendeur(object sender, CommandEventArgs e)
        {
            Session["desactiver_panier"] = e.CommandArgument.ToString();
            Response.Redirect("DesactiverPanierVendeur.aspx");
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
                Response.Redirect("DesactiverPanierVendeur.aspx");
            }
        }

        protected void AfficherPremierePage(object sender, EventArgs e)
        {
            Master.AfficherPremierePage();
        }

        //SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    SqlDataAdapter adapteurPaniers = new SqlDataAdapter("SELECT TOP 5 (C.Nom + C.Prenom) AS NomC, C.NoClient,V.NomAffaires, A.NoVendeur, SUM(A.NbItems * P.PrixVente) AS SousTotal FROM PPArticlesEnPanier AS A INNER JOIN PPVendeurs AS V ON A.NoVendeur = V.NoVendeur INNER JOIN PPProduits AS P ON A.NoProduit = P.NoProduit inner join PPClients AS C on A.NoClient = C.NoClient where A.NoVendeur=" + Session["ID"] + " GROUP BY V.NomAffaires, A.NoVendeur, C.Nom,C.Prenom,C.NoClient", myConnection);
        //    DataTable tablePaniers = new DataTable();
        //    adapteurPaniers.Fill(tablePaniers);

        //    //rptPaniers.DataSource = new DataView(tablePaniers);
        //    //rptPaniers.DataBind();
        //}

        //protected void rptPaniers_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    RepeaterItem item = e.Item;

        //    if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
        //    {
        //        HyperLink hypVendeur = (HyperLink)item.FindControl("hypVendeur");
        //        Label lblSousTotal = (Label)item.FindControl("lblSousTotal");
        //        NettoyerPanier ctrPanier = (NettoyerPanier)item.FindControl("ctrPanierN");
        //        DataRowView drvPanier = (DataRowView)e.Item.DataItem;

        //        String vendeur = (String)drvPanier["NomAffaires"];
        //        decimal sousTotal = (decimal)drvPanier["SousTotal"];
        //        long noVendeur = (long)drvPanier["NoVendeur"];
        //        long noClient = (long)drvPanier["NoClient"];

        //        //long numero = (long)drvPanier["NoClient"];
        //        //ctrPanier.NoClient = (long)numero;

        //        hypVendeur.Text = vendeur;
        //        hypVendeur.NavigateUrl = "Panier.aspx?noclient=" + noClient + "&novendeur=" + noVendeur;

        //        ctrPanier.NoClient = noClient;
        //        ctrPanier.NoVendeur = (int)Session["ID"];

        //        lblSousTotal.Text = sousTotal.ToString("C");
        //    }
        //}

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class BoiteMessage : System.Web.UI.Page
    {
        SqlConnection connexion = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2;");

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlCommand cmdMessages = new SqlCommand("SELECT M.NoMessage, X.AdresseEmail, M.DateEnvoi, M.Sujet, M.Lu FROM PPMessages AS M INNER JOIN " +
                                                   "(SELECT NoClient AS No, AdresseEmail FROM PPClients UNION " +
                                                    "SELECT NoVendeur AS No, AdresseEmail FROM PPVendeurs UNION " + 
                                                    "SELECT NoGestionnaire AS No, AdresseEmail FROM PPGestionnaires) AS X " +
                                                    "ON M.Envoyeur = X.No " +
                                                    "WHERE (M.Recepteur = 10700) ORDER BY M.DateEnvoi DESC", connexion);
            connexion.Open();
            SqlDataReader sdr = cmdMessages.ExecuteReader();
            for (int i = 0 ; sdr.Read() ; i++)
            {
                LigneMessage l = (LigneMessage)Page.LoadControl("~/LigneMessage.ascx");
                ListeMessage.Controls.Add(l);
                l.De = sdr["AdresseEmail"].ToString();
                l.Sujet = sdr["Sujet"].ToString();
                l.Date = (DateTime)sdr["DateEnvoi"];
                l.Lu = (Boolean)sdr["Lu"];
                l.NoMessage = (Int64)sdr["NoMessage"];
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Puces_R
{
    public partial class NavigationItems : System.Web.UI.MasterPage
    {
        public event EventHandler ChargerItems;

        public ScriptManager ScriptManager
        {
            get
            {
                return ctrScriptManager;
            }
        }

        public bool CriteresVisibles
        {
            set
            {
                pnlBarreCritere.Visible = value;
            }
        }

        public int PageActuelle
        {
            get
            {
                return ctrNavigationHaut.PageActuelle;
            }
            set
            {
                ctrNavigationHaut.PageActuelle = value;
                ctrNavigationBas.PageActuelle = value;
            }
        }

        public int NbPages
        {
            get
            {
                return ctrNavigationHaut.NbPages;
            }
            set
            {
                ctrNavigationHaut.NbPages = value;
                ctrNavigationBas.NbPages = value;
            }
        }

        SqlConnection myConnection = new SqlConnection("Server=sqlinfo.cgodin.qc.ca;Database=BD6B8_424R;User Id=6B8equipe424r;Password=Password2");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChargerItems(this, EventArgs.Empty);

                if (Session["ID"] == null)
                {
                    Response.Redirect("Default.aspx", true);
                }
               
                AfficherPremierePage();
            }

            ctrNavigationBas.PageChangee += changerDePage;
            ctrNavigationHaut.PageChangee += changerDePage;
        }

        private void changerDePage(object sender, EventArgs e)
        {
            if (sender == ctrNavigationBas)
            {
                ctrNavigationHaut.PageActuelle = ctrNavigationBas.PageActuelle;
            }
            else
            {
                ctrNavigationBas.PageActuelle = ctrNavigationHaut.PageActuelle;
            }
            ChargerItems(this, EventArgs.Empty);
        }

        public void AfficherPremierePage()
        {
            ctrNavigationHaut.PageActuelle = 0;
            ctrNavigationBas.PageActuelle = 0;
            ChargerItems(this, EventArgs.Empty);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R.Controles
{
    public partial class NavigationParPage : System.Web.UI.UserControl
    {
        public event EventHandler PageChangee;

        public bool LigneBasVisible
        {
            set
            {
                pnlLigneBas.Visible = value;
            }
        }

        public bool LigneHautVisible
        {
            set
            {
                pnlLigneHaut.Visible = value;
            }
        }

        public int NbPages
        {
            get
            {
                if (ViewState["NbPages"] != null)
                {
                    return (int)ViewState["NbPages"];
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                ViewState["NbPages"] = value;
                changerVisibilite();
            }
        }

        public int PageActuelle
        {
            get
            {
                if (ViewState["PageActuelle"] != null)
                {
                    return (int)ViewState["PageActuelle"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (PageActuelle != value)
                {
                    ViewState["PageActuelle"] = value;
                    if (PageChangee != null)
                    {
                        PageChangee(this, EventArgs.Empty);
                    }
                }
                changerVisibilite();
            }
        }

        protected void btnFirst_OnClick(object sender, EventArgs e)
        {
            PageActuelle = 0;
        }

        protected void btnPrevious_OnClick(object sender, EventArgs e)
        {
            PageActuelle -= 1;
        }

        protected void btnNext_OnClick(object sender, EventArgs e)
        {
            PageActuelle += 1;
        }

        protected void btnLast_OnClick(object sender, EventArgs e)
        {
            PageActuelle = NbPages - 1;
        }

        private void changerVisibilite()
        {
            pnlBarreNavigation.Visible = (PageActuelle > 0) || (PageActuelle < NbPages - 1);
            pnlLeftNavigation.Visible = (PageActuelle > 0);
            pnlRightNavigation.Visible = (PageActuelle < NbPages - 1);
            lblInfoAuCentre.Text = "Page " + (PageActuelle + 1) + " de " + NbPages;
        }
    }
}
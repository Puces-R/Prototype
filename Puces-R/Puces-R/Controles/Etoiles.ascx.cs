using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R.Controles
{
    public partial class Etoiles : System.Web.UI.UserControl
    {
        public bool Modifiable
        {
            get
            {
                return (bool)ViewState["Modifiable"];
            }
            set
            {
                ViewState["Modifiable"] = value;
            }
        }

        public decimal Cote
        {
            get
            {
                if (ViewState["Cote"] == null)
                {
                    return -1;
                }
                return (decimal)ViewState["Cote"];
            }
            set
            {
                ViewState["Cote"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Cote != -1)
            {
                afficherCote();
            }
        }

        private void afficherCote()
        {
            String[] images = new String[5];

            for (int i = 0; i < 5; i++)
            {
                images[i] = "../Images/";

                decimal restant = Cote - i;

                if (restant <= 0)
                {
                    images[i] += "EtoileVide.png";
                }
                else if (restant >= 1)
                {
                    images[i] += "EtoilePleine.png";
                }
                else
                {
                    images[i] += "DemieEtoile.png";
                }
            }

            rptEtoiles.DataSource = images;
            rptEtoiles.DataBind();
        }

        protected void rptEtoiles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            {
                ImageButton imgEtoile = (ImageButton)item.FindControl("imgEtoile");

                imgEtoile.ImageUrl = (String)item.DataItem;
                imgEtoile.CommandArgument = (rptEtoiles.Items.Count + 1).ToString();
                imgEtoile.Enabled = Modifiable;
            }
        }

        protected void rptEtoiles_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Etoile")
            {
                Cote = decimal.Parse((String)e.CommandArgument);
                afficherCote();
            }
        }
    }
}
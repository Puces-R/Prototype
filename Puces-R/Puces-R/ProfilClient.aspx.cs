using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class ProfilClient : System.Web.UI.Page
    {
        protected void btnSauvegarder_OnClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (IsValid)
            {
                ctrProfil.Sauvegarder();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Puces_R.Controles
{
    public partial class MenuInvite : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Librairie.SelectionnerItemMenuActuel(ctrMenu.Items, Path.GetFileNameWithoutExtension(Request.Url.AbsoluteUri));
        }
    }
}
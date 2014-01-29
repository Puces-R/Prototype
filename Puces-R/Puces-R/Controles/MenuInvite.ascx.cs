﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Puces_R.Controles
{
    public partial class MenuInvite : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String urlPage = Path.GetFileNameWithoutExtension(Request.Url.AbsoluteUri);
            foreach (MenuItem item in ctrMenu.Items)
            {
                if (item.Selectable)
                {
                    String urlItem = Path.GetFileNameWithoutExtension(item.NavigateUrl);
                    item.Selected = String.Equals(urlItem, urlPage);
                }
            }
        }
    }
}
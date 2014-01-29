﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class Telephone : System.Web.UI.UserControl
    {
        public bool Obligatoire
        {
            set
            {
                reqTel.Visible = value;
            }
        }

        public string Label
        {
            set
            {
                lbl.Text = value;
            }
        }

        public string NoTelephone
        {
            get
            {
                string tel = tbReg.Text + tbPart1.Text + tbPart2.Text;
                return tel == string.Empty ? null : tel;
            }
            set
            {
                if (value == null || value.Trim() == string.Empty)
                {
                    tbReg.Text =
                    tbPart1.Text =
                    tbPart2.Text = "";
                }
                else
                {
                    string no = new string(value.Where(c => char.IsDigit(c)).ToArray());
                    tbReg.Text = no.Substring(0, 3);
                    tbPart1.Text = no.Substring(3, 3);
                    tbPart2.Text = no.Substring(6, 4);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void validerObligatoire(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = reqReg.IsValid || reqPart1.IsValid || reqPart2.IsValid;
        }

        protected void validerTelephone(object sender, ServerValidateEventArgs e)
        {
            if (!reqReg.IsValid && !reqPart1.IsValid && !reqPart2.IsValid)
            {
                reqReg.IsValid =
                reqPart1.IsValid =
                reqPart2.IsValid =
                e.IsValid = true;

            }
            else if (reqReg.IsValid && reqPart1.IsValid && reqPart2.IsValid &&
                     formatReg.IsValid && formatPart1.IsValid && formatPart2.IsValid)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
    }
}
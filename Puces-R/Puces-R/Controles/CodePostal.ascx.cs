﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Puces_R
{
    public partial class CodePostal : System.Web.UI.UserControl
    {
        public bool Obligatoire
        {
            set
            {
                reqCodePostal.Visible = value;
            }
        }

        public string Code
        {
            get
            {
                string code = tbPart1.Text + " " + tbPart2.Text;

                return code == string.Empty ? null : code.ToUpper();
            }
            set
            {
                if (value == null || value.Trim().Length < 6)
                {
                    tbPart1.Text = tbPart2.Text = "";
                }
                else
                {
                    string code = value.Replace("-", "").Replace(" ", "").Trim().ToUpper();
                    tbPart1.Text = code.Substring(0, 3);
                    tbPart2.Text = code.Substring(3, 3);
                }
            }
        }

        public static string Format(string code)
        {
            string s = code.Replace("-", "").Replace(" ", "").Trim().ToUpper();
            if (s.Length != 6)
            {
                return "";
            }
            return s.Substring(0, 3) + " " + s.Substring(3, 3);
        }

        protected void validerObligatoire(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = reqPart1.IsValid || reqPart2.IsValid;
        }

        protected void validerFormat(object sender, ServerValidateEventArgs e)
        {
            if (!reqPart1.IsValid && !reqPart2.IsValid)
            {
                reqPart1.IsValid =
                reqPart2.IsValid =
                e.IsValid = true;                
            }
            else if (formatPart1.IsValid && formatPart2.IsValid && reqPart1.IsValid && reqPart2.IsValid)
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